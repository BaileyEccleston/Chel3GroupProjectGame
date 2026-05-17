using UnityEngine;
using System.Collections;

public class FallingPlatformScript : MonoBehaviour
{
    public Rigidbody rb;

    public float shakeTime = 0.4f;
    public float respawnTime = 3f;

    Vector3 startPos;
    Quaternion startRot;

    bool falling;

    void Start()
    {
        startPos = transform.position;
        startRot = transform.rotation;

        rb.isKinematic = true;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!falling)
            {
                StartCoroutine(Fall());
            }
        }
    }

    IEnumerator Fall()
    {
        falling = true;

        Vector3 pos = transform.position;

        // shake
        for (float i = 0; i < shakeTime; i += Time.deltaTime)
        {
            transform.position = pos + Random.insideUnitSphere * 0.03f;

            yield return null;
        }

        transform.position = pos;

        // fall
        GetComponent<Collider>().enabled = false;

        rb.isKinematic = false;

        // wait before respawn
        yield return new WaitForSeconds(respawnTime);

        // reset platform
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        rb.isKinematic = true;

        transform.position = startPos;
        transform.rotation = startRot;

        GetComponent<Collider>().enabled = true;

        falling = false;
    }
}