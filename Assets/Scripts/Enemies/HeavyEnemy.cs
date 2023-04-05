using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyEnemy : MonoBehaviour
{

    [SerializeField] private Rigidbody2D missilePrefab;

    [Header("Stats")]
    [SerializeField] private float fireDelay = 2;

    Transform player;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        StartCoroutine(FireRoutine());
    }

    private IEnumerator FireRoutine() {
        while (true)
        {
            yield return new WaitForSeconds(fireDelay);

            Utilities.DirectionToRotation(player.position - transform.position, out Quaternion rotation);
            Rigidbody2D missileBody = Instantiate(missilePrefab, transform.position, rotation);
        }
    }

}
