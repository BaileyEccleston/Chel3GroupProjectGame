using UnityEngine;
using System.Collections;

public class FallingPlatformScript : MonoBehaviour
{
    public Rigidbody rb;

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
        if (collision.gameObject.CompareTag("Player") && !falling)
        {
            StartCoroutine(Fall());
        }
    }

    IEnumerator Fall()
    {
        falling = true;

        Vector3 pos = transform.position;

        // shake
        for (float i = 0; i < 0.4f; i += Time.deltaTime)
        {
            transform.position = pos + Random.insideUnitSphere * 0.03f;
            yield return null;
        }

        transform.position = pos;
        yield return new WaitForSeconds(2f);
        // fall
        GetComponent<Collider>().enabled = false;
        rb.isKinematic = false;

        yield return new WaitForSeconds(3f);

        // reset
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        rb.isKinematic = true;

        transform.position = startPos;
        transform.rotation = startRot;

        GetComponent<Collider>().enabled = true;

        falling = false;
    }
}