using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health, IDamageable<int>
{
    [SerializeField]
    int maxHealth;
    [SerializeField]
    int currentHealth;
    /// <summary>
    /// Makes enemy's current health equal to maximum health
    /// </summary>
    private void Start()
    {
        currentHealth = maxHealth;
    }
    /// <summary>
    /// Enemy takes damage
    /// </summary>
    /// <param name="damage"></param>
    public  void TakeDamage(int damage)
    {
        currentHealth -= damage;
        CheckIfWeDead();
    }
    /// <summary>
    /// Checks the enemy if enemy health is lower than 1 hp
    /// </summary>
    public override void CheckIfWeDead()
    {
        base.CheckIfWeDead();
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            FindObjectOfType<AudioManager>().Play("PinkEnemyDie");

            Destroy(gameObject);
        }
    }
}
