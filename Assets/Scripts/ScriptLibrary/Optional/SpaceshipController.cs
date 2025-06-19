using UnityEngine;

public class SpaceshipController : MonoBehaviour {
    public float thrust = 1000f;
    public float rotationSpeed = 100f;
    public CelestialBody nearestBody; // Optional gravity integration

    void Update() {
        if (nearestBody != null && nearestBody.transform != null) {
            Vector3d playerPos = new Vector3d(transform.position.x, transform.position.y, transform.position.z);
            Vector3d bodyPos = new Vector3d(nearestBody.transform.position.x, nearestBody.transform.position.y, nearestBody.transform.position.z);
            Vector3d gravityForce = GravityCalculator.CalculateGravityForce(playerPos, bodyPos, 1.0, nearestBody.mass);
            transform.position += gravityForce.ToVector3() * Time.deltaTime;
        }
        float moveInput = Input.GetAxis("Vertical");
        float yawInput = Input.GetAxis("Horizontal");
        Vector3 force = transform.forward * moveInput * thrust * Time.deltaTime;
        transform.position += force;
        transform.Rotate(0, yawInput * rotationSpeed * Time.deltaTime, 0);
    }
}