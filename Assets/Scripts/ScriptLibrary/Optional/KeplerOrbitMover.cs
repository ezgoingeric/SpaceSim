using UnityEngine;

//[System.Serializable]

namespace KeplerOrbitMover
{
    public class CelestialBody
    {
        public string name;
        public double[] position;
        public double[] velocity;
        public double mass;
        public float baseSize;
        public Transform transform;
    }

    public class Vector3d
    {
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

    public class KeplerOrbitMover : MonoBehaviour
    {
        public CelestialBody attractor; // Central body (e.g., Sun)
        public Vector3d velocity = Vector3d.zero; // Initial velocity
        private Vector3d position;

        void Start()
        {
            position = new Vector3d(transform.position.x, transform.position.y, transform.position.z);
        }

        void Update()
        {
            if (attractor == null || attractor.transform == null) return;
            Vector3d attractorPos = new Vector3d(attractor.transform.position.x, attractor.transform.position.y, attractor.transform.position.z);
            // Simplified Keplerian orbit update
            velocity += CalculateAcceleration(attractorPos) * Time.deltaTime;
            position += velocity * Time.deltaTime;
            transform.position = position.ToVector3();
        }

        private Vector3d CalculateAcceleration(Vector3d attractorPos)
        {
            double G = 6.67430e-11;
            double attractorMass = attractor.mass;
            Vector3d direction = attractorPos - position;
            double distance = direction.magnitude;
            if (distance < 1e-6) return Vector3d.zero;
            double force = G * attractorMass / (distance * distance);
            return direction.normalized * force;
        }
    }
}