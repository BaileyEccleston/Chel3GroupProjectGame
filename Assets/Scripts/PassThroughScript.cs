using UnityEngine;
using System.Collections;

public class PassThroughScript : MonoBehaviour
{
    public Collider platformCollider;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            platformCollider.enabled = false;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(TurnColliderBackOn());
        }
    }

    IEnumerator TurnColliderBackOn()
    {
        yield return new WaitForSeconds(0.3f);

        platformCollider.enabled = true;
    }
}