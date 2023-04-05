using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] private float health = 100;
    [SerializeField] private EnemyObject enemyObject;

    private void GetHurt(float damage) {
        health -= damage;
        if (health <= 0)
        {
            Die(ParticleManager.Instance.explosion);
        }
    }

    public void Die(ParticleSystem particles) {
        Destroy(gameObject);

        ParticleManager.Instance.Play(particles, transform.position, Quaternion.identity);
        FollowCamera.Instance.ScreenShake(0.2f, 0.2f);
        FollowCamera.Instance.Hitstop(0.1f);

        EnemyManager.Instance.DestroyEnemy(enemyObject.enemyPoints);
    }

    void OnTriggerEnter2D(Collider2D trigger) {
        if (trigger.CompareTag("Bullet"))
        {
            Destroy(trigger.gameObject);
            GetHurt(10);
        }
    }

}
