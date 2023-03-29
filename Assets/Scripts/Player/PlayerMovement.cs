using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    // Stats
    [SerializeField] private float thrustForce = 10;
    [SerializeField] private float boostingThrustForce = 20;
    [SerializeField] private float boostingDrag = 5;
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
        if (boostInput > 0)
        {
            playerBody.AddRelativeForce(Vector2.up * thrustInput * boostingThrustForce);
            playerBody.drag = boostingDrag;
        } else
        {
            playerBody.AddRelativeForce(Vector2.up * thrustInput * thrustForce);
            playerBody.drag = originalDrag;
        }
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
