using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour, IActivable
{
    private float startAngle = 0.0f; // pocz�tkowy k�t rotacji
    private float targetAngle = -90.0f; // docelowy k�t rotacji
    public float moveTime = 5.0f; // czas trwania ruchu
    private float timer = 0.0f; // aktualny czas ruchu

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime; // zwi�kszenie czasu ruchu o czas jednej klatki

        if (timer < moveTime)
        {
            float t = timer / moveTime; // normalizacja czasu do przedzia�u [0,1]
            float angle = Mathf.Lerp(startAngle, targetAngle, t); // interpolacja liniowa k�ta rotacji
            transform.rotation = Quaternion.Euler(0, angle, 0); // ustawienie nowego k�ta rotacji
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, targetAngle, 0); // ustawienie docelowego k�ta rotacji
        }
    }

    public void Activate()
    {
        this.enabled = true;
    }
}