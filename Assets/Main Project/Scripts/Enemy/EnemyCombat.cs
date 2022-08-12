using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] int attackDamage = 1;
    [SerializeField] float attackRange;
    [SerializeField] GameObject attackPoint;
    [SerializeField] LayerMask playerLayer;

    public bool isAttacking = false;
    public void Attack()
    {
        Collider2D[] hitresults = Physics2D.OverlapCircleAll(attackPoint.transform.position, attackRange,playerLayer);
        if (hitresults == null)
            return;
        foreach(Collider2D hit in hitresults)
        {
            if (hit.GetComponent<IDamageable<int>>() != null)
            {
                hit.GetComponent<IDamageable<int>>().TakeDamage(attackDamage);
                hit.GetComponent<Health>().CheckIfWeDead();
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.transform.position, attackRange);
    }
    public void AttackOrder()
    {
        //if (isAttacking)
            //yield break;
        isAttacking = true;
        Attack();
        //yield return new WaitForSeconds(1f);
        isAttacking = false;
    }
}
