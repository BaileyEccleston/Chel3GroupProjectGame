using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    public Vector3 direction = Vector3.right;
    public float force;

    void OnTriggerStay(Collider other)
    {
        // check if it's the player
        if (other.CompareTag("feet"))
        {
            // Get the Rigidbody from the parent (the Player)
            Rigidbody rb = other.GetComponentInParent<Rigidbody>();

            if (rb != null)
            {
                Vector3 movement = direction.normalized * force * Time.fixedDeltaTime;
                rb.MovePosition(rb.position + movement*Time.fixedDeltaTime);
            }
        }
    }
}
