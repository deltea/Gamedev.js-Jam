using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    // Stats
    [Header("Thrusting")]
    [SerializeField] private float thrustForce = 10;
    [SerializeField] private float thrustingDrag = 20;

    [Header("Boosting")]
    [SerializeField] private float boostingForce = 10;
    [SerializeField] private float boostStartForce = 5;
    [SerializeField] private ParticleSystem boostingParticles;

    [Header("Turning")]
    [SerializeField] private float turnSpeed = 5;
    [SerializeField] private float thrustingTurnSpeed = 1.5f;
    [SerializeField] private float thrustingAngularDrag = 20;

    // Input
    private float thrustInput;
    private float turnInput;
    private float boostInput;

    Rigidbody2D playerBody;
    float originalDrag;
    private float originalAngularDrag;

    void Start() {
        playerBody = GetComponent<Rigidbody2D>();

        originalDrag = playerBody.drag;
        originalAngularDrag = playerBody.angularDrag;
    }

    void FixedUpdate() {
        playerBody.drag = thrustingDrag * thrustInput;
        playerBody.gravityScale = (int)thrustInput ^ 1;
        playerBody.angularDrag = thrustInput > 0 ? thrustingAngularDrag : originalAngularDrag;

        float force = thrustInput * (boostInput > 0 ? boostingForce : thrustForce);

        playerBody.AddRelativeForce(Vector2.up * force);
        playerBody.AddTorque(turnInput * (thrustInput > 0 ? thrustingTurnSpeed : turnSpeed));
    }

    void OnRotation(InputValue value) {
        turnInput = value.Get<float>();
    }

    void OnThrust(InputValue value) {
        thrustInput = value.Get<float>();
    }

    void OnBoost(InputValue value) {
        boostInput = value.Get<float>();
        if (boostInput > 0 && thrustInput > 0)
        {
            playerBody.AddRelativeForce(Vector2.up * boostStartForce, ForceMode2D.Impulse);
            boostingParticles.Play();
        } else if (boostInput == 0) {
            boostingParticles.Stop();
        }
    }

}
