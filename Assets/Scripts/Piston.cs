using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piston : MonoBehaviour
{
    public float moveDistance = 3f;
    public float speed = 5f;

    private Vector3 startPos;
    private Vector3 endPos;

    private bool isOn = false;

    void Start()
    {
        startPos = transform.position;

        // Only move upwards
        endPos = startPos + Vector3.up * moveDistance;
    }

    public void SetActive(bool state)
    {
        isOn = state;
    }

    void Update()
    {
        Vector3 target = isOn ? endPos : startPos;

        transform.position = Vector3.MoveTowards(
            transform.position,
            target,
            speed * Time.deltaTime
        );
    }
}