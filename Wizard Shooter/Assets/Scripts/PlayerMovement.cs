using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField]
    float mouseSensitivity = 3.5f;

    Transform cameraTrans;
    float cameraPitch = 0;

    [Header("Movement")]
    public float moveSpeed;
    public float dashSpeed;

    public float groundDrag;
    //jump
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;
    //dash
    public float dashForce;
    public float dashCooldown;
    public int dashCost;
    bool readyToDash;
    bool dashing;

    [HideInInspector] public float walkSpeed;
    [HideInInspector] public float sprintSpeed;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        cameraTrans = Camera.main.transform;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        readyToJump = true;
        readyToDash = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Camera;
        Vector2 mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        transform.Rotate(Vector3.up * mouseDelta.x * mouseSensitivity);

        cameraPitch -= mouseDelta.y * mouseSensitivity;
        cameraPitch = Mathf.Clamp(cameraPitch, -90, 90);
        cameraTrans.localEulerAngles = Vector3.right * cameraPitch;

        // ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, whatIsGround);

        MyInput();
        SpeedControl();

        // handle drag
        if (grounded&&!dashing)
            rb.drag = groundDrag;
        else
            rb.drag = 0;
    }
    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // when to jump
        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && readyToDash&&GameManager.instance.mana>=dashCost)
        {
            readyToDash = false;
            Dash();
            Invoke(nameof(ResetDash), dashCooldown);
        }
    }

    private void MovePlayer()
    {
        // calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // on ground
        if (grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

        // in air
        else if (!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        float curSpeed = moveSpeed;
        if (dashing)
            curSpeed = dashSpeed;
        else
            curSpeed = moveSpeed;
        // limit velocity if needed
        if (flatVel.magnitude > curSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        // reset y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    private void ResetJump()
    {
        
        readyToJump = true;
    }

    void Dash()
    {
        GameManager.instance.mana -= dashCost;
        dashing = true;
        Vector3 dir =  GetDirection();

        rb.AddForce(dir* dashForce, ForceMode.Impulse);
        StartCoroutine(StopDash());
    }
    IEnumerator StopDash()
    {
        
        yield return new WaitForSeconds(0.5f);
        dashing = false;
    }
    private void ResetDash()
    {        
        readyToDash = true;
        
    }
    private Vector3 GetDirection()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3();

        direction = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (verticalInput == 0 && horizontalInput == 0)
            direction = orientation.forward;

        return direction.normalized;
    }
}
