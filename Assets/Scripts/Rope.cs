using UnityEngine;

public class Rope : MonoBehaviour
{
    public Transform anchorPlayer;
    public float maxRopeLength = 5f;

    private Rigidbody rb;
    private PlayerMovement playerMovement;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerMovement = GetComponent<PlayerMovement>();
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
}