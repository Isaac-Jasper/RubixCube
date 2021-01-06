using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 10f;
    public float sprintSpeed;
    public float gravity = -9.81f;
    public float groundDistance = 0.4f;
    public float jumpHeight = 2f;
    private float baseSpeed;

    public LayerMask groundMask;
    public Transform groundCheck;

    Vector3 velocity;
    bool canJump;

    void Start()
    {
        baseSpeed = speed;
    }
    void Update()
    {
        canJump = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        //canJump = true;
        if (canJump && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && canJump)
            velocity.y += Mathf.Sqrt(jumpHeight * -2f * gravity);

        if (Input.GetButton("Sprint"))
            speed = sprintSpeed;
        else
            speed = baseSpeed;

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}
