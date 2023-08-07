using UnityEngine;

public class OrbitingObject : MonoBehaviour
{
    public Transform playerTransform;
    public float rotationSpeed = 5f;
    public float minVerticalAngle = 40f;
    public float maxVerticalAngle = 21f;
    public float minDistance = 5f;
    public float maxDistance = 20f;

    private void Update()
    {
        Vector3 directionToPlayer = playerTransform.position - transform.position;
        float distanceToPlayer = directionToPlayer.magnitude;
        float normalizedDistance = Mathf.Clamp01((distanceToPlayer - minDistance) / (maxDistance - minDistance));
        float targetVerticalAngle = Mathf.Lerp(maxVerticalAngle, minVerticalAngle, normalizedDistance);
        Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer, Vector3.up) *
                                    Quaternion.Euler(targetVerticalAngle, 0f, 0f);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}