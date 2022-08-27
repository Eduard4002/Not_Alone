using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thrower : EnemyUnitBase
{
    public AudioClip damageSound;
    public AudioClip deathSound;

    public AudioSource source;
    public override void PlayDeathClip()
    {
        source.clip = deathSound;
        source.Play();
    }
    public override void PlayDamageClip()
    {
        source.clip = damageSound;
        source.Play();
    }
    public override void SetAudioVolume(float volume)
    {
        source.volume = volume;
    }
    
    public GameObject objectToThrow;

   public float weaponSpeed;
   public float timeToDestroyBall;
   public float offsetX;
    public override void Attack()
    {
       GameObject go = Instantiate(objectToThrow, new Vector2(transform.position.x+offsetX,transform.position.y), Quaternion.identity);
       ball ball = go.GetComponent<ball>();
       ball.speed = weaponSpeed;    
       ball.damage = Stats.damage;
       ball.timer = timeToDestroyBall;
    }

    public override void PlayRunAnimation()
    {

    }
    public override void StopRunAnimation()
    {
        
    }
}
