using UnityEngine;

//[System.Serializable]
namespace OrbitVisualizer
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

    public class OrbitVisualizer : MonoBehaviour
    {
        public CelestialBody body;
        private LineRenderer lineRenderer;

        void Start()
        {
            lineRenderer = gameObject.AddComponent<LineRenderer>();
            lineRenderer.startColor = new Color(1, 1, 1, 0.2f);
            lineRenderer.endColor = new Color(1, 1, 1, 0.2f);
            lineRenderer.positionCount = 100;
            UpdateOrbitPath();
        }

        void Update()
        {
            UpdateOrbitPath();
        }

        void UpdateOrbitPath()
        {
            for (int i = 0; i < 100; i++)
            {
                float theta = 2 * Mathf.PI * i / 100;
                Vector3 pos = new Vector3(
                    (float)body.position[0] * Mathf.Cos(theta),
                    0,
                    (float)body.position[0] * Mathf.Sin(theta)
                );
                lineRenderer.SetPosition(i, pos);
            }
        }
    }
}