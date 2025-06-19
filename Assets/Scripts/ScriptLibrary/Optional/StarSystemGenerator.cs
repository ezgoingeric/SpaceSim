using System.Collections.Generic;

//[System.Serializable]
namespace StarSystemGenerator
{
    public class CelestialBody
    {
        public string name;
        public double[] position;
        public double[] velocity;
        public double mass;
        public float baseSize;
        //public Transform transform;
    }

    public class StarSystemGenerator // : MonoBehaviour
    {
        public List<CelestialBody> GenerateStarSystem(string systemName, int planetCount)
        {
            List<CelestialBody> bodies = new List<CelestialBody>();
            CelestialBody star = new CelestialBody
            {
                name = systemName + " Star",
                position = new double[] { 0, 0, 0 },
                velocity = new double[] { 0, 0, 0 },
                mass = 1.989e30,
                baseSize = 1.0f
            };
            bodies.Add(star);
            for (int i = 0; i < planetCount; i++)
            {
                CelestialBody planet = new CelestialBody
                {
                    name = $"{systemName} Planet {i}",
                    position = new double[] { 5e10 * (i + 1), 0, 0 },
                    velocity = new double[] { 0, 3e4, 0 },
                    mass = 5.972e24,
                    baseSize = 0.1f
                };
                bodies.Add(planet);
            }
            return bodies;
        }
    }
}