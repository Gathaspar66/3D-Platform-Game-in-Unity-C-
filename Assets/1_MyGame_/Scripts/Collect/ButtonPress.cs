using Unity.VisualScripting;
using UnityEngine;

public class ButtonPress : MonoBehaviour
{
    private bool canUseButton = true;
    public GameManager gameManager;
    public PlayerMovement playerMovement;
    private float moveDistance = -0.09f;
    public GameObject activateObject;
    private bool isPlayerNear = false;

    private void Start()
    {
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canUseButton && isPlayerNear)
        {
            canUseButton = false;
            activateObject.GetComponent<IActivable>().Activate();
            MoveButton();
        }
    }


    public void MoveButton()
    {
        transform.localPosition += Vector3.up * moveDistance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerNear = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerNear = false;
        }
    }
}