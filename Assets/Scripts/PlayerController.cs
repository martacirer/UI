using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject cam;

    [HideInInspector] public Rigidbody _playerRigidbody;

    [HideInInspector] public static bool canMove = true;
    [HideInInspector] public bool activateSpeedControl = true;
    private bool canSlide = true;
    private bool isSliding;
    private bool isRunning;
    private bool canJump = true;
    private bool isGrounded = true;
    private bool isInLadder;
    private bool usingLadder = false;

    [Header("Speed Parametres\n")]
    [SerializeField] private float force = 30f;
    [SerializeField] private float groundDrag = 10f;
    [SerializeField] private float airMultiplyer;

    [HideInInspector] public float horizontalInput;
    [HideInInspector] public float verticalInput;

    private Vector3 moveDirection;

    [Header("Jump Parametres\n")]
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float jumpCooldown;
    [SerializeField] private float playerHight;

    [SerializeField] private LayerMask whatIsGorund;

    [Header("Gravity Modifier\n")]
    public float normalGrav = -10f;
    private float currentGrav;

    private float oneUnit = 1f;
    private float halfUnit = .5f;
    private float dobleUnit = 2f;
    private float tenthOfUnit = .1f;
    private float normalForceMulti = 10f;
    private float runningForceMulti = 14f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _playerRigidbody = GetComponent<Rigidbody>();

        normalGrav = Physics.gravity.y;
        currentGrav = normalGrav;
    }

    private void Update()
    {
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, cam.transform.eulerAngles.y, transform.eulerAngles.z);

        //Restricting the movement of the player with a boolean
        if (canMove)
        {
            PlayerInput();
            SpeedControl();
            
            if (isGrounded)
            {
                _playerRigidbody.drag = groundDrag;
                Physics.gravity = new Vector3(0, normalGrav, 0);
            }
            else
            {
                _playerRigidbody.drag = oneUnit;
                Physics.gravity = new Vector3(0, normalGrav, 0);
            }
        }
    }

    void FixedUpdate()
    {
        //Restricting the movement of the player with a boolean
        if (canMove)
        {
            MovePlayer();
        }
        
        //Raycast that checks if the player is touching the ground
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHight * halfUnit + tenthOfUnit, whatIsGorund);
    }

    //All the inputs on whitch the player depends to move arround
    void PlayerInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        //Jump if the player is on the ground
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.JoystickButton0))
        {
            if (canJump && isGrounded)
            {
                canJump = false;

                //transform.GetChild(0).GetChild(0).GetComponent<SpiderProceduralAnimation>().enabled = false;

                JumpMechanic();

                Invoke(nameof(JumpReset), jumpCooldown);
            }
            if (isInLadder)
            {
                usingLadder = true;
            }
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            usingLadder = false;
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            isRunning = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            isRunning = false;
        }

        //Player crouching state
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.JoystickButton8))
        {
            if(isGrounded && canSlide)
            {
                canJump = false;
                isSliding = true;
                canSlide = false;
                GetComponent<CapsuleCollider>().height = halfUnit;
                _playerRigidbody.AddForce(Vector3.down * force, ForceMode.Impulse);
            }
        }
    }

    //All the diferents move speeds of the player depending on if is touching the ground, running, crouching, attatched to a wall, grappling or in free fall
    void MovePlayer()
    {
        moveDirection = transform.forward * verticalInput + transform.right * horizontalInput;

        if (isGrounded)
        {
            if (isSliding)
            {
                _playerRigidbody.AddForce(moveDirection.normalized * force * dobleUnit, ForceMode.Force);
            }
            else if (isRunning)
            {
                _playerRigidbody.AddForce(moveDirection.normalized * force * runningForceMulti, ForceMode.Force);
            }
            else
            {
                _playerRigidbody.AddForce(moveDirection.normalized * force * normalForceMulti, ForceMode.Force);
            }
        }
        else if(usingLadder)
        {
            _playerRigidbody.AddForce(Vector3.up * force * normalForceMulti / 4, ForceMode.Force);
        }
        else
        {
            _playerRigidbody.AddForce(moveDirection.normalized * force * (normalForceMulti - dobleUnit) * airMultiplyer, ForceMode.Force);
        }
    }

    //If the player isn't running the players max speed is clamped to a max value
    void SpeedControl()
    {
        if (activateSpeedControl)
        {
            Vector3 flatVel = new Vector3(_playerRigidbody.velocity.x, 0, _playerRigidbody.velocity.z);
            float newForce = force / dobleUnit;

            if (flatVel.magnitude > newForce && !isRunning)
            {
                Vector3 limitedVel = flatVel.normalized * newForce;
                _playerRigidbody.velocity = new Vector3(limitedVel.x, _playerRigidbody.velocity.y, limitedVel.z);
            }
        }
    }

    //When called this function adds an upwards force to the player
    void JumpMechanic()
    {
        _playerRigidbody.velocity = new Vector3(_playerRigidbody.velocity.x, 0f, _playerRigidbody.velocity.z);

        _playerRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    //When called set the ability to jump to true
    void JumpReset()
    {
        canJump = true;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ladder"))
        {
            normalGrav = 0;
        }
    }

    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.CompareTag("Ladder") && !isGrounded)
        {
            isInLadder = true;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Ladder"))
        {
            isInLadder = false;
            usingLadder = false;
            _playerRigidbody.velocity = Vector3.up * 2;
            normalGrav = currentGrav;
        }
    }
}