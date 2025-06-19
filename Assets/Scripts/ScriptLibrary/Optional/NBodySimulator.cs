using UnityEngine;
using System.Collections.Generic;
/*
//[System.Serializable]

namespace NBodySimulator
{
    public class CelestialBody
    {
        public string name;
        public double[] position;
        public double[] velocity;
        public double mass;
        public float baseSize;
        public Transform transform;
        public Vector3d Velocity
        {
            get => new Vector3d(velocity[0], velocity[1], velocity[2]);
            set => velocity = new double[] { value.x, value.y, value.z };
        }
        public Vector3d Position
        {
            get => new Vector3d(position[0], position[1], position[2]);
            set => position = new double[] { value.x, value.y, value.z };
        }
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
        public static Vector3d operator /(Vector3d a, double b) => new Vector3d(a.x / b, a.y / b, a.z / b);
        public Vector3 ToVector3() => new Vector3((float)x, (float)y, (float)z);
    }

    public class NBodySimulator : MonoBehaviour
    {
        public List<CelestialBody> bodies;

        void Update()
        {
            if (bodies == null) return;
            foreach (var body in bodies)
            {
                if (body.transform == null) continue;
                Vector3d acceleration = Vector3d.zero;
                Vector3d pos = new Vector3d(body.transform.position.x, body.transform.position.y, body.transform.position.z);
                foreach (var other in bodies)
                {
                    if (other == body || other.transform == null) continue;
                    Vector3d otherPos = new Vector3d(other.transform.position.x, other.transform.position.y, other.transform.position.z);
                    acceleration += GravityCalculator.CalculateGravityForce(pos, otherPos, body.mass, other.mass) / body.mass;
                }
                body.Velocity += acceleration * Time.deltaTime;
                body.Position = pos + body.Velocity * Time.deltaTime;
                body.transform.position = body.Position.ToVector3();
            }
        }
    }
}
*/