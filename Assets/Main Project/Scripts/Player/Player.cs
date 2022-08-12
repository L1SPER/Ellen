using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    PlayerMovement playerMovement;
    PlayerAnimationController playerAnimationController;
    Health health;
    PlayerCombat playerCombat;
    private Rigidbody2D rigidbody2D;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        playerMovement = GetComponent<PlayerMovement>();
        playerAnimationController = GetComponent<PlayerAnimationController>();
        playerCombat = GetComponent<PlayerCombat>();
        health = GetComponent<Health>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    // public IEnumerator AttackOrder()
    // {
    //     if(playerCombat.isAttacking)
    //         yield break;
    //     playerCombat.isAttacking=true;
    //     playerCombat.Attack();
    //     yield return new WaitForSeconds(1f);
    //     playerCombat.isAttacking=false;
    //     yield break;
    // }
}
