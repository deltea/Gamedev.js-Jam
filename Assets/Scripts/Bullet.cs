using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    Rigidbody2D bulletBody;

    void Start() {
        bulletBody = GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D trigger) {
        if (trigger.CompareTag("BulletBounds") || trigger.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }

}
