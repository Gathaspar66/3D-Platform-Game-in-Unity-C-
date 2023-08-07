using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementsMovingUp : MonoBehaviour, IActivable
{
    // Start is called before the first frame update
    private float start;

    public float distance;
    public float movement;

    void Start()
    {
        start = transform.position.y;
    }


    void Update()
    {
        if (transform.position.y < start + distance)
        {
            gameObject.transform.position =
                new Vector3(transform.position.x, transform.position.y + movement, transform.position.z);
        }
    }

    public void Activate()
    {
        this.enabled = true;
    }
}