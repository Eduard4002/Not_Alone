using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blows : EnemyUnitBase
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

    public override void Attack()
    {
        base.Attack();
        source.clip = deathSound;
        source.Play();
        mainManager.RemoveEnemy(gameObject);
        Destroy(gameObject);
    }
}
