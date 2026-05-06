using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public Vector3 direction = Vector3.right;
    public float force = 10f;

    void OnTriggerStay(Collider other)
    {
        // check if it's the player
        if (other.CompareTag("Player"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();

            // if it has a rigidbody
            if (rb != null)
            {
                Vector3 push = direction.normalized * force;

                rb.AddForce(push, ForceMode.Acceleration);
            }
        }
    }
}
