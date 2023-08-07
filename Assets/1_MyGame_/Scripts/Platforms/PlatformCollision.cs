using UnityEngine;

public class PlatformCollision : MonoBehaviour
{
    private bool isPlayerOnPlatform = false;

    private Vector3 lastPlatformPosition;
    private Transform playerTransform;

    CharacterController controller;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            controller = other.gameObject.GetComponent<CharacterController>();


            isPlayerOnPlatform = true;
            playerTransform = other.gameObject.transform;
            //other.transform.SetParent(transform);
            playerTransform.SetParent(transform);

            //lastPlatformPosition = transform.position; // Zapisujemy pozycjê platformy w momencie wejœcia gracza
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerOnPlatform = false;
            //other.transform.parent = null;
            playerTransform.SetParent(null);
            playerTransform = null;

            //lastPlatformPosition = transform.position;
            //Reset(1f);
            //lastPlatformPosition = Vector3.zero;
        }
    }

    private void Update()
    {
        if (isPlayerOnPlatform && playerTransform != null)
        {
            Vector3 platformMovement = transform.position - lastPlatformPosition;
            Vector3 targetPosition = playerTransform.position + platformMovement;

            print(platformMovement + "movement");

            print(playerTransform.position + "przed");

            controller.Move(platformMovement);
            print(playerTransform.position + "po");
        }

        lastPlatformPosition = transform.position;
    }
}