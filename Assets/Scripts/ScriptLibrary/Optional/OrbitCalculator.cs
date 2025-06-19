using UnityEngine;
using System.Collections.Generic;

//[System.Serializable]

namespace OrbitCalculator
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

    public class OrbitCalculator : MonoBehaviour
    {
        public List<CelestialBody> InitializeKeplerOrbits(List<CelestialBody> bodies, int steps = 1000)
        {
            foreach (var body in bodies)
            {
                // Example orbital parameters (e.g., from NASA fact sheet)
                double semiMajorAxis = 5.79e10; // Mercury’s semi-major axis (m)
                double eccentricity = 0.2056; // Mercury’s eccentricity
                double[] orbitPositions = ComputeKeplerOrbit(semiMajorAxis, eccentricity, body.mass, steps);
                body.position = orbitPositions[0..3]; // Initial position
                                                      // Set initial velocity (simplified)
                body.velocity = new double[] { 0, 47.87e3, 0 }; // Mercury’s orbital velocity (m/s)
            }
            return bodies;
        }

        private double[] ComputeKeplerOrbit(double semiMajorAxis, double eccentricity, double mass, int steps)
        {
            double[] positions = new double[steps * 3];
            double G = 6.67430e-11;
            double mu = G * mass;
            for (int i = 0; i < steps; i++)
            {
                double theta = 2 * Mathf.PI * i / steps;
                double r = semiMajorAxis * (1 - eccentricity * eccentricity) / (1 + eccentricity * Mathf.Cos((float)theta));
                positions[i * 3] = r * Mathf.Cos((float)theta);
                positions[i * 3 + 1] = r * Mathf.Sin((float)theta);
                positions[i * 3 + 2] = 0;
            }
            return positions;
        }
    }
}
