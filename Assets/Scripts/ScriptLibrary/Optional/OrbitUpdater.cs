using UnityEngine;
using System.Collections.Generic;
/*
//[System.Serializable]

namespace OrbitUpdater
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

    public class OrbitUpdater : MonoBehaviour
    {
        public List<CelestialBody> otherBodies; // List of other celestial bodies
        public Vector3d velocity = Vector3d.zero;
        public double mass = 5.972e24; // Earth-like mass
        private Vector3d position;

        void Start()
        {
            position = new Vector3d(transform.position.x, transform.position.y, transform.position.z);
        }

        void Update()
        {
            if (otherBodies == null) return;
            position = new Vector3d(transform.position.x, transform.position.y, transform.position.z);
            Vector3d acceleration = CalculateTotalAcceleration();
            velocity += acceleration * Time.deltaTime;
            position += velocity * Time.deltaTime;
            transform.position = position.ToVector3();
        }

        private Vector3d CalculateTotalAcceleration()
        {
            Vector3d totalForce = Vector3d.zero;
            foreach (var body in otherBodies)
            {
                if (body.transform == null || body.transform == transform) continue;
                Vector3d bodyPos = new Vector3d(body.transform.position.x, body.transform.position.y, body.transform.position.z);
                totalForce += GravityCalculator.CalculateGravityForce(position, bodyPos, mass, body.mass);
            }
            return totalForce / mass; // a = F/m
        }
    }
}
*/