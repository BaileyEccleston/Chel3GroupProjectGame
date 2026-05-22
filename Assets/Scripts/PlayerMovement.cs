using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine.UI;

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

    public Rigidbody rb;
    public float moveInput;

    float airAcceleration = 30f;

    [Header("Wall Check & Stamina")]
    public bool playerWalled = false;
    public LayerMask wallLayer;
    PlayerStamina playerStamina;


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

    StarManager starManager;

    [Header("Feedback")]
    [SerializeField]
    private AudioSource Audio;
    [SerializeField]
    private AudioClip Anchorclip;
    [SerializeField]
    private AudioClip Jumpclip;
    [SerializeField]
    private ParticleSystem landDust;
    bool wasGrounded;



    Checkpoints checkpoint;



    void Start()
    {
        starManager = FindFirstObjectByType<StarManager>();
        lives = FindFirstObjectByType<PlayerLives>();
        checkpoint = FindFirstObjectByType<Checkpoints>();
        rb = GetComponent<Rigidbody>();
        currentBounceAmount = startingBounceAmount;
        playerStamina = GetComponent<PlayerStamina>();

    }


    void FixedUpdate()
    {
        if (jumpOnWall && playerGrounded)
        {
            jumpOnWall = false;
            playerStamina.ResetStamina();
        }
        wasGrounded = playerGrounded;
        playerGrounded = Physics.CheckSphere(groundCheck.position, checkRadius, groundLayer);
        playerWalled = Physics.CheckSphere(groundCheck.position, checkRadius, wallLayer);
        if(!wasGrounded && playerGrounded)
        {
            landDust.Play();
        }
        if (playerGrounded)
        {
            timesBounced = 0;
            coyoteTimeStarted = false;
            hasBounced = false; 
        }



        if (playerStamina.currentStamina <= 0 && !jumpOnWall)
        {
            playerStamina.ResetStamina();
            anchored = false;
            rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
            rb.isKinematic = false;
        }



        float distance = Vector3.Distance(transform.position, otherPlayer.position);
        ropeTight = distance >= rope.maxRopeLength * ropeTensionThreshold;

        if (!launched)
        {
            if (playerGrounded)
            {
                if (!rb.isKinematic)
                {
                    rb.linearVelocity = new Vector3(moveInput * speed, rb.linearVelocity.y, 0);
                }
            }
            else
            {
                if (!ropeTight)
                {
                    hasBounced = false; 
                    if (!rb.isKinematic)
                    {
                        rb.linearVelocity = new Vector3(moveInput * speed, rb.linearVelocity.y, 0);
                    }
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
                Debug.Log("Launched");
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
        if (moveInput != 0)
        {
            Vector3 scale = transform.localScale;
            if (moveInput < 0)
            {
                scale.x = -1;
            }
            else if (moveInput > 0)
            {
                scale.x = 1;
            }
            transform.localScale = scale;
        }


  

    }

    bool jumpOnWall = false;
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (playerWalled && playerStamina.currentStamina > 0)
            {
                jumpOnWall = true;
                playerStamina.StopAllCoroutines();
                UseJumpStamina();
                playerStamina.StartCoroutine(playerStamina.ToggleStamina());
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, 0);
                coyoteTimeStarted = false;
                if (!anchored)
                {
                    Audio.clip = Jumpclip;
                    Audio.Play();
                }
            }
            if (playerGrounded || coyoteTimeStarted)
            {

                rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, 0);
                coyoteTimeStarted = false;
                if (!anchored)
                {
                    Audio.clip = Jumpclip;
                    Audio.Play();
                }
            }
        }

        if (context.canceled && rb.linearVelocity.y > 0)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f, 0);
        }
    }

    public void OnAnchor(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (!anchored && playerGrounded && !playerWalled)
            {
                Audio.clip = Anchorclip;
                Audio.Play();
                anchored = true;
                rb.constraints = RigidbodyConstraints.FreezeAll;
                rb.isKinematic = true;
            }
            else if (!anchored && !playerGrounded && playerWalled)
            {
                playerStamina.StopAllCoroutines();
                playerStamina.StartCoroutine(playerStamina.ToggleStamina());
                anchored = true;
                rb.isKinematic = true;
                StartCoroutine(UseStamina());
            }
        }

        if (context.canceled)
        {
            anchored = false;
            rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
            rb.isKinematic = false;
        }
    }

    public IEnumerator UseStamina()
    {
        if (playerStamina.currentStamina > 0)
        {
            yield return new WaitForSeconds(1);
            playerStamina.LoseStamina(playerStamina.staminaPerSecond);
            if (anchored)
            {
                StartCoroutine(UseStamina());
            }
            else
            {
                playerStamina.ResetStamina();
            }
        }
    }


    public void UseJumpStamina()
    {
        if (playerStamina.currentStamina > 0)
        {
            playerStamina.LoseStamina(playerStamina.staminaPerSecond * 4);
        }
    }

    public bool canChangeRopeLength = false;

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
        else if (other.CompareTag("DynamicRope"))
        {
            canChangeRopeLength = true;
        }
        else if (other.CompareTag("Checkpoint"))
        {
            string numberText = other.gameObject.name;
            int newNum;

            if (int.TryParse(numberText, out newNum))
            {
                if (newNum > checkpoint.currentCheckpoint)
                {
                    checkpoint.currentCheckpoint = newNum;
                    Debug.Log("Updated to " + checkpoint.currentCheckpoint);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("DynamicRope"))
        {
            canChangeRopeLength = false;
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

    public bool respawnRunning = false;
    PlayerLives lives;
    public IEnumerator Respawn()
    {
        respawnRunning = true;
        if (otherPlayer.GetComponent<PlayerMovement>().respawnRunning)
        {
            lives.DecreaseLives();
            otherPlayer.GetComponent<PlayerMovement>().StopAllCoroutines();
            RestartFromCheckpoint();
            otherPlayer.GetComponent<PlayerMovement>().RestartFromCheckpoint();
        }
        else
        {
            lives.DecreaseLives();
            yield return new WaitForSeconds(5f);
            respawnRunning = false;
            Vector3 respawnPoint = new Vector3(otherPlayer.transform.position.x, otherPlayer.transform.position.y + 2, otherPlayer.transform.position.z);
            transform.position = respawnPoint;
        }

    }

    public void RestartFromCheckpoint()
    {
        Vector3 checkpointPos = checkpoint.checkpoints[checkpoint.currentCheckpoint - 1].transform.position;

        if (gameObject.name.Contains("1"))
        {
            transform.position = checkpointPos + Vector3.left * 0.5f;
        }
        else if (gameObject.name.Contains("2"))
        {
            transform.position = checkpointPos + Vector3.right * 0.5f;
        }
        else
        {
            transform.position = checkpointPos;
        }
    }
}