using UnityEngine;
using UnityEngine.UI;

/*
public class HUDReadout : MonoBehaviour
{
    public SpaceSimLibrary simLibrary;
    public Text hudText;

    void Update()
    {
        CelestialBody nearest = null;
        float minDistance = float.MaxValue;
        foreach (var body in simLibrary.celestialBodies)
        {
            float distance = Vector3.Distance(simLibrary.playerTransform.position, body.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearest = body;
            }
        }
        if (nearest != null)
        {
            hudText.text = $"Object: {nearest.name}, Mass: {nearest.mass:E2} kg, Distance: {minDistance / 1000:F0} km";
        }
    }
}
*/