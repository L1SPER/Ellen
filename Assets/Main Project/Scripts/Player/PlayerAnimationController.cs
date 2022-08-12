using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void PlayCrouchAnim()
    {
        animator.SetBool("isIdleing", false);
        animator.SetBool("isWalking", false);
        animator.SetBool("isRunning", false);
        animator.SetBool("isJumping", false);
        animator.SetBool("isCrouching", true);
        animator.SetBool("isAttacking", false);
        animator.SetBool("isHurting", false);
        animator.SetBool("isDeath", false);
    }

    public void PlayWalkAnim()
    {
        animator.SetBool("isIdleing", false);
        animator.SetBool("isWalking", true);
        animator.SetBool("isRunning", false);
        animator.SetBool("isJumping", false);
        animator.SetBool("isCrouching", false);
        animator.SetBool("isAttacking", false);
        animator.SetBool("isHurting", false);
        animator.SetBool("isDeath", false);
    }

    public void PlayRunAnim()
    {
        animator.SetBool("isIdleing", false);
        animator.SetBool("isWalking", false);
        animator.SetBool("isRunning", true);
        animator.SetBool("isJumping", false);
        animator.SetBool("isCrouching", false);
        animator.SetBool("isAttacking", false);
        animator.SetBool("isHurting", false);
        animator.SetBool("isDeath", false);
    }
    public void PlayJumpAnim()
    {
        animator.SetBool("isJumping", true);
        animator.SetBool("isIdleing", false);
        animator.SetBool("isWalking", false);
        animator.SetBool("isRunning", false);
        animator.SetBool("isCrouching", false);
        animator.SetBool("isAttacking", false);
        animator.SetBool("isHurting", false);
        animator.SetBool("isDeath", false);
    }
    public void PlayIdleAnim()
    {
        animator.SetBool("isIdleing", true);
        animator.SetBool("isWalking", false);
        animator.SetBool("isRunning", false);
        animator.SetBool("isJumping", false);
        animator.SetBool("isCrouching", false);
        animator.SetBool("isAttacking", false);
        animator.SetBool("isHurting", false);
        animator.SetBool("isDeath", false);
    }
    public void PlayAttackAnim()
    {
        animator.SetBool("isIdleing", false);
        animator.SetBool("isWalking", false);
        animator.SetBool("isRunning", false);
        animator.SetBool("isJumping", false);
        animator.SetBool("isCrouching", false);
        animator.SetBool("isHurting", false);
        animator.SetBool("isAttacking", true);
        animator.SetBool("isDeath", false);
    }
    public void PlayHurtAnim()
    {
        animator.SetBool("isHurting", true);
        animator.SetBool("isIdleing", false);
        animator.SetBool("isWalking", false);
        animator.SetBool("isRunning", false);
        animator.SetBool("isJumping", false);
        animator.SetBool("isCrouching", false);
        animator.SetBool("isAttacking", false);
        animator.SetBool("isDeath", false);
    }
    public void PlayDeathAnim()
    {
        animator.SetBool("isDeath", true);
        animator.SetBool("isHurting", false);
        animator.SetBool("isIdleing", false);
        animator.SetBool("isWalking", false);
        animator.SetBool("isRunning", false);
        animator.SetBool("isJumping", false);
        animator.SetBool("isCrouching", false);
        animator.SetBool("isAttacking", false);
    }
    public void StopHurtingAnim()
    {
        animator.SetBool("isHurting", false);
    }
    public void StopAttackingAnim()
    {
        animator.SetBool("isAttacking", false);
    }
    public void StopCrouchingAnim()
    {
        animator.SetBool("isCrouching", false);
    }
    public void StopIdleingAnim()
    {
        animator.SetBool("isIdleing", false);
    }
    public void StopWalkingAnim()
    {
        animator.SetBool("isWalking", false);
    }
    public void StopRunningAnim()
    {
        animator.SetBool("isRunning", false);
    }
    public void StopJumpingAnim()
    {
        animator.SetBool("isJumping", false);
    }
    public void StopDeathAnim()
    {
        animator.SetBool("isDeath", false);
    }
}
