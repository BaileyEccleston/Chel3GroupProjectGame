using NUnit.Framework.Constraints;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Experimental.GraphView.GraphView;
using System.Collections;

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

    void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    void FixedUpdate()
    {
        playerGrounded = Physics.CheckSphere(groundCheck.position, checkRadius, groundLayer);

        if (playerGrounded)
        {
            coyoteTimeStarted = false;
        }

        float distance = Vector3.Distance(transform.position, otherPlayer.position);
        bool ropeTight = distance >= rope.maxRopeLength * ropeTensionThreshold;

        if (playerGrounded)
        {
            rb.linearVelocity = new Vector3(moveInput * speed, rb.linearVelocity.y, 0);
        }
        else
        {
            
            if (!ropeTight)
            {
                rb.linearVelocity = new Vector3(moveInput * speed, rb.linearVelocity.y, 0);
            }
            else
            {
                rb.AddForce(Vector3.right * moveInput * airAcceleration, ForceMode.Acceleration);

                //float maxAirSpeed = speed * 1.5f;
                float maxAirSpeed = speed * 3;

                if (Mathf.Abs(rb.linearVelocity.x) > maxAirSpeed)
                {
                    rb.linearVelocity = new Vector3(Mathf.Sign(rb.linearVelocity.x) * maxAirSpeed, rb.linearVelocity.y, 0);
                }
            }
            


            /*
            rb.AddForce(Vector3.right * moveInput * airAcceleration, ForceMode.Acceleration);

            //float maxAirSpeed = speed * 1.5f;
            float maxAirSpeed = speed * 3;

            if (Mathf.Abs(rb.linearVelocity.x) > maxAirSpeed)
            {
                rb.linearVelocity = new Vector3(Mathf.Sign(rb.linearVelocity.x) * maxAirSpeed, rb.linearVelocity.y, 0);
            }*/
        }
       // Debug.Log(playerGrounded);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<float>();
       // Debug.Log("moving");
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

        if (context.canceled)
        {
            if (rb.linearVelocity.y > 0)
            {
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f ,0);
            }
        }
        Debug.Log("Jump");
    }

    public void OnAnchor(InputAction.CallbackContext context)
    {
        if (!anchored)
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

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, checkRadius);
    }

    public StarManager starManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "StarCollectable")
        {
            Destroy(other.gameObject);
            starManager.starCollectableCollected = true;
            starManager.starsEarned++;
        }
        else if (other.gameObject.tag == "Collectable")
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




}