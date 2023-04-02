using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] private float health = 100;

    private void GetHurt(float damage) {
        health -= damage;
        if (health <= 0)
        {
            Die(ParticleManager.Instance.explosion);
        }
    }

    public void Die(ParticleSystem particles) {
        Destroy(gameObject);

        AudioManager.Instance.PlayRandomSound(AudioManager.Instance.explosions);
        ParticleManager.Instance.Play(particles, transform.position, Quaternion.identity);
        FollowCamera.Instance.ScreenShake(0.2f, 0.2f);
        FollowCamera.Instance.Hitstop(0.1f);
    }

    void OnTriggerEnter2D(Collider2D trigger) {
        if (trigger.CompareTag("Bullet"))
        {
            Destroy(trigger.gameObject);
            GetHurt(10);
        }
    }

}
