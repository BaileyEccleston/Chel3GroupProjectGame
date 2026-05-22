using UnityEngine;
using System.Collections;
public class AnimateAudience : MonoBehaviour
{
    public float jumpHeight = 0.5f;
    public float jumpDuration = 0.4f;

    public float minWait = 0.5f;
    public float maxWait = 3f;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.localPosition;
        StartCoroutine(JumpLoop());
    }

    IEnumerator JumpLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minWait, maxWait));

            float timer = 0f;

            while (timer < jumpDuration)
            {
                timer += Time.deltaTime;

                float t = timer / jumpDuration;

                // Creates an arc: 0 -> 1 -> 0
                float height = Mathf.Sin(t * Mathf.PI) * jumpHeight;

                transform.localPosition = startPos + Vector3.up * height;

                yield return null;
            }

            transform.localPosition = startPos;
        }
    }
}
