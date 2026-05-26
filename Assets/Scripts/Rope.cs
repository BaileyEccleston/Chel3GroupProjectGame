using UnityEngine;
using UnityEngine.InputSystem;

public class Rope : MonoBehaviour
{
    [SerializeField]
    public bool bungee;


    public Transform anchorPlayer;
    // max distance allowed between the 2 players
    public float maxRopeLength = 5f;


    // variables for altering dynamic rope
    [SerializeField] private float ropeAdjustSpeed = 3f;
    [SerializeField] private float minRopeLength = 1f;
    [SerializeField] private float maxAllowedRopeLength = 15f;

    private Rigidbody rb;
    private PlayerMovement playerMovement;

    private bool increasingRope;
    private bool decreasingRope;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerMovement = GetComponent<PlayerMovement>();
    }
    void Update()
    {
        // alter length of rope when holding triggers
        if (increasingRope && playerMovement.canChangeRopeLength && playerMovement.otherPlayer.GetComponent<PlayerMovement>().canChangeRopeLength)
        {
            Debug.Log("Increase length");
            maxRopeLength += ropeAdjustSpeed * Time.deltaTime;
        }

        if (decreasingRope && playerMovement.canChangeRopeLength && playerMovement.otherPlayer.GetComponent<PlayerMovement>().canChangeRopeLength)
        {
            Debug.Log("Decrease length");
            maxRopeLength -= ropeAdjustSpeed * Time.deltaTime;
        }

        // keep rope length within bounds
        maxRopeLength = Mathf.Clamp(maxRopeLength, minRopeLength, maxAllowedRopeLength);
    }

    void FixedUpdate()
    {
        float currentDistance = Vector3.Distance(transform.position, anchorPlayer.position);
        // stop player from moving past max rope length
        if (currentDistance > maxRopeLength)
        {
            Vector3 directionToAnchor = (anchorPlayer.position - transform.position).normalized;

            Vector3 targetPosition = anchorPlayer.position - (directionToAnchor * maxRopeLength);

            rb.MovePosition(targetPosition);

            // adjust airborne velocity to stop movement away from the rope past the max length (keep player to circle around other player)
            if (!playerMovement.playerGrounded)
            {
                float velocityAway = Vector3.Dot(rb.linearVelocity, -directionToAnchor);

                if (velocityAway > 0)
                {
                    rb.linearVelocity += directionToAnchor * velocityAway;
                }

                // downward force when swinging
                rb.AddForce(Vector3.down * 10f, ForceMode.Acceleration);
            }

        }
    }

    public void OnIncreaseRopeLength(InputAction.CallbackContext context)
    {
        increasingRope = context.ReadValueAsButton();
    }

    public void OnDecreaseRopeLength(InputAction.CallbackContext context)
    {
        decreasingRope = context.ReadValueAsButton();
    }
}