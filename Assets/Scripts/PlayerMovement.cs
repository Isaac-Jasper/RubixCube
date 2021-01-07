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
    //public float counter = 0;
    private float baseSpeed;

    public LayerMask groundMask;
    public Transform groundCheck;

    //public bool timer = false;
    //public bool canRRJump; //road runner jump
    Vector3 velocity;
    bool canJump;

    void Start()
    {
        baseSpeed = speed;
    }
    void Update()
    {
        //float counterHolder;
        //counter += Time.deltaTime;
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

        /*if (timer)
        {
            canRRJump = true;
            counterHolder = counter;
            if (counter - counterHolder > 1)
            {
                timer = false;
                canRRJump = false;
            }
        }*/
    }

    /*bool canJump(bool jumped)
    {
        bool RRFrames = true;
        if (jumped) //if you jumped you cant jump again return normal
        {
            RRFrames = false;
            return Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        }
        if (RRFrames == false && Physics.CheckSphere(groundCheck.position, groundDistance, groundMask)) //if you can jump but you cant RRJump you can cyotejump
        {
            RRFrames = true;
        }
        if (Physics.CheckSphere(groundCheck.position, groundDistance, groundMask) == false && RRFrames && canRRJump) //if you cant jump (are in the air) and can RRJump jump
        {
            RRFrames = false;
            return true;
        }
        if (Physics.CheckSphere(groundCheck.position, groundDistance, groundMask) == false) //if your in the air and didnt jump start a counter
        {
            timer = true;
        }

        return Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
    }*/// theoretical cyote frames code 
}
