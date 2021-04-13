using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Game.Controller.PlayerCharacterController controller;
    [SerializeField] SpriteRenderer renderer;
    void Update()
    {
        animator.SetFloat("X", Input.GetAxisRaw("Horizontal"));
        if (Input.GetAxisRaw("Horizontal") < 0f)
            renderer.flipX = true;
        if (Input.GetAxisRaw("Horizontal") > 0f)
            renderer.flipX = false;

        animator.SetBool("isGrounded", controller.isGrounded);
    }
}
