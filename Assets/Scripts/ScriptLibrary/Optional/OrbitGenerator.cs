using System.Collections.Generic;
using UnityEngine;

//[System.Serializable]

namespace OrbitGenerator
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

    public class OrbitGenerator : MonoBehaviour
    {
        public List<CelestialBody> GenerateOrbits(string systemName, int planetCount)
        {
            List<CelestialBody> bodies = new List<CelestialBody>();
            CelestialBody sun = new CelestialBody
            {
                name = systemName + " Sun",
                position = new double[] { 0, 0, 0 },
                velocity = new double[] { 0, 0, 0 },
                mass = 1.989e30, // Sun-like mass
                baseSize = 1.0f
            };
            bodies.Add(sun);
            for (int i = 0; i < planetCount; i++)
            {
                double semiMajorAxis = 5e10 * (i + 1); // Example orbital distance
                double orbitalSpeed = Mathf.Sqrt((float)(6.67430e-11 * sun.mass / semiMajorAxis));
                CelestialBody planet = new CelestialBody
                {
                    name = $"{systemName} Planet {i}",
                    position = new double[] { semiMajorAxis, 0, 0 },
                    velocity = new double[] { 0, orbitalSpeed, 0 },
                    mass = 5.972e24, // Earth-like mass
                    baseSize = 0.1f
                };
                bodies.Add(planet);
            }
            return bodies;
        }
    }
}