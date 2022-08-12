using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keys : MonoBehaviour
{
    GameManager gameManager;
    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            AddKeys();
            gameManager.trophyCounter++;
            Destroy(this.gameObject);
        }
    }
    private void AddKeys()
    {
        gameManager.collectedKeys.Add(this.transform.position);
    }
}
