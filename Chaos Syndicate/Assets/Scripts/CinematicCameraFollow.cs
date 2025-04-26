using UnityEngine;

public class CinematicCameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0, 5, -10);
    public float positionSmoothTime = 0.6f;
    public float rotationSmoothTime = 2f;
    public float minDistance = 3.49f; // minimum allowed distance before stopping movement
    public float minFollowSpeed = 0.25f; // slow down when very close

    private Vector3 velocity = Vector3.zero;
    private Quaternion targetRotation;

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + target.TransformDirection(offset);
        float distance = Vector3.Distance(transform.position, desiredPosition);

        float smoothTime = (distance < minDistance) ? minFollowSpeed : positionSmoothTime;

        // Smooth position
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothTime);

        // Smooth rotation
        targetRotation = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSmoothTime);
    }
}