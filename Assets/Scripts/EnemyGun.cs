using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour
{

    [SerializeField] private Rigidbody2D enemyBulletPrefab;

    // Stats
    [SerializeField] private float bulletSpeed = 10;
    [SerializeField] private float fireDelay = 2;
    [SerializeField] private float spread = 5;

    void Start() {
        StartCoroutine(FireRoutine());
    }

    private IEnumerator FireRoutine() {
        while (true)
        {
            yield return new WaitForSeconds(fireDelay);
            Rigidbody2D bulletBody = Instantiate(enemyBulletPrefab, transform.position, transform.rotation * Quaternion.Euler(0, 0, Random.Range(-spread, spread)));
            bulletBody.AddRelativeForce(Vector2.up * bulletSpeed, ForceMode2D.Impulse);
        }
    }

}
