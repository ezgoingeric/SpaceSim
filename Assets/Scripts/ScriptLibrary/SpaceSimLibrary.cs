using UnityEngine;
using System.Runtime.InteropServices;
using System.Collections.Generic;

[System.Serializable]
public class CelestialBody {
    public string name;
    public double[] position; // [x, y, z] in meters
    public double[] velocity; // [vx, vy, vz] in m/s
    public double mass; // in kg
    public float baseSize; // Base size for rendering (in Unity units)
    public Transform transform; // Unity transform for rendering
}

public class Vector3d {
    public double x, y, z;
    public Vector3d(double x, double y, double z) { this.x = x; this.y = y; this.z = z; }
    public static Vector3d zero = new Vector3d(0, 0, 0);
    public double magnitude => Mathf.Sqrt((float)(x * x + y * y + z * z));
    public Vector3d normalized => new Vector3d(x / magnitude, y / magnitude, z / magnitude);
    public static Vector3d operator +(Vector3d a, Vector3d b) => new Vector3d(a.x + b.x, a.y + b.y, a.z + b.z);
    public static Vector3d operator -(Vector3d a, Vector3d b) => new Vector3d(a.x - b.x, a.y - b.y, a.z - b.z);
    public static Vector3d operator *(Vector3d a, double b) => new Vector3d(a.x * b, a.y * b, a.z * b);
    public Vector3 ToVector3() => new Vector3((float)x, (float)y, (float)z);
}

public class SpaceSimLibrary : MonoBehaviour {
    public List<CelestialBody> celestialBodies = new List<CelestialBody>();
    public Transform playerTransform;
    public TextAsset jsonData; // JSON file with celestial data
    private const double G = 6.67430e-11; // Gravitational constant (m^3 kg^-1 s^-2)
    private const float k = 1000f; // Scaling constant for hybrid scaling
    private Vector3d originOffset = Vector3d.zero; // Floating origin offset

    // DLL imports for PhysicsPlugin
    [DllImport("PhysicsPlugin")]
    private static extern void UpdateOrbit(ref double[] positions, ref double[] velocities, double[] masses, int bodyCount, double deltaTime);

    [DllImport("PhysicsPlugin")]
    private static extern void CalculateGravity(double[] pos1, double[] pos2, double mass1, double mass2, ref double[] force);

    void Start() {
        // Load celestial bodies from JSON
        if (jsonData != null) {
            celestialBodies = new List<CelestialBody>(JsonUtility.FromJson<CelestialBody[]>(jsonData.text));
            foreach (var body in celestialBodies) {
                GameObject go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                body.transform = go.transform;
                body.transform.position = new Vector3((float)body.position[0], (float)body.position[1], (float)body.position[2]);
            }
        }
    }

    void Update() {
        // Update floating origin
        UpdateFloatingOrigin();
        // Update orbits using PhysicsPlugin.dll
        UpdateOrbits();
        // Update visuals for celestial bodies
        foreach (var body in celestialBodies) {
            UpdateCelestialBodyVisual(body);
        }
        // Update player movement
        UpdatePlayerMovement();
    }

    // Calculate gravitational force using DLL
    public Vector3d CalculateGravitationalForce(CelestialBody body1, CelestialBody body2) {
        double[] force = new double[3];
        CalculateGravity(body1.position, body2.position, body1.mass, body2.mass, ref force);
        return new Vector3d(force[0], force[1], force[2]);
    }

    // Update orbits using Dormand-Prince 5(4) integrator from DLL
    void UpdateOrbits() {
        double[] positions = new double[celestialBodies.Count * 3];
        double[] velocities = new double[celestialBodies.Count * 3];
        double[] masses = new double[celestialBodies.Count];
        for (int i = 0; i < celestialBodies.Count; i++) {
            positions[i * 3] = celestialBodies[i].position[0] - originOffset.x;
            positions[i * 3 + 1] = celestialBodies[i].position[1] - originOffset.y;
            positions[i * 3 + 2] = celestialBodies[i].position[2] - originOffset.z;
            velocities[i * 3] = celestialBodies[i].velocity[0];
            velocities[i * 3 + 1] = celestialBodies[i].velocity[1];
            velocities[i * 3 + 2] = celestialBodies[i].velocity[2];
            masses[i] = celestialBodies[i].mass;
        }
        UpdateOrbit(ref positions, ref velocities, masses, celestialBodies.Count, Time.deltaTime);
        for (int i = 0; i < celestialBodies.Count; i++) {
            celestialBodies[i].position[0] = positions[i * 3] + originOffset.x;
            celestialBodies[i].position[1] = positions[i * 3 + 1] + originOffset.y;
            celestialBodies[i].position[2] = positions[i * 3 + 2] + originOffset.z;
            celestialBodies[i].velocity[0] = velocities[i * 3];
            celestialBodies[i].velocity[1] = velocities[i * 3 + 1];
            celestialBodies[i].velocity[2] = velocities[i * 3 + 2];
            celestialBodies[i].transform.position = new Vector3((float)celestialBodies[i].position[0], (float)celestialBodies[i].position[1], (float)celestialBodies[i].position[2]);
        }
    }

    // Update player movement
    void UpdatePlayerMovement() {
        float moveSpeed = 1000f;
        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        playerTransform.position += playerTransform.TransformDirection(input) * moveSpeed * Time.deltaTime;
    }

    // Update celestial body visuals with hybrid scaling
    void UpdateCelestialBodyVisual(CelestialBody body) {
        float distance = Vector3.Distance(body.transform.position, Camera.main.transform.position);
        float scaleFactor = Mathf.Pow(k / distance, 0.5f);
        body.transform.localScale = Vector3.one * body.baseSize * scaleFactor;
    }

    // Floating origin to maintain precision
    void UpdateFloatingOrigin() {
        if (Vector3.Distance(playerTransform.position, Vector3.zero) > 1e6) {
            Vector3d playerPos = new Vector3d(playerTransform.position.x, playerTransform.position.y, playerTransform.position.z);
            originOffset += playerPos;
            foreach (var body in celestialBodies) {
                body.position[0] -= playerPos.x;
                body.position[1] -= playerPos.y;
                body.position[2] -= playerPos.z;
                body.transform.position = new Vector3((float)body.position[0], (float)body.position[1], (float)body.position[2]);
            }
            playerTransform.position = Vector3.zero;
        }
    }
}