using UnityEngine;

namespace GravityAttractor
{
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
        public static double Distance(Vector3d a, Vector3d b) => (a - b).magnitude;
    }

    public static class GravityAttractor
    {
        public static Vector3d CalculateGravity(Vector3d objectPos, Vector3d planetPos, double objectMass, double planetMass)
        {
            Vector3d direction = planetPos - objectPos;
            double distance = direction.magnitude;
            if (distance < 1e-6) return Vector3d.zero;
            double forceMagnitude = (6.67430e-11 * objectMass * planetMass) / (distance * distance);
            return direction.normalized * forceMagnitude;
        }
    }
}