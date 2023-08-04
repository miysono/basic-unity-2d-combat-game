using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
  public float runSpeed = 0.6f; // Running speed.
  public float jumpForce = 2.6f; // Jump height.

  private Rigidbody2D body; // Variable for the RigidBody2D component.
  private SpriteRenderer sr; // Variable for the SpriteRenderer component.
  private Animator animator; // Variable for the Animator component. [OPTIONAL]

  private bool isGrounded; // Variable that will check if character is on the ground.
  private bool isOnPlatform;
  public GameObject groundCheckPoint; // The object through which the isGrounded check is performed.
  public float groundCheckRadius; // isGrounded check radius.
  public LayerMask groundLayer; // Layer wich the character can jump on.
  public LayerMask wallLayer;


  public SpeedPickup speedPickup;
  private float pickupSpeedMultiplier = 1f;
  private float lastPickupTime = 0f;
  public float pickupDuration = 5f;


  //BASIC MOVEMENT
  private bool jumpPressed = false; // Variable that will check is "Space" key is pressed.
  private bool APressed = false; // Variable that will check is "A" key is pressed.
  private bool DPressed = false; // Variable that will check is "D" key is pressed.
  private bool isJumping = false;
  private bool isFalling = false;
  private float lastJumpedTime = 0f;
  private float jumpCooldown = .5f; // 0> continue & fix
  private float jumpCount = 0;
  private bool canJump = true;

  //DASHING
  public float dashSpeed = 10f;
  public float dashDuration = 0.5f;
  public float dashCooldown = 1f;
  bool canDash = true;
  bool dashPressed = false;
  float currentDashDuration;
  float currentDashCooldown;

  //WALL EDGE DETECTION
  bool onWallEdge = false;
  public float climbSpeed = 5f;

  //CLIMBING CHAINS
  public float chainClimbSpeed = 10f; // speed at which the player climbs the ladder
  private bool isClimbing; // whether the player is currently climbing the ladder
  private float inputVertical; // vertical input from the player
  public float gravityScale;
  bool canClimb = false;

  //Platforms
  public GameObject platform;

  private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Chains"))
        {
            canClimb = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Chains"))
        {
            canClimb = false;
        }
    }


  void Awake() {
    body = GetComponent<Rigidbody2D>(); // Setting the RigidBody2D component.
    sr = GetComponent<SpriteRenderer>(); // Setting the SpriteRenderer component.
    animator = GetComponent<Animator>(); // Setting the Animator component. [OPTIONAL]
    wallLayer = LayerMask.GetMask("Wall");
  }

  // Update() is called every frame.
  void Update() {
    if (Input.GetKeyDown(KeyCode.Space)) jumpPressed = true; // Checking on "Space" key pressed.
    if (Input.GetKey(KeyCode.A)) APressed = true; // Checking on "A" key pressed.
    if (Input.GetKey(KeyCode.D)) DPressed = true; // Checking on "D" key pressed.
    if (Input.GetKeyDown(KeyCode.LeftShift)) dashPressed = true;
  }

  // Update using for physics calculations.
  void FixedUpdate() {
      isGrounded = Physics2D.OverlapCircle(groundCheckPoint.transform.position, groundCheckRadius, groundLayer); // Checking if character is on the ground.
      isOnPlatform = Physics2D.OverlapCircle(groundCheckPoint.transform.position, groundCheckRadius, LayerMask.GetMask("Platform")); // Checking if character is on the ground


      animator.SetFloat("Speed" , Mathf.Abs(Input.GetAxis("Horizontal")));
      // Left/Right movement.
      if (APressed) {
          Collider2D hit = Physics2D.OverlapCircle(transform.position + Vector3.left * 0.5f, 0.05f, wallLayer);
          if(!hit)
          {
          body.velocity = new Vector2(-runSpeed, body.velocity.y); // Move left physics.
          transform.eulerAngles = new Vector3(transform.eulerAngles.x, 180, transform.eulerAngles.z); // Rotating the character object to the left.
          APressed = false; // Returning initial value.
          }
      }
      else if (DPressed) {
          Collider2D hit = Physics2D.OverlapCircle(transform.position + Vector3.right * 0.5f, 0.1F, wallLayer);
          if(!hit)
          {
          body.velocity = new Vector2(runSpeed, body.velocity.y); // Move right physics.
          transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0, transform.eulerAngles.z); // Rotating the character object to the right.
          DPressed = false; // Returning initial value.
          }
      }
      else body.velocity = new Vector2(0, body.velocity.y);

      // Jumps.
      if (jumpPressed && jumpCount < 1 && canJump)
       {
          jumpPressed = false;
          body.velocity = new Vector2(0, jumpForce); // Jump physics.
          jumpCount++;
      }
      if(isGrounded || isOnPlatform){
          jumpCount = 0;
      } 

      if(body.velocity.y < -2)
      {
        isFalling = true;
      } else isFalling = false;
      animator.SetBool("IsFalling", isFalling);

      if(body.velocity.y > 1)
      {
        isJumping = true;
      }else isJumping = false;
      animator.SetBool("IsJumping", isJumping);

  
      // Dashes
      if(dashPressed && canDash){
        currentDashDuration = dashDuration;
        currentDashCooldown = dashCooldown;
        canDash = false;
        dashPressed = false;
        animator.SetTrigger("IsDashing");
      }
      if(currentDashDuration > 0)
      {
        body.velocity = transform.right * dashSpeed;
        currentDashDuration -= Time.deltaTime;
      }
      if(currentDashCooldown > 0)
      {
        currentDashCooldown -= Time.deltaTime;
      }
      if(currentDashCooldown <= 0)
      {
        canDash = true;
      }



      //CHAINS CLIMBING
      if(canClimb && Input.GetKey(KeyCode.W)){
            isClimbing = true;
            body.gravityScale = 0;
            canJump = false;
        }else{
            isClimbing = false;
            body.gravityScale = 5;
            canJump = true;
            animator.SetBool("ChainClimbing", false);
        }
        if (isClimbing)
        {
            transform.Translate(Vector2.up * chainClimbSpeed * Time.deltaTime);
            animator.SetBool("ChainClimbing", true);
        }
        
        //Platform Mechanic

        if(isClimbing || Input.GetKey(KeyCode.S)) platform.GetComponent<Collider2D>().enabled = false;
        else platform.GetComponent<Collider2D>().enabled = true;


        if(speedPickup.speedPicked)
        {
          lastPickupTime = Time.time;
          pickupSpeedMultiplier = 1.5f;
          runSpeed = runSpeed * pickupSpeedMultiplier;
          speedPickup.speedPicked = false;
        }


        if(Time.time - lastPickupTime > pickupDuration)
        {
          runSpeed = runSpeed / pickupSpeedMultiplier;
          pickupSpeedMultiplier = 1;
        }

  }

        
}