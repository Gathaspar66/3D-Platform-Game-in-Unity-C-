using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 20f;
    public float jumpForce = 7f;
    private float sensitivity = 10.0f;
    private float maxYAngle = 80.0f;
    private Vector2 currentRotation;
    private Vector2 lastMousePosition;
    private Camera mainCamera;
    public CharacterController controller;
    private Vector3 movement = Vector3.zero;
    public bool isGrounded;
    public int jumpCount = 0;
    private int maxJumpCount = 0; //Wartoœæ 0 oznacza mo¿liwy 1 skok. Wartoœæ 1 oznacza 2 skoki.
    public bool canMove = true;
    public AudioSource audioSource;
    public AudioClip jumpSound;
    public AudioClip walkSound;

    private void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        mainCamera = Camera.main;
        controller = GetComponent<CharacterController>();
        GameManager.gm.PlayerMovement = this;
    }

    private void Update()
    {
        if (isGrounded)
        {
            jumpCount = maxJumpCount;
        }

        if (canMove)
        {
            MovePlayer();
            JumpPlayer();
            MoveCamera();
            controller.Move(movement * Time.deltaTime);
        }
    }

    private void FixedUpdate()
    {
        isGrounded = controller.isGrounded;
    }

    private void MovePlayer()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        Vector3 cameraForward = mainCamera.transform.forward;
        cameraForward.y = 0f;

        Vector3 movementDirection =
            (cameraForward * verticalInput + mainCamera.transform.right * horizontalInput).normalized;

        movementDirection *= moveSpeed;
        movementDirection.y = movement.y;
        movement = movementDirection;
        PlayWalkSound();
    }


    private void JumpPlayer()
    {
        if (movement.y < 0 && isGrounded)
        {
            movement.y = 0;
        }

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            audioSource.PlayOneShot(jumpSound, 4f);
            movement.y = jumpForce;
        }

        if (!isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            if (jumpCount > 0)
            {
                audioSource.PlayOneShot(jumpSound, 4f);
                movement.y = jumpForce;
                jumpCount -= 1;
            }
        }

        movement.y += Physics.gravity.y * Time.deltaTime;
    }

    private void MoveCamera()
    {
        //pobierz wartoœci osi myszy
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        //oblicz now¹ rotacjê kamery
        currentRotation.x += mouseX * sensitivity;
        currentRotation.y -= mouseY * sensitivity;
        currentRotation.y = Mathf.Clamp(currentRotation.y, -maxYAngle, maxYAngle);

        //przesuñ kursor na œrodek ekranu
        if (lastMousePosition.x != 0 || lastMousePosition.y != 0)
        {
            Vector2 deltaMouse = new Vector2(Screen.width / 2f - lastMousePosition.x,
                Screen.height / 2f - lastMousePosition.y);
            currentRotation.x += deltaMouse.x * sensitivity;
            currentRotation.y += deltaMouse.y * sensitivity;
            currentRotation.y = Mathf.Clamp(currentRotation.y, -maxYAngle, maxYAngle);
        }

        lastMousePosition = new Vector2(Screen.width / 2f, Screen.height / 2f); //zapisanie aktualnej pozycji myszy

        //obracanie gracza tylko w lewo i prawo
        transform.rotation = Quaternion.Euler(0f, currentRotation.x + 90, 0f);

        //obracanie kamery
        mainCamera.transform.eulerAngles = new Vector2(currentRotation.y, currentRotation.x);
    }

    public void PlayWalkSound()
    {
        if (controller.velocity.magnitude > 0 && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
        else if (controller.velocity.magnitude == 0)
        {
            audioSource.Stop();
        }
    }

    public void GiveJump(int Jumps)
    {
        this.maxJumpCount = Jumps;
    }
}