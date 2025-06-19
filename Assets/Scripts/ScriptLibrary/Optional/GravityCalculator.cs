using UnityEngine;

// [System.Serializable]

namespace GravityCalculatorNS
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
}

public class GravityCalculator {
    public static Vector3d CalculateGravityForce(Vector3d pos1, Vector3d pos2, double mass1, double mass2) {
        double G = 6.67408e-11; // Gravitational constant
        Vector3d direction = pos2 - pos1;
        double distance = direction.magnitude;
        if (distance < 1e-6) return Vector3d.zero;
        double forceMagnitude = G * mass1 * mass2 / (distance * distance);
        return direction.normalized * forceMagnitude;
    }
}
