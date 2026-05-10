using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerButton : MonoBehaviour
{
    [Header("Animation")]
    [SerializeField]
    private Animation anim;

    [SerializeField]
    private AnimationClip ButtonUp;

    [SerializeField]
    private AnimationClip ButtonDown;

    [Header("Platform")]
    public Piston piston;

    [Header("Button Type")]
    [SerializeField]
    private bool useCooldown = false;

    [SerializeField]
    private float cooldownTime = 3f;

    private Coroutine cooldownRoutine;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // cancel cooldown if player steps back on
            if (cooldownRoutine != null)
            {
                StopCoroutine(cooldownRoutine);
            }

            // play down animation
            if (ButtonDown != null)
            {
                anim.clip = ButtonDown;
                anim.Play();
            }

            piston.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (useCooldown)
            {
                cooldownRoutine = StartCoroutine(Cooldown());
            }
            else
            {
                TurnOff();
            }
        }
    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(cooldownTime);

        TurnOff();
    }

    void TurnOff()
    {
        // play up animation
        if (ButtonUp != null)
        {
            anim.clip = ButtonUp;
            anim.Play();
        }

        piston.SetActive(false);
    }
}