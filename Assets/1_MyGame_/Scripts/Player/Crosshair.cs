using UnityEngine;

public class Crosshair : MonoBehaviour
{
    void Update()
    {
        Vector2 mousePos = Input.mousePosition;
        transform.position = mousePos;
    }
}