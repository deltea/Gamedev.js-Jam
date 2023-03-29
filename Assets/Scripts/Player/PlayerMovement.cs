using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    // Stats
    [SerializeField] private float thrustForce = 10;
    [SerializeField] private float thrustingDrag = 20;
    [SerializeField] private float turnSpeed = 5;

    // Input
    private float thrustInput;
    private float turnInput;
    private float boostInput;

    Rigidbody2D playerBody;
    float originalDrag;

    void Start() {
        playerBody = GetComponent<Rigidbody2D>();

        originalDrag = playerBody.drag;
    }

    void FixedUpdate() {
        playerBody.drag = thrustingDrag * thrustInput;
        playerBody.gravityScale = (int)thrustInput ^ 1;
        playerBody.AddRelativeForce(Vector2.up * thrustInput * thrustForce);
        playerBody.AddTorque(turnInput * turnSpeed);
    }

    void OnRotation(InputValue value) {
        turnInput = value.Get<float>();
    }

    void OnThrust(InputValue value) {
        thrustInput = value.Get<float>();
    }

    void OnBoost(InputValue value) {
        boostInput = value.Get<float>();
    }

}
