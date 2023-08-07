using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    public float fallHeight = 3f;

    public bool doOnce = true;
    public bool canDie = false;

    void Update()
    {
        if (gameObject.transform.position.y < fallHeight && doOnce)
        {
            LevelManager.lm.PlayerDie();
            doOnce = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
            if (canDie)
            {
                LevelManager.lm.PlayerDie();
            }

           // canDie = true;
        }
    }
}