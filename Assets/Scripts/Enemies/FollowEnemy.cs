using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowEnemy : MonoBehaviour
{

    public float speed = 5;
    public float rotationSmoothing = 0.05f;

    Transform player;
    Rigidbody2D enemyBody;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemyBody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate() {
        Vector2 direction = player.position - transform.position;
        Utilities.DirectionToRotation(direction, out Quaternion targetRotation);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSmoothing);

        enemyBody.AddRelativeForce(Vector2.up * speed);
    }

}
