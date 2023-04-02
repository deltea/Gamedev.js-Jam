using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyEnemy : MonoBehaviour
{

    [SerializeField] private Rigidbody2D missilePrefab;

    [Header("Stats")]
    [SerializeField] private float fireDelay = 2;

    void Start() {
        StartCoroutine(FireRoutine());
    }

    private IEnumerator FireRoutine() {
        while (true)
        {
            yield return new WaitForSeconds(fireDelay);
            Rigidbody2D missileBody = Instantiate(missilePrefab, transform.position, Quaternion.identity);
        }
    }

}
