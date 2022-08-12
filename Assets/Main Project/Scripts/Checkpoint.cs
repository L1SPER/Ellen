using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    GameManager gameManager;
    Animator animator;
    private void Awake()
    {
        gameManager= FindObjectOfType<GameManager>();
        animator = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            animator.Play("Checkpoint");
            gameManager.WinMenu();
        }
    }
}
