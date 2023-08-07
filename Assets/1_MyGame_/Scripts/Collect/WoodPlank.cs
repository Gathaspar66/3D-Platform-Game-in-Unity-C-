using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodPlank : MonoBehaviour, IActivable
{
    void Start()
    {
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Rigidbody>().useGravity = true;
    }
    public void Activate()
    {
        this.enabled = true;
    }

}