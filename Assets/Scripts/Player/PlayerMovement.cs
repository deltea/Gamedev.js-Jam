using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private Transform graphics;
    [SerializeField] private float yRotationAmount = 20;
    [SerializeField] private float rotationSmoothing = 0.01f;

    // Stats
    [Header("Thrusting")]
    [SerializeField] private float thrustForce = 10;
    [SerializeField] private float thrustingDrag = 20;
    [SerializeField] private ParticleSystem thrustParticles;

    [Header("Boosting")]
    [SerializeField] private float boostingForce = 10;
    [SerializeField] private float boostStartForce = 5;
    [SerializeField] private ParticleSystem boostingParticles;

    [Header("Turning")]
    [SerializeField] private float turnSpeed = 5;
    [SerializeField] private float thrustingTurnSpeed = 1.5f;
    [SerializeField] private float boostingTurnSpeed = 0.5f;
    [SerializeField] private float thrustingAngularDrag = 20;

    // Input
    private float thrustInput;
    private float turnInput;
    private float boostInput;

    Rigidbody2D playerBody;
    float originalDrag;
    float originalAngularDrag;

    #region Singleton
    
    static public PlayerMovement Instance = null;
    void Awake() {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);
    }
    
    #endregion

    void Start() {
        playerBody = GetComponent<Rigidbody2D>();

        originalDrag = playerBody.drag;
        originalAngularDrag = playerBody.angularDrag;
    }

    void Update() {
        Quaternion targetRotation = Quaternion.Euler(-90, playerBody.velocity.normalized.x * yRotationAmount, 0);
        graphics.localRotation = Quaternion.Lerp(graphics.localRotation, targetRotation, rotationSmoothing);
    }

    void FixedUpdate() {
        playerBody.drag = thrustingDrag * thrustInput;
        playerBody.gravityScale = (int)thrustInput ^ 1;
        playerBody.angularDrag = thrustInput > 0 ? thrustingAngularDrag : originalAngularDrag;

        float force = thrustInput * (boostInput > 0 ? boostingForce : thrustForce);
        float turn = thrustInput > 0 ? (boostInput > 0 ? boostingTurnSpeed : thrustingTurnSpeed) : turnSpeed;

        playerBody.AddRelativeForce(Vector2.up * force);
        playerBody.AddTorque(turnInput * turn);
    }

    private void Boost() {
        playerBody.AddRelativeForce(Vector2.up * boostStartForce, ForceMode2D.Impulse);
        boostingParticles.Play();

        FollowCamera.Instance.ScreenShake(0.1f, 0.2f);
        FollowCamera.Instance.Hitstop(0.05f);
    }

    void OnRotation(InputValue value) {
        turnInput = value.Get<float>();
    }

    void OnThrust(InputValue value) {
        thrustInput = value.Get<float>();

        if (boostInput > 0 && thrustInput > 0) Boost();
        else boostingParticles.Stop();

        if (thrustInput > 0 && boostInput == 0) thrustParticles.Play();
        else if (thrustInput == 0) thrustParticles.Stop();
    }

    void OnBoost(InputValue value) {
        boostInput = value.Get<float>();

        if (boostInput > 0 && thrustInput > 0) {
            thrustParticles.Stop();
            Boost();
        }
        else if (boostInput == 0) {
            boostingParticles.Stop();
            if (thrustInput > 0) thrustParticles.Play();
        }
    }

}
