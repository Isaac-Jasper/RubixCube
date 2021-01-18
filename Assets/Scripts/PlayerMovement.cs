using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public PlayerStats playerStats;
    public CharacterController controller;

    private float speed;
    public float sprintSpeed;
    public float gravity = -9.81f;
    public float groundDistance = 0.4f;
    
    private float baseSpeed;

    public LayerMask groundMask;
    public Transform groundCheck;

    public bool infiniteJumps;
    Vector3 velocity;
    bool canJump;

    void Start()
    {
        speed = playerStats.speed;
        baseSpeed = playerStats.speed; ;
    }
    void Update()
    {
        canJump = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (infiniteJumps)
            canJump = true;
        if (Physics.CheckSphere(groundCheck.position, groundDistance, groundMask) && velocity.y < 0)
            velocity.y = -2f;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * playerStats.speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && infiniteJumps)
            velocity.y = -2f;
        if (Input.GetButtonDown("Jump") && canJump)
            velocity.y += Mathf.Sqrt(playerStats.jumpheight * -2f * gravity);

        if (Input.GetButton("Sprint"))
            playerStats.speed = sprintSpeed;
        else
            playerStats.speed = baseSpeed;

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}
