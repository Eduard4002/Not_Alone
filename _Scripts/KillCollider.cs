using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillCollider : MonoBehaviour
{
    public GameManager mainManager;
     public void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")){
            mainManager.HasLost();
        }
    }
}
