using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneEnemy : MonoBehaviour
{

    [SerializeField] private GameObject graphics;
    [SerializeField] private float flashDelay = 0.1f;
    [SerializeField] private float explodeDelay = 1;
    [SerializeField] private float followMultiplier = 2;
    [SerializeField] private float beforeExplosionBoost = 1;
    [SerializeField] private float flashSpeedIncrement = 0.01f;
    [SerializeField] private float explosionRadius = 2;
    [SerializeField] private float activationRange = 2;
    [SerializeField] private LayerMask playerLayer;

    private bool activated;

    Transform player;
    Enemy enemy;
    FollowEnemy followEnemy;
    Rigidbody2D enemyBody;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemy = GetComponent<Enemy>();
        followEnemy = GetComponent<FollowEnemy>();
        enemyBody = GetComponent<Rigidbody2D>();
    }

    void Update() {
        if (Vector2.Distance(transform.position, player.position) < activationRange && !activated)
        {
            activated = true;
            StartCoroutine(Flash());
        }
    }

    private IEnumerator Flash() {
        float flashSpeed = flashDelay;
        while (true)
        {
            graphics.SetActive(false);
            yield return new WaitForSeconds(flashSpeed);
            graphics.SetActive(true);
            yield return new WaitForSeconds(flashSpeed);

            flashSpeed -= flashSpeedIncrement;
            if (flashSpeed <= flashSpeedIncrement)
            {
                flashSpeed = 0;
                StartCoroutine(Attack());
                break;
            }
        }
    }

    private IEnumerator Attack() {
        enemyBody.AddRelativeForce(Vector2.up * beforeExplosionBoost, ForceMode2D.Impulse);
        followEnemy.speed *= followMultiplier;
        followEnemy.rotationSmoothing = 1;

        yield return new WaitForSeconds(explodeDelay);

        bool hitPlayer = Physics2D.OverlapCircle(transform.position, explosionRadius, playerLayer);
        if (hitPlayer && !PlayerHealth.Instance.isInvincible)
        {
            PlayerHealth.Instance.GetHurt();
        }

        enemy.Die(ParticleManager.Instance.enemyExplosion);
    }

}
