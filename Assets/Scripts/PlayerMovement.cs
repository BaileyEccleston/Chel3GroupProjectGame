using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using Unity.VisualScripting;

public class PlayerMovement : MonoBehaviour
{
    [Header("Ground Check")]
    public Transform groundCheck;
    public float checkRadius = 0.2f;
    public LayerMask groundLayer;

    public bool playerGrounded = false;

    public Transform otherPlayer;
    public bool anchored = false;

    public float speed = 5f;
    public float jumpForce = 15f;

    private Rigidbody rb;
    public float moveInput;

    float airAcceleration = 30f;

    [Header("Rope")]
    public Rope rope;
    public float ropeTensionThreshold = 0.8f;

    [Header("CoyoteTime")]
    public bool coyoteTimeStarted = false;
    public float coyoteTimeLength = 0.2f;

    bool ropeTight;
    bool launched = false;
    bool hasBounced = false;
    private float startingBounceAmount = 18f;
    private float currentBounceAmount;
    private float timesBounced = 0;

    public StarManager starManager;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentBounceAmount = startingBounceAmount;
    }

    void FixedUpdate()
    {
        playerGrounded = Physics.CheckSphere(groundCheck.position, checkRadius, groundLayer);

        if (playerGrounded)
        {
            timesBounced = 0;
            coyoteTimeStarted = false;
            hasBounced = false; 
        }

        

        float distance = Vector3.Distance(transform.position, otherPlayer.position);
        ropeTight = distance >= rope.maxRopeLength * ropeTensionThreshold;

        if (!launched)
        {
            if (playerGrounded)
            {
                rb.linearVelocity = new Vector3(moveInput * speed, rb.linearVelocity.y, 0);
            }
            else
            {
                if (!ropeTight)
                {
                    hasBounced = false; 
                    rb.linearVelocity = new Vector3(moveInput * speed, rb.linearVelocity.y, 0);
                }
                else
                {
                    if (rope.bungee && !hasBounced && transform.position.y < otherPlayer.position.y - 5)
                    {
                        hasBounced = true;
                        launched = true;

                        Vector3 directionToLaunch = (otherPlayer.position - transform.position).normalized;
                        rb.linearVelocity = Vector3.zero;


                        // change amount bounced based on how many times you have bounced previously

                        switch (timesBounced)
                        {
                            case 0:
                                currentBounceAmount = startingBounceAmount;
                                break;
                            case 1:
                                currentBounceAmount = startingBounceAmount * 0.9f;
                                break;
                            case 2:
                                currentBounceAmount = startingBounceAmount * 0.8f;
                                break;
                            case 3:
                                currentBounceAmount = startingBounceAmount * 0.7f;
                                break;
                            case 4:
                                currentBounceAmount = startingBounceAmount * 0.6f;
                                break;
                            case 5:
                                currentBounceAmount = startingBounceAmount * 0.4f;
                                break;
                            case 6:
                                currentBounceAmount = startingBounceAmount * 0.2f;
                                break;
                        }
                        if (timesBounced < 7)
                        {
                            rb.AddForce(directionToLaunch * currentBounceAmount, ForceMode.VelocityChange);
                            timesBounced++;
                        }

                        StartCoroutine(ResetLaunch());
                    }
                    else if (!rope.bungee || (rope.bungee && hasBounced))
                    {
                        rb.AddForce(Vector3.right * moveInput * airAcceleration, ForceMode.Acceleration);

                        float maxAirSpeed = speed * 3;
                        if (Mathf.Abs(rb.linearVelocity.x) > maxAirSpeed)
                        {
                            rb.linearVelocity = new Vector3(Mathf.Sign(rb.linearVelocity.x) * maxAirSpeed, rb.linearVelocity.y, 0);
                        }
                    }
                }
            }
        }
    }

    public void OnBungee(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (rope.bungee && ropeTight && !anchored && playerGrounded && otherPlayer.GetComponent<PlayerMovement>().playerGrounded)
            {
                launched = true;
                Vector3 targetPosition = otherPlayer.position + Vector3.up * 3f;
                Vector3 directionToLaunch = (targetPosition - transform.position).normalized;
                rb.linearVelocity = directionToLaunch * 20f;

                StartCoroutine(ResetLaunch());
            }
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<float>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (playerGrounded || coyoteTimeStarted)
            {
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, 0);
                coyoteTimeStarted = false;
            }
        }

        if (context.canceled && rb.linearVelocity.y > 0)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f, 0);
        }
    }

    public void OnAnchor(InputAction.CallbackContext context)
    {
        if (!anchored && playerGrounded)
        {
            anchored = true;
            rb.constraints = RigidbodyConstraints.FreezeAll;
            rb.isKinematic = true;
        }
        else
        {
            anchored = false;
            rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
            rb.isKinematic = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("StarCollectable"))
        {
            Destroy(other.gameObject);
            starManager.starCollectableCollected = true;
            starManager.starsEarned++;
        }
        else if (other.CompareTag("Collectable"))
        {
            Destroy(other.gameObject);
            starManager.collectablesCollected++;
        }
    }

    public IEnumerator CoyoteTimeTimer()
    {
        coyoteTimeStarted = true;
        yield return new WaitForSeconds(coyoteTimeLength);
        coyoteTimeStarted = false;
    }

    IEnumerator ResetLaunch()
    {
        yield return new WaitForSeconds(0.7f);
        launched = false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, checkRadius);
    }
}