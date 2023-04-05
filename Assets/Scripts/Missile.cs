using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{

    [SerializeField] private float speed = 0.05f;
    [SerializeField] private float rotationSmoothing = 1;
    [SerializeField] private float explosionDelay = 1;
    [SerializeField] private float activationRange = 5;
    [SerializeField] private float explosionRadius = 2;
    [SerializeField] private LayerMask playerLayer;

    private bool activated = false;

    Transform player;
    Rigidbody2D missileBody;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        missileBody = GetComponent<Rigidbody2D>();
    }

    void Update() {
        if (Vector2.Distance(transform.position, player.position) < activationRange && !activated)
        {
            activated = true;
            StartCoroutine(Explode());
        }
    }

    void FixedUpdate() {
        Vector2 direction = player.position - transform.position;
        Utilities.DirectionToRotation(direction, out Quaternion targetRotation);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSmoothing);

        missileBody.AddRelativeForce(Vector2.up * speed);
    }

    private IEnumerator Explode() {
        yield return new WaitForSeconds(explosionDelay);

        ParticleManager.Instance.Play(ParticleManager.Instance.enemyExplosion, transform.position, Quaternion.identity);

        bool hitPlayer = Physics2D.OverlapCircle(transform.position, explosionRadius, playerLayer);
        if (hitPlayer && !PlayerHealth.Instance.isInvincible)
        {
            PlayerHealth.Instance.GetHurt();
        }

        Destroy(gameObject);
    }

}
