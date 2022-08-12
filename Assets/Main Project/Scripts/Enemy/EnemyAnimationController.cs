using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    private Animator animator;
    private void Awake()
    {
        animator= GetComponent<Animator>();
    }

    //Play Animations
    public void PlayIdleAnim()
    {
        animator.SetBool("isIdleing",true);
        animator.SetBool("isWalking", false);
        animator.SetBool("isRunning", false);
        animator.SetBool("isAttacking", false);
    }
    public void PlayWalkingAnim()
    {
        animator.SetBool("isIdleing", false);
        animator.SetBool("isWalking", true);
        animator.SetBool("isRunning", false);
        animator.SetBool("isAttacking", false);
    }
    public void PlayRunningAnim()
    {
        animator.SetBool("isIdleing", false);
        animator.SetBool("isWalking", false);
        animator.SetBool("isRunning", true);
        animator.SetBool("isAttacking", false);
    }
    public void PlayAttackAnim()
    {
        animator.SetBool("isIdleing", false);
        animator.SetBool("isWalking", false);
        animator.SetBool("isRunning", false);
        animator.SetBool("isAttacking", true);
    }
    public void PlayDeathAnim()
    {
        animator.SetBool("isDeath", true);
    }

    //Stop Animations
    public void StopIdleAnim()
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
    public void StopAttackAnim()
    {
        animator.SetBool("isAttacking", false);
    }
    public void StopDeathAnim()
    {
        animator.SetBool("isDeath", false);
    }

}
