using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrashCannon : MonoBehaviour, IActivable
{
    public GameObject explosion;
    public GameObject cannon;
    public GameObject afterExplosion;
    public GameObject trash;
    void Start()
    {
    }


    void Update()
    {
    }

    public void Activate()
    {
        explosion.SetActive(true);
        trash.SetActive(true);
        afterExplosion.SetActive(true);

        Destroy(cannon);
    }
}