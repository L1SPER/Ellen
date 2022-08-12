using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField]
    int attackDamage = 1;
    [SerializeField]
    float attackRange;
    [SerializeField]
    GameObject attackPoint;
    [SerializeField]
    LayerMask targetLayer;

    public bool isAttacking=false;
    public void Attack()
    {
        Collider2D[] hitResults = Physics2D.OverlapCircleAll(attackPoint.transform.position, attackRange, targetLayer);
        if( hitResults==null)
            return;
     
        foreach(Collider2D hit in hitResults)
        {
            if(hit.GetComponent<IDamageable<int>>()!=null)
            {
                hit.GetComponent<IDamageable<int>>().TakeDamage(attackDamage);
                hit.GetComponent<Health>().CheckIfWeDead();
            }
        }
    }

    private void OnDrawGizmosSelected() {
        if(attackPoint==null)
            return;

        Gizmos.DrawWireSphere(attackPoint.transform.position,attackRange);    
    }
    public IEnumerator AttackOrder()
    {
        if(isAttacking)
            yield break;
        isAttacking=true;
        Attack();
        isAttacking=false;
        yield return new WaitForSeconds(.2f);
        yield break;
    }
}
