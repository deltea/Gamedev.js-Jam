using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    // Stats
    [SerializeField] private float thrustForce = 10;
    [SerializeField] private float turnSpeed = 5;

    // Input
    private float thrustInput;
    private float turnInput;

    Rigidbody2D playerBody;

    void Start() {
        playerBody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate() {
        playerBody.AddRelativeForce(Vector2.up * thrustInput * thrustForce);
        // if (playerBody.rotation > 90 && playerBody.rotation < 180)
        // {
        // } else
        // {
        //     playerBody.AddRelativeForce(Vector2.up * thrustInput * thrustForce / 2);      
        // }

        playerBody.AddTorque(turnInput * turnSpeed);
    }

    void OnRotation(InputValue value) {
        turnInput = value.Get<float>();
    }

    void OnThrust(InputValue value) {
        thrustInput = value.Get<float>();
    }

}
