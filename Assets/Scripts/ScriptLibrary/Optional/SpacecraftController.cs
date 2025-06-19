using UnityEngine;

/*
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

    public class SpacecraftController : MonoBehaviour
    {
        //public SpaceSimLibrary simLibrary;
        public Vector3d velocity = Vector3d.zero;
        public double mass = 1000; // Spacecraft mass (kg)
        private Vector3d position;

        void Update()
        {
            position = new Vector3d(transform.position.x, transform.position.y, transform.position.z);
            // Leapfrog integration
            Vector3d acceleration = CalculateAcceleration();
            velocity += acceleration * Time.deltaTime;
            position += velocity * Time.deltaTime;
            transform.position = position.ToVector3();
            // User input for thrust (simplified)
            float thrust = Input.GetAxis("Vertical") * 1000;
            velocity += new Vector3d(0, thrust, 0) * Time.deltaTime;
        }

        private Vector3d CalculateAcceleration()
        {
            Vector3d totalAcceleration = Vector3d.zero;
            foreach (var body in simLibrary.celestialBodies)
            {
                Vector3d bodyPos = new Vector3d(body.position[0], body.position[1], body.position[2]);
                Vector3d force = GravityAttractor.CalculateGravity(position, bodyPos, mass, body.mass);
                totalAcceleration += force / mass;
            }
            return totalAcceleration;
        }
    }
*/