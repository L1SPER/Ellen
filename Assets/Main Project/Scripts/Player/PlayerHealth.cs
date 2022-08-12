using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   
public class PlayerHealth : Health, IDamageable<int>
{
    public int maxHealth=4;
    public int currentHealth=4;
    public HealthBar healthBar;
    public bool untouchable=false;
    public bool isHurted = false;
    private PlayerAnimationController playerAnimationController;
    private GameManager gameManager;
    private void Awake() {
        gameManager=FindObjectOfType<GameManager>();
        playerAnimationController=GetComponent<PlayerAnimationController>();
        healthBar = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<HealthBar>();
    }

    /// <summary>
    /// Shows player's current health in the health bar
    /// </summary>
    private void Start() {
        healthBar.SetMaxHealth(maxHealth);
        healthBar.SetHealth(currentHealth);
    }
    /// <summary>
    /// Player takes damage
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(int damage)
    {
        if(!untouchable)
            currentHealth-=damage;
        StartCoroutine(UntouchableActive());
        CheckIfWeDead();
        healthBar.SetHealth(currentHealth);
    }
    /// <summary>
    /// Checks the player if player's health is lower than 1 hp
    /// </summary>
    public override void CheckIfWeDead()
    {
        base.CheckIfWeDead();
        if(currentHealth<=0)
        {
            currentHealth=0;
            StartCoroutine(PlayDeathAnimWithSeconds());
        }
    }
    public IEnumerator PlayDeathAnimWithSeconds()
    {
        playerAnimationController.PlayDeathAnim();
        yield return new WaitForSeconds(.5f);
        gameManager.Pause();
        //StartCoroutine(gameManager.PauseMenuWaitSeconds(1f));
    }
    public IEnumerator  UntouchableActive()
    {
        if (untouchable)
            yield break;
        untouchable=true;
        playerAnimationController.PlayHurtAnim();
        yield return new WaitForSeconds(2f);
        playerAnimationController.StopHurtingAnim();
        untouchable = false;
    }
    /// <summary>
    /// If player dies,this func resets player's health
    /// </summary>
    public void ResetHealth()
    {
        currentHealth = maxHealth;
        healthBar.SetHealth(currentHealth);
    }
}
