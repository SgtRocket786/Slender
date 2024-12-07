using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class SlenderPlayerController : MonoBehaviour
{
    // Camera:
    public Camera playerCam;

    // Movement Settings:
    public float walkSpeed = 5f;
    public float runSpeed = 7f;
    public float jumpPower = 0f;
    public float gravity = 10f;

    // Camera Settings:
    public float lookSpeed = 2f;
    public float lookXLimit = 75f;

    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    // Can the Player Move?:
    private bool canMove = true;

    CharacterController characterController;

    void Start()
    {
        // Ensure we are using the Character Controller component:
        characterController = GetComponent<CharacterController>();

        // Lock and hide cursor:
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Walking/Running in Action:
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        bool isRunning = Input.GetKey(KeyCode.LeftShift);

        float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        // Jumping in Action:
        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpPower;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        characterController.Move(moveDirection * Time.deltaTime);

        // Camera Movement in Action:
        if (canMove)
        {
            rotationX -= Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCam.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
    }

    public void ResetPlayer()
    {
        // Reset player movement and camera states
        moveDirection = Vector3.zero;

        // Reset cursor lock and visibility
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
