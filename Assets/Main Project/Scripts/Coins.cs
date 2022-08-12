using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour
{
    GameManager gameManager;
    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            AddCoins();
            gameManager.AddScore();
            Destroy(this.gameObject);
        }
    }

    private void AddCoins()
    {
        gameManager.collectedCoins.Add(this.transform.position); 
    }
}
