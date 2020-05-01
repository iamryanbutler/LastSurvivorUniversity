using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed;
    public float rotationSpeed;
    public float dashSpeed;

    private float dashCooldownTimer;
    private float dashDuration;
    private Rigidbody rb;
    private Vector3 movement;
    static Animator anim;

    void Start()
    {
        movementSpeed = movementSpeed != 0f ? movementSpeed : 6.5f;
        rotationSpeed = rotationSpeed != 0f ? rotationSpeed : 0.15f;
        dashSpeed = dashSpeed != 0f ? dashSpeed : 20f;
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        ManageTimers();

        movement = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));

        if (movement != Vector3.zero)
        {
            anim.SetBool("isRunning", true);
            transform.Translate(movement * movementSpeed * Time.deltaTime, Space.World);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), rotationSpeed);

            if (dashCooldownTimer <= 0f && Input.GetKey(KeyCode.LeftShift))
            {
                rb.velocity = movement * dashSpeed;
                StartTimers();
            }

        }

        else
            anim.SetBool("isRunning", false);
        

    }

    void ManageTimers()
    {
        dashCooldownTimer = dashCooldownTimer > 0f ? dashCooldownTimer - Time.deltaTime : 0f;

        if (dashDuration > 0f)
            dashDuration -= Time.deltaTime;
        else
        {
            dashDuration = 0f;
            rb.velocity = Vector3.zero;
        }
    }

    void StartTimers()
    {
        dashDuration = .2f;
        dashCooldownTimer = 5f;
    }

    

}
