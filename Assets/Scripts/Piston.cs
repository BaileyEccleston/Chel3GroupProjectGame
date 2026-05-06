using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piston : MonoBehaviour
{
    public float distance = 3f;
    public float speed = 5f;

    public Vector3 direction = Vector3.up; // change in inspector

    private Vector3 startPos;
    private Vector3 endPos;

    private bool isOn = false;

    void Start()
    {
        startPos = transform.position;
        endPos = startPos + direction.normalized * distance;
    }

    public void SetActive(bool state)
    {
        isOn = state;
    }

    void Update()
    {
        Vector3 target;

        if (isOn)
        {
            target = endPos;
        }
        else
        {
            target = startPos;
        }

        transform.position = Vector3.MoveTowards(
            transform.position,
            target,
            speed * Time.deltaTime
        );
    }
}