using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    public bool isInvincible = false;

    [SerializeField] private Transform heartPrefab;
    [SerializeField] private Transform heartBar;
    [SerializeField] private GameObject graphics;
    [SerializeField] private float invincibleTime = 2;
    [SerializeField] private float invincibleFlashDelay = 0.2f;
    [SerializeField] private int maxHealth = 5;
    
    private int health;

    Collider2D playerCollider;

    #region Singleton
    
    static public PlayerHealth Instance = null;
    void Awake() {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);
    }
    
    #endregion

    void Start() {
        playerCollider = GetComponent<Collider2D>();

        health = maxHealth;
        AddHearts(maxHealth);
    }

    public void GetHurt() {
        health--;
        AddHearts(-1);
        StartCoroutine(Invincible());

        FollowCamera.Instance.ScreenShake(0.1f, 0.2f);
        FollowCamera.Instance.Hitstop(0.1f);

        print("Oooooff! Your health is now: " + health);
        if (health <= 0)
        {
            Die();
        }
    }

    public void Heal() {
        health++;
        AddHearts(1);
    }

    private IEnumerator Flash() {
        while (true)
        {
            graphics.SetActive(false);
            yield return new WaitForSeconds(invincibleFlashDelay);
            graphics.SetActive(true);
            yield return new WaitForSeconds(invincibleFlashDelay);
        }
    }

    private IEnumerator Invincible() {
        isInvincible = true;

        StartCoroutine(Flash());
        yield return new WaitForSeconds(invincibleTime);
        StopAllCoroutines();
        
        isInvincible = false;
    }

    private void Die() {
        print("You DIED");
    }

    private void AddHearts(int amount) {
        if (amount > 0)
        {
            for (int i = 0; i < amount; i++)
            {
                Instantiate(heartPrefab, heartBar);
            }
        } else if (amount < 0) {
            for (int i = 0; i < Mathf.Abs(amount); i++)
            {
                Destroy(heartBar.GetChild(0).gameObject);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D trigger) {
        if (trigger.CompareTag("Enemy") && PlayerMovement.Instance.boostKilling)
        {
            trigger.GetComponent<Enemy>().Die(ParticleManager.Instance.explosion);
        } else if (trigger.CompareTag("Enemy Bullet") && !isInvincible && !PlayerMovement.Instance.boostKilling)
        {
            Destroy(trigger.gameObject);
            GetHurt();
        }
    }

}
