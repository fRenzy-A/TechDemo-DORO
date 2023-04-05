using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Transform m_Camera;

    [Header("Movement")]
    public float movespeed = 6f;
    float outofCrouch;
    public float crouchmovespeed = 2f;
    public float movementMultiplier = 10f;    
    public float slideBoost = 4f;
    [SerializeField] float airMultiplier = 0.4f;

    [Header("Dash")]
    public float dashstrength = 6f;
    public float dashCDValue = 1.0f;
    public float dashCD = 0.0f;
    public float slideCDValue = 2.0f;
    public float slideCD = 0.0f;
    public bool canDash = true;
    public bool canSlideBoost = true;

    [Header("Keybinds")]
    [SerializeField] KeyCode jumpKey = KeyCode.Space;
    [SerializeField] KeyCode crouchKey = KeyCode.LeftControl;
    [SerializeField] KeyCode dashKey = KeyCode.LeftShift;

    [Header("Jumping")]
    public float jumpForce = 5f;

    [Header("Drag")]
    float groundDrag = 6f;
    float airDrag = 2f;

    float playerHeight = 2f;

    


    public float crouch = -1f;

    float horizontalMovement;
    float verticalMovemement;
    

    bool isGrounded;
    Vector3 moveDirection;

    Rigidbody rb;
    public BoxCollider hitbox;
    public Transform orientation;
    private void Start()
    {
        outofCrouch = movespeed;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        dashCD = dashCDValue;
    }

    private void Update()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight / 2 + 0.1f);

        MyInput();
        ControlDrag();


        if (Input.GetKeyDown(jumpKey) && isGrounded)
        {
            Jump();
        }
        if (Input.GetKeyDown(crouchKey))
        {
            Crouch();
        }
        else if (Input.GetKeyUp(crouchKey) && isGrounded)
        {
            Stand();
        }
        else if (Input.GetKeyUp(crouchKey) && !isGrounded)
        {
            Stand();
        }
        if (Input.GetKeyDown(dashKey))
        {
            Dash();
        }
        if (!canDash)
        {
            DashCooldown();
        }
        if (!canSlideBoost)
        {
            SlideBoostCoolDown();
        }
        
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }
    void MyInput()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovemement = Input.GetAxisRaw("Vertical");

        moveDirection = transform.forward * verticalMovemement + transform.right * horizontalMovement;
    }
    void ControlDrag()
    {
        if (isGrounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = airDrag;
        }
    }

    void Jump()
    {
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    void Crouch()
    {
        if (canSlideBoost && rb.velocity.magnitude != 0 ) SlideBoost();

        movespeed = crouchmovespeed;
        m_Camera.transform.Translate(0,-0.5f, 0);

        hitbox.center = new Vector3(0, -0.5f,0);
        hitbox.size = new Vector3(0.75f, 1, 0.75f);
    }
    void SlideBoost()
    {
        if (rb.velocity.magnitude != 0 && isGrounded)
        {
            rb.AddForce(movespeed * slideBoost * moveDirection.normalized, ForceMode.Impulse);
            canSlideBoost = false;
        }
    }
    void Stand()
    {
        movespeed = outofCrouch;
        m_Camera.transform.Translate(0, 0.5f, 0);
        hitbox.center = new Vector3(0, 0, 0);
        hitbox.size = new Vector3(0.75f, 2, 0.75f);
    }

    void Dash()
    {
        if (canDash)
        {
            if (rb.velocity.magnitude == 0 && isGrounded)
            {
                rb.AddForce(orientation.forward * dashstrength * movespeed , ForceMode.VelocityChange);
            }
            else if (isGrounded)
            {
                rb.AddForce(moveDirection.normalized * movespeed * dashstrength, ForceMode.VelocityChange);
            }

            else if (isGrounded == false)
            {
                rb.AddForce(moveDirection.normalized * movespeed * airMultiplier * dashstrength, ForceMode.VelocityChange);
            }
            
            canDash = false;
        }
        
        
        
    }
    void DashCooldown()
    {
        dashCD -= Time.deltaTime;
        if (dashCD <= 0)
        {
            canDash = true;
            dashCD = dashCDValue;
        }
    }

    void SlideBoostCoolDown()
    {
        slideCD -= Time.deltaTime;
        if (slideCD <= 0)
        {
            canSlideBoost = true;
            slideCD = slideCDValue;
        }
    }


    void MovePlayer()
    {
        if (isGrounded)
        {
            rb.AddForce(moveDirection.normalized * movespeed * movementMultiplier, ForceMode.Acceleration);
        }
        else
        {
            rb.AddForce(moveDirection.normalized * movespeed * movementMultiplier * airMultiplier, ForceMode.Acceleration);
        }
        
    }
}
