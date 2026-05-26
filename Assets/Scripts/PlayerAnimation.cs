using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerAnimation : MonoBehaviour
{

    public PlayerMovement player;
    public Animator playerAnimator;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // play idle when not moving 
        if (player.playerWalled&&player.playerGrounded && Mathf.Abs(player.rb.linearVelocity.x) > 0.1f && Mathf.Abs(player.rb.linearVelocity.y) > 0.1f)
        {
            playerAnimator.Play("Idle");
        }


    }

    public void OnMove(InputAction.CallbackContext context)
    {
        playerAnimator.SetFloat("IsRunning", 1);

        if (context.canceled)
        {
            playerAnimator.SetFloat("IsRunning", 0);

        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
        {

            playerAnimator.SetBool("IsJumping", true);
        }
        if (context.canceled)
        {
            playerAnimator.SetTrigger("StopJumping");
            playerAnimator.SetBool("IsJumping", false);
        }
    }

    public void OnAnchor(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (player.playerWalled && player.anchored)
            {
                Debug.Log("walled");
                playerAnimator.SetBool("IsWalled", true);
            }
        }

        if (context.canceled)
        {
            playerAnimator.SetTrigger("StopWalled");
            playerAnimator.SetBool("IsWalled", false);
        }
    }
}
