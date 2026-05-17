using UnityEngine;
using UnityEngine.InputSystem;

public class Rope : MonoBehaviour
{
    [SerializeField]
    public bool bungee;


    public Transform anchorPlayer;
    public float maxRopeLength = 5f;

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


        maxRopeLength = Mathf.Clamp(maxRopeLength, minRopeLength, maxAllowedRopeLength);
    }

    void FixedUpdate()
    {
        float currentDistance = Vector3.Distance(transform.position, anchorPlayer.position);

        if (currentDistance > maxRopeLength)
        {
            Vector3 directionToAnchor = (anchorPlayer.position - transform.position).normalized;

            Vector3 targetPosition = anchorPlayer.position - (directionToAnchor * maxRopeLength);

            rb.MovePosition(targetPosition);

            if (!playerMovement.playerGrounded)
            {
                float velocityAway = Vector3.Dot(rb.linearVelocity, -directionToAnchor);

                if (velocityAway > 0)
                {
                    rb.linearVelocity += directionToAnchor * velocityAway;
                }

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