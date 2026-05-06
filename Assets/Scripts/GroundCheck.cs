using UnityEngine;

public class PlayerGroundCheck : MonoBehaviour
{
    public PlayerMovement player;

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ground") && !player.coyoteTimeStarted && player.GetComponent<Rigidbody>().linearVelocity.y <= 0)
        {
            player.StartCoroutine(player.CoyoteTimeTimer());
        }
    }
}
