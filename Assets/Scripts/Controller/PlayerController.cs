using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Settings")]
    public float moveSpeed = 2f;
    public float lookSpeed = 60f;
    public float jumpPower = 8f;
    public float gravity = 9.81f;

    [Header("Jump Timing")]
    public float jumpTimeLeniency = 0.1f;
    float timeToStopBeingLenient = 0;

    private CharacterController _controller;
    void Start()
    {
        SetUpCharacterController();
    }

    private void SetUpCharacterController()
    {
        _controller = GetComponent<CharacterController>();
        if (_controller == null)
        {
            Debug.LogError("there is no attached controller ");
        }
    }

    private void SetUpInputManager()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ProcessMovement();
        ProcessRotation();
    }


    Vector3 moveDirection;
    void ProcessMovement()
    {
        float leftRightInput = Input.GetAxis("Horizontal");
        float forwardBackwardInput = Input.GetAxis("Vertical");
        bool jumpPressed = Input.GetButtonDown("Jump");

        if (_controller.isGrounded)
        {
            timeToStopBeingLenient = Time.time + jumpTimeLeniency;

            moveDirection = new Vector3(leftRightInput, 0, forwardBackwardInput);

            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection = moveDirection * moveSpeed;

            if (jumpPressed)
            {
                moveDirection.y = jumpPower;
            }
        }
        else
        {
            moveDirection = new Vector3(leftRightInput * moveSpeed, moveDirection.y, forwardBackwardInput * moveSpeed);
            moveDirection = transform.TransformDirection(moveDirection);

            if (jumpPressed && Time.time < timeToStopBeingLenient)
            {
                moveDirection.y = jumpPower;
            }
        }

        moveDirection.y -= gravity * Time.deltaTime;

        if(_controller.isGrounded && moveDirection.y < 0)
        {
            moveDirection.y = -0.3f;
        }

        _controller.Move(moveDirection * Time.deltaTime);
    }

    void ProcessRotation()
    {
        float horizontalLoookInput = Input.GetAxis("Mouse X");
        Vector3 playerRotation = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(new Vector3(playerRotation.x, playerRotation.y + horizontalLoookInput * lookSpeed * 
            Time.deltaTime, playerRotation.z));
    }
}
