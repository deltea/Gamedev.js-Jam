using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    [SerializeField] private int maxHealth = 5;
    
    private int health;

    #region Singleton
    
    static public PlayerHealth Instance = null;
    void Awake() {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);
    }
    
    #endregion

    void Start() {
        health = maxHealth;
    }

    public void GetHurt() {
        health--;

        FollowCamera.Instance.ScreenShake(0.1f, 0.2f);
        FollowCamera.Instance.Hitstop(0.1f);

        print("Oooooff! Your health is now: " + health);
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die() {
        print("You DIED");
    }

    void OnTriggerEnter2D(Collider2D trigger) {
        if (trigger.CompareTag("Enemy") && PlayerMovement.Instance.boostKilling)
        {
            trigger.GetComponent<Enemy>().Die(ParticleManager.Instance.explosion);
        } else if (trigger.CompareTag("Enemy Bullet"))
        {
            Destroy(trigger.gameObject);
            GetHurt();
        }
    }

}
