using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    [SerializeField] private float maxHealth = 100;
    
    private float health;

    void Start() {
        health = maxHealth;
    }

    private void GetHurt(float damage) {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die() {
        print("You DIED");
    }

    void OnTriggerEnter2D(Collider2D trigger) {
        if (trigger.CompareTag("Enemy"))
        {
            trigger.GetComponent<Enemy>().Die();
            GetHurt(10);
        } else if (trigger.CompareTag("Enemy Bullet"))
        {
            Destroy(trigger.gameObject);
            GetHurt(5);
        }
    }

}
