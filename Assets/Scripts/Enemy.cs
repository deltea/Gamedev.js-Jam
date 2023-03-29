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
            Die();
        }
    }

    public void Die() {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D trigger) {
        if (trigger.CompareTag("Bullet"))
        {
            Destroy(trigger.gameObject);
            GetHurt(10);
        }
    }

}
