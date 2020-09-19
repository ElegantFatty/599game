﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float turnSpeed = 20f;
    public float JumpSpeed = 0.001f;
    private bool grounded = true;
    public float forwardForce = 10f; // forward force in fixedupdate
    public float sidewayForce = 1f; // force moving left or right
    Animator m_Animator;
    Rigidbody m_Rigidbody;
    Vector3 m_Movement;
    Quaternion m_Rotation = Quaternion.identity;

    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // float horizontal = Input.GetAxis("Horizontal");
        // float vertical = Input.GetAxis("Vertical");

        // m_Movement.Set(horizontal, 0f, vertical);
        // m_Movement.Normalize();

        // bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        // bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);
        // bool isWalking = hasHorizontalInput || hasVerticalInput;
        // m_Animator.SetBool("IsWalking", isWalking);

        // Vector3 desiredForward = Vector3.RotateTowards(transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);
        // m_Rotation = Quaternion.LookRotation(desiredForward);

        m_Movement.Set(0, 0, 1f);

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
            // m_Rigidbody.AddForce(sidewayForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
             m_Movement.Set(1f, 0, 1f);
        } else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
            // m_Rigidbody.AddForce(-sidewayForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
            m_Movement.Set(-1f, 0, 1f);
        }
        m_Movement.Normalize();
        m_Animator.SetBool("IsWalking", true);

        if (Input.GetKey(KeyCode.Space)) {
            Jump();
        }
    }

    void OnAnimatorMove()
    {
        m_Rigidbody.MovePosition(m_Rigidbody.position + 10 * m_Movement * m_Animator.deltaPosition.magnitude);
        m_Rigidbody.MoveRotation(m_Rotation);
    }

    private void Jump()  
    {
        if (grounded)
        {
            // m_Rigidbody.AddForce(0f, JumpSpeed, 0f, ForceMode.VelocityChange);
            m_Rigidbody.AddForce(Vector3.up * JumpSpeed);
            grounded = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        grounded = true;
    }
}

// base movement script by yini and jeff

// public class PlayerMovement : MonoBehaviour
// {
//     public Rigidbody rb; // the player rigidbody
//     public float initialForwardForce = 2000f;
//     public float forwardForce = 10f; // forward force in fixedupdate
//     public float sidewayForce = 1f; // force moving left or right
//     public float jumpForce = 10f; // forward force in fixedupdate
//     public GameObject ground;
//     public float forwardSpeed = 20f;
//     // Start is called before the first frame update
//     void Start()
//     {
//         // rb.AddForce(0, 200, 500);
//     }

//     // Update is called once per frame
//     // use FixedUpdate for physics (add force, etc)
//     void FixedUpdate()
//     {
//         // consider frame rate (different computers have different frame rate)
//         // use Time.deltaTime to even out the differences
//         rb.AddForce(0, 0, initialForwardForce * Time.deltaTime);

//         // use WASD and up down left right to control the player movement  
//         if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
//             rb.AddForce(sidewayForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
//         } else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
//             rb.AddForce(-sidewayForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
//         // } else if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) {
//         //     rb.AddForce(0, 0, forwardForce * Time.deltaTime, ForceMode.VelocityChange);
//         // } else if (Input.GetKey(KeyCode.S) ||  Input.GetKey(KeyCode.DownArrow)) {
//         //     rb.AddForce(0, 0, -forwardForce * Time.deltaTime, ForceMode.VelocityChange);
//         } else if (Input.GetKey(KeyCode.Space) ) {
//             rb.AddForce(0, jumpForce * Time.deltaTime, 0, ForceMode.VelocityChange);
//         }

//         // if the player falls off, restart the game
//         if (rb.position.y < -1f) {
//             FindObjectOfType<GameManager>().EndGame();
//         }
//     }
// }