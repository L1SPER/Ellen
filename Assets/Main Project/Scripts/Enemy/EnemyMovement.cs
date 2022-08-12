using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] GameObject Player;
    EnemyCollicisionCheck enemyCollicisionCheck;
    Transform transform;
    EnemyAnimationController enemyAnimationController;
    [SerializeField] float walkingSpeed = 250f;
    [SerializeField] float runningSpeed = 500f;
    [SerializeField] float maxDistance =10f;//The distance required for the enemy to run to the player
    [SerializeField] float minDistance = 2f;//The distance required for the enemy to attack to the player
    [SerializeField] float catchingSpeed = 10f;
    [SerializeField] float distance=0f;
    bool lookingRight=true;

    private void Awake()
    {
        enemyAnimationController = GetComponent<EnemyAnimationController>();
        rb = GetComponent<Rigidbody2D>();
        enemyCollicisionCheck=FindObjectOfType<EnemyCollicisionCheck>();
        transform=GetComponent<Transform>();
    }
    private void Update()
    {
        Movement();
        FlipFace();
    }
    private void Movement()
    {
        distance = transform.position.x - Player.transform.position.x;
        if (maxDistance < Mathf.Abs(distance)&&lookingRight)//Walking
        {
            rb.velocity = new Vector2(walkingSpeed * Time.deltaTime, 0);
            enemyAnimationController.PlayWalkingAnim();
            StartCoroutine(PlayAudio("PinkEnemyWalk", 0f)); 
        }
        else if(maxDistance < Mathf.Abs(distance) && !lookingRight)
        {
            rb.velocity = new Vector2(-walkingSpeed * Time.deltaTime, 0);
            enemyAnimationController.PlayWalkingAnim();
            StartCoroutine(PlayAudio("PinkEnemyWalk",0f));

        }
        else
         {
            if (minDistance > Mathf.Abs(distance)) //Attack 
            {
                enemyAnimationController.PlayAttackAnim();
                StartCoroutine(PlayAudio("PinkEnemyAttack",2f));
                rb.velocity = new Vector2(0, 0);
            }
            else //Catch the player
            {
                if (distance<0&&!lookingRight)//If enemy is looking right, run right
                {
                    Vector3 flip = transform.localScale;
                    flip.x *= -1;
                    transform.localScale = flip;
                    lookingRight = !lookingRight;
                    rb.velocity = new Vector2(runningSpeed * Time.deltaTime, 0);
                }
                else if(distance>0&&lookingRight) //If enemy is looking left, run left
                {
                    Vector3 flip = transform.localScale;
                    flip.x *= -1;
                    transform.localScale = flip;
                    lookingRight = !lookingRight;
                    rb.velocity = new Vector2(-runningSpeed * Time.deltaTime, 0);
                }
                transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, catchingSpeed*Time.deltaTime);
                enemyAnimationController.PlayRunningAnim();
            }
        }
    }
    private IEnumerator PlayAudio(string audioName ,float time)
    {
        FindObjectOfType<AudioManager>().Play(audioName);
        yield return new WaitForSeconds(time);
    }
    private void FlipFace()
    {
        if (enemyCollicisionCheck.isColliding)//When enemy hit something, change direction and velocity 
        {
            Debug.Log("Flip funct");
            enemyCollicisionCheck.isColliding = false;
            Vector3 flip = transform.localScale;
            flip.x *= -1;
            transform.localScale = flip;
            lookingRight = !lookingRight;
            rb.velocity = new Vector2(-rb.velocity.x, 0);
        }
        else
        {
            return;
        }
    }
}
