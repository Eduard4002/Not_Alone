using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ball : MonoBehaviour
{
    Player player;
    public float speed;
    public int damage;
    public float timer;
    GameManager mainManager;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        mainManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(timer - Time.deltaTime > 0){
            timer -= Time.deltaTime;
        }else{
            Destroy(gameObject);
        }
        if(!mainManager.IsPaused()){
            transform.position = Vector2.MoveTowards(transform.position,player.GetObjectPosition(),speed*Time.deltaTime);
            
        }
         
    }

     void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Player")){
            player.TakeDamage(damage);
            Destroy(gameObject);
        }
        
    }
}
