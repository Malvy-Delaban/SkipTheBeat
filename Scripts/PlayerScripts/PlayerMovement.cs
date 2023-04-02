using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Self components
    private Rigidbody2D rb;

    // External variables
    [SerializeField] private LayerMask mapLayer;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private GameObject platformCheck;
    [SerializeField] private SpriteRenderer sr;

    // GroundCheck variables
    private float widthGroundCheck = 0.5f;
    private float heightGroundCheck = 0.1f;
    private bool isGrounded = false;

    // Vertical movement variables
    private float jumpForceMultiplier = 22f;
    private int jumpNumberAvailable = 0;
    private int maxJumpNumber = 1;
    private float verticalVelocityThreshold = 0.1f;
    private bool isJumping = false;
    private bool isJumpAsked = false;
    private float currentPlayerInputVertical = 0f;
    private float traversePlatformVerticalAxisThreshold = 0.8f;
    private bool lastDirection = true;

    // Gravity variables
    private float initialGravityScale = 0f;
    private float fallGravityMultiplier = 2.5f;

    // Horizontal movement variables
    private float currentPlayerInputHorizontal = 0f;
    private float speedMovementHorizontalMultiplier = 10f;
    private float speedDashMultiplier = 30f;
    private bool isDashAsked = false;
    private bool isDashing = false;
    private Vector3 horizontalVelocityRef;
    private float smoothVelocityTiming = 0.08f;
    private float decelerationSmoothVelocityTiming = 0.05f;

    // Cooldowns
    private float timeOfDash = 0.15f;
    private float timeOfJump = 0.15f;
    private float cooldownDash = 3f;
    private float cooldownJump = 0.1f;

    // Timers
    private float timeRemainingBeforeEndOfDash = 0f;
    private float timeRemainingBeforeEndOfJump = 0f;
    private float timeBeforeNextDash = 0f;
    private float timeBeforeNextJump = 0f;

    // Buffering
    private float bufferingTime = 0.25f;
    private float timeBeforBufferingJumpCancel = 0f;
    private float timeBeforBufferingDashCancel = 0f;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        initialGravityScale = rb.gravityScale;
    }

    void Update()
    {
        GetPlayerInputs();
        UpdateTimers();
        UpdateUi();
    }

    void UpdateUi() {
        sr.color = (timeBeforeNextDash <= 0 ? new Color32(136, 190, 214, 255) : new Color32(0, 80, 100, 255));
    }

    void FixedUpdate() {
        GetGroundedStatus();
        MovePlayerHorizontally();
        MovePlayerVertically();
    }

    void UpdateTimers() {
        if (timeRemainingBeforeEndOfJump > 0)
            timeRemainingBeforeEndOfJump -= Time.deltaTime;
        if (timeRemainingBeforeEndOfDash > 0)
            timeRemainingBeforeEndOfDash -= Time.deltaTime;

        if (timeBeforBufferingJumpCancel > 0)
            timeBeforBufferingJumpCancel -= Time.deltaTime;
        if (timeBeforBufferingDashCancel > 0)
            timeBeforBufferingDashCancel -= Time.deltaTime;

        if (timeBeforeNextJump > 0)
            timeBeforeNextJump -= Time.deltaTime;
        if (timeBeforeNextDash > 0)
            timeBeforeNextDash -= Time.deltaTime;
    }

    void MovePlayerHorizontally() {
        if (isDashAsked && timeBeforBufferingDashCancel > 0 && timeBeforeNextDash <= 0) {
            isDashing = true;
            isDashAsked = false;
            timeRemainingBeforeEndOfDash = timeOfDash;
            timeBeforBufferingDashCancel = 0;
            timeBeforeNextDash = cooldownDash;
        } else if (isDashing && timeRemainingBeforeEndOfDash > 0) {
            rb.velocity = new Vector3((lastDirection == true ? 1f : -1f) * speedDashMultiplier, 0, 0);
        } else if (isDashing && timeRemainingBeforeEndOfDash > 0) {
            isDashing = false;
        } else {
            Vector3 currentVelocity = rb.velocity;
            Vector3 targetVelocity = currentVelocity;
            targetVelocity.x = currentPlayerInputHorizontal * speedMovementHorizontalMultiplier;
            float currentSmoothTime = (currentPlayerInputHorizontal != 0f ? smoothVelocityTiming : decelerationSmoothVelocityTiming);
            currentSmoothTime *= (isGrounded ? 1 : 3 );
            rb.velocity = Vector3.SmoothDamp(currentVelocity, targetVelocity, ref horizontalVelocityRef, currentSmoothTime);
        }
    }

    void MovePlayerVertically() {
        if (isJumpAsked && timeBeforBufferingJumpCancel > 0) {
            if (currentPlayerInputVertical < -traversePlatformVerticalAxisThreshold && isGrounded && IsOnTraversablePlatform()) {
                TraversePlatform();
                isJumpAsked = false;
                timeBeforBufferingJumpCancel = 0;
            } else if (isJumpAsked && jumpNumberAvailable > 0 && timeBeforeNextJump <= 0) {
                timeBeforeNextJump = cooldownJump;
                isJumping = true;
                isJumpAsked = false;
                jumpNumberAvailable--;
                timeRemainingBeforeEndOfJump = timeOfJump;
                timeBeforBufferingJumpCancel = 0;
            }
        }
        if (timeRemainingBeforeEndOfJump > 0 && isJumping) {
            Vector2 newVel = rb.velocity;
            newVel.y = jumpForceMultiplier;
            rb.velocity = newVel;
        }
        if (timeRemainingBeforeEndOfJump <= 0 && isJumping) {
            isJumping = false;
            Vector2 newVel = rb.velocity;
            newVel.y = 0;
            rb.velocity = newVel;
        }
        if (rb.velocity.y < -verticalVelocityThreshold)
            rb.gravityScale = initialGravityScale * fallGravityMultiplier;
    }

    void TraversePlatform() {
        Collider2D[] collidersFound = GetGroundedColliders();

        foreach (Collider2D col in collidersFound) {
            if (col.tag == "TraversablePlatform")
                col.gameObject.GetComponent<PlatformCheck>().ChangePlatformState(false);
        }
    }

    void GetPlayerInputs() {
        currentPlayerInputHorizontal = Input.GetAxisRaw("Horizontal");
        if (currentPlayerInputHorizontal != 0)
            lastDirection = (currentPlayerInputHorizontal == 1f ? true : false);
        currentPlayerInputVertical = Input.GetAxisRaw("Vertical");
        if (!isJumpAsked) {
            isJumpAsked = Input.GetButtonDown("Jump");
            if (isJumpAsked)
                timeBeforBufferingJumpCancel = bufferingTime;
        } else if (isJumpAsked && timeBeforBufferingJumpCancel <= 0)
            isJumpAsked = false;
        if (!isDashAsked) {
            isDashAsked = Input.GetButtonDown("Fire1");
            if (isDashAsked)
                timeBeforBufferingDashCancel = bufferingTime;
        } else if (isDashAsked && timeBeforBufferingDashCancel <= 0)
            isDashAsked = false;
    }

    Collider2D[] GetGroundedColliders() {
        Vector2 topLeftArea = new Vector2(groundCheck.position.x - (widthGroundCheck / 2), groundCheck.position.y - (heightGroundCheck / 2));
        Vector2 bottomRightArea = new Vector2(groundCheck.position.x + (widthGroundCheck / 2), groundCheck.position.y + (heightGroundCheck / 2));

        return Physics2D.OverlapAreaAll(topLeftArea, bottomRightArea, mapLayer);
    }

    bool IsOnTraversablePlatform() {
        Collider2D[] collidersFound = GetGroundedColliders();
        
        foreach (Collider2D col in collidersFound)
            if (col.tag == "TraversablePlatform")
                return true;
        return false;
    }

    void GetGroundedStatus() {
        Collider2D[] collidersFound = GetGroundedColliders();
        bool isOnGround = (collidersFound.Length > 0 ? true : false);

        if (isOnGround && !isGrounded && rb.velocity.y == 0) {
            isGrounded = true;
            isJumping = false;
            rb.gravityScale = initialGravityScale ;
            jumpNumberAvailable = maxJumpNumber;
            timeBeforeNextJump = cooldownJump;
        } else if (!isOnGround && isGrounded) {
            isGrounded = false;
        }
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(groundCheck.position, new Vector3(widthGroundCheck, heightGroundCheck, 0));
    }
}
