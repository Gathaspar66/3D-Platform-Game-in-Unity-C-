using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    private float rotateSpeed = 45f; // Pr�dko�� obrotu w stopniach na sekund�


    void Update()
    {
        transform.Rotate(0f, rotateSpeed * Time.deltaTime, 0f); // Obracanie obiektu

        // Zapewnienie, �e warto�� obrotu nigdy nie przekroczy 360 stopni
        if (transform.rotation.eulerAngles.y > 360f)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x,
                0f,
                transform.rotation.eulerAngles.z);
        }
    }
}