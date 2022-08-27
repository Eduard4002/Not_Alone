using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
public class EnemyUnitBase : MonoBehaviour
{
    public EnemyStats Stats;
    Player player;
     float timer;
     EnemyUI ui;
     public GameManager mainManager;
    Animator anim;
     

    public void Start(){
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        mainManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();
        anim = GetComponent<Animator>();
        timer = Stats.waitTime;
        ui = GetComponentInChildren<EnemyUI>();
        if(ui != null){
            ui.SetMaxHealth(Stats.health);
        }
       
    }

    public virtual void Update(){
        if(mainManager.IsPaused() == false){
            if(timer > 0){
            timer -= Time.deltaTime;
        }
        float dist = Vector3.Distance(player.GetObjectPosition(), transform.position);
        if(dist <= Stats.rangeToFigth){
            if(timer <= 0){
                Attack();
                timer = Stats.waitTime;
            }
             
        }
        if(dist <= Stats.rangeToFollow){
            transform.position = Vector3.MoveTowards(transform.position, player.GetObjectPosition(), Stats.speed * Time.deltaTime);
            PlayRunAnimation();
        }else{
             StopRunAnimation();
        }
        }
        
    }

    public virtual void SetAudioVolume(float volume){}
    public virtual void PlayDeathClip(){}
    public virtual void PlayDamageClip(){}

    public virtual void PlayRunAnimation(){
        anim.SetFloat("speed",0.2f);
    }
    public virtual void StopRunAnimation(){
        anim.SetFloat("speed",0f);
    }

    public virtual void Attack(){
        player.TakeDamage(Stats.damage);
    }
    public void TakeDamage(int dmg){
        Stats.health -= dmg;
        if(ui != null){
            ui.SetHealth(Stats.health);
        }
        
        if(Stats.health <= 0){
            PlayDeathClip();
            mainManager.RemoveEnemy(gameObject);
            Destroy(gameObject,0.1f);
        }else{
            PlayDamageClip();
        }
        
    }
}

[Serializable]

public struct EnemyStats{
    public int health;
    public float speed;
    public int damage;
    public float rangeToFollow;
    public float rangeToFigth;
    public float waitTime;
}

