using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCollider : MonoBehaviour
{
    public GameManager gameManager;

    public void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")){
            gameManager.HasWon();
        }
    }
}
