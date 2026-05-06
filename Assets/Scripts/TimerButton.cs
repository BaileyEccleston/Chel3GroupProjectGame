using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerButton : MonoBehaviour
{
    public float pressDepth = 0.2f;
    public float pressSpeed = 10f;

    public Piston piston;

    private Vector3 startPos;
    private Vector3 downPos;

    private bool isPressed = false;

    void Start()
    {
        startPos = transform.localPosition;
        downPos = startPos + Vector3.down * pressDepth;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPressed = true;

            if (piston != null)
            {
                piston.SetActive(true);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            piston.SetActive(false);
        }
    }

    void Update()
    {
        // move button down or up
        if (isPressed)
        {
            transform.localPosition = Vector3.Lerp(
                transform.localPosition,
                downPos,
                pressSpeed * Time.deltaTime
            );
        }
        else
        {
            transform.localPosition = Vector3.Lerp(
                transform.localPosition,
                startPos,
                pressSpeed * Time.deltaTime
            );
        }
    }
}