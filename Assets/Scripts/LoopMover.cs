using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopMover : MonoBehaviour
{
    public float speed = 3f;

    public float left = -5f;
    public float right = 5f;
    public float front = 0f;
    public float back = 5f;

    private Vector3[] points;
    private int current = 0;

    void Start()
    {
        Vector3 startPos = transform.position;

        // make points
        points = new Vector3[4];

        points[0] = startPos + new Vector3(left, 0, front);
        points[1] = startPos + new Vector3(left, 0, back);
        points[2] = startPos + new Vector3(right, 0, back);
        points[3] = startPos + new Vector3(right, 0, front);
    }

    void Update()
    {
        // get where we are going
        Vector3 target = points[current];

        // move there
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        // if we reached it
        if (Vector3.Distance(transform.position, target) < 0.05f)
        {
            current++;

            // loop back to start
            if (current >= points.Length)
            {
                current = 0;
            }
        }
    }

}
