using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveJumps : MonoBehaviour
{
    public GameManager gameManager;
    public PlayerMovement playerMovement;
    public int JumpCount;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.gm.PlayerMovement.GiveJump(JumpCount);
            GameManager.gm.PlayerMovement.controller.enabled = false;
            GameManager.gm.PlayerMovement.gameObject.transform.position = new Vector3(3, 19, -17);
            GameManager.gm.PlayerMovement.controller.enabled = true;
            this.enabled = false;


            gameObject.transform.position =
                new Vector3(transform.position.x, transform.position.y - 0.19f, transform.position.z);
            Destroy(gameObject);
        }
    }
}