using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{

    [SerializeField] private float speed = 0.05f;
    [SerializeField] private float rotationSmoothing = 1;

    Transform player;
    Rigidbody2D missileBody;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        missileBody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate() {
        Vector2 direction = player.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, Quaternion.identity * direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSmoothing);

        missileBody.AddRelativeForce(Vector2.up * speed);
    }

}
