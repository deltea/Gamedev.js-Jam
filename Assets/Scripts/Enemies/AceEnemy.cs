using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AceEnemy : MonoBehaviour
{

    [SerializeField] private Rigidbody2D bulletPrefab;
    [SerializeField] private Transform firePoint;

    [Header("Stats")]
    [SerializeField] private float lookAhead = 1;
    [SerializeField] private float fireDelay = 2;
    [SerializeField] private float bulletSpeed = 10;
    [SerializeField] private float timeBetweenBullets = 0.1f;
    [SerializeField] private int numOfBullets = 3;

    Transform player;
    Rigidbody2D playerBody;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerBody = player.GetComponent<Rigidbody2D>();

        StartCoroutine(ShootRoutine());
    }

    private IEnumerator ShootRoutine() {
        while (true)
        {
            yield return new WaitForSeconds(fireDelay);
            for (int i = 0; i < numOfBullets; i++)
            {
                Rigidbody2D bulletBody = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

                Vector2 direction = player.position + ((Vector3)playerBody.velocity * lookAhead) - transform.position;
                bulletBody.AddForce(direction.normalized * bulletSpeed, ForceMode2D.Impulse);
                
                yield return new WaitForSeconds(timeBetweenBullets);
            }
        }
    }

}
