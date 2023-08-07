using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveSword : MonoBehaviour
{
    public GameManager gameManager;
    public PlayerMovement playerMovement;

    public GameObject sword;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
           
                sword.SetActive(true);
            


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