using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private Transform graphics;
    [SerializeField] private float yRotationAmount = 20;
    [SerializeField] private float yRotationRange = 10;
    [SerializeField] private float rotationSmoothing = 0.01f;

    // Stats
    [Header("Thrusting")]
    [SerializeField] private float thrustForce = 10;
    [SerializeField] private float thrustingDrag = 20;
    [SerializeField] private ParticleSystem thrustParticles;

    [Header("Boosting")]
    [SerializeField] private float boostingForce = 10;
    [SerializeField] private float boostCooldown = 3;
    [SerializeField] private float boostStartForce = 5;
    [SerializeField] private float boostKillTime = 0.5f;
    [SerializeField] private ParticleSystem boostingParticles;
    [SerializeField] private ParticleSystem boostReadyParticles;

    [Header("Turning")]
    [SerializeField] private float turnSpeed = 5;
    [SerializeField] private float thrustingTurnSpeed = 1.5f;
    [SerializeField] private float boostingTurnSpeed = 0.5f;
    [SerializeField] private float thrustingAngularDrag = 20;

    // Input
    private float thrustInput;
    private float turnInput;
    private float boostInput;
    [HideInInspector] public bool boostKilling;

    Rigidbody2D playerBody;
    float originalDrag;
    float originalAngularDrag;
    public bool boostReady = true;
    public bool boosting = false;

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
        if (!PlayerHealth.Instance.isDead)
        {
            float yRotation = 0;
            float zRotation = transform.eulerAngles.z;

            if (zRotation > yRotationRange && zRotation < 180 - yRotationRange)
            {
                yRotation = -1;
            } else if (zRotation > 180 + yRotationRange && zRotation < 360 - yRotationRange)
            {
                yRotation = 1;
            }

            Quaternion targetRotation = Quaternion.Euler(-90, yRotation * yRotationAmount, 0);
            graphics.localRotation = Quaternion.Lerp(graphics.localRotation, targetRotation, rotationSmoothing);
        }
    }

    void FixedUpdate() {
        if (!PlayerHealth.Instance.isDead)
        {
            playerBody.drag = thrustingDrag * thrustInput;
            playerBody.gravityScale = (int)thrustInput ^ 1;
            playerBody.angularDrag = thrustInput > 0 ? thrustingAngularDrag : originalAngularDrag;

            float force = thrustInput * ((boostInput > 0 && boosting) ? boostingForce : thrustForce);
            float turn = thrustInput > 0 ? ((boostInput > 0 && boosting) ? boostingTurnSpeed : thrustingTurnSpeed) : turnSpeed;

            playerBody.AddRelativeForce(Vector2.up * force);
            playerBody.AddTorque(turnInput * turn);
        }
    }

    private IEnumerator Boost() {
        if (!PlayerHealth.Instance.isDead)
        {
            boostReady = false;
                    
            playerBody.AddRelativeForce(Vector2.up * boostStartForce, ForceMode2D.Impulse);
            boostingParticles.Play();

            FollowCamera.Instance.ScreenShake(0.1f, 0.2f);
            FollowCamera.Instance.Hitstop(0.08f);

            boostKilling = true;
            yield return new WaitForSeconds(boostKillTime);
            boostKilling = false;
        }
    }

    private IEnumerator Cooldown() {
        yield return new WaitForSeconds(boostCooldown);
        boostReady = true;
        boostReadyParticles.Play();
    }

    void OnRotation(InputValue value) {
        turnInput = value.Get<float>();
    }

    void OnThrust(InputValue value) {
        thrustInput = value.Get<float>();

        if (!PlayerHealth.Instance.isDead)
        {
            if (boostInput > 0 && thrustInput > 0 && boostReady) {
                boosting = true;
                StartCoroutine(Boost());
            } else {
                boosting = false;
                StartCoroutine(Cooldown());
                boostingParticles.Stop();
            }

            if (thrustInput > 0 && boostInput == 0) thrustParticles.Play();
            else if (thrustInput == 0) thrustParticles.Stop();
        }
    }

    void OnBoost(InputValue value) {
        boostInput = value.Get<float>();
        
        if (!PlayerHealth.Instance.isDead)
        {
            if (boostInput > 0 && thrustInput > 0 && boostReady) {
                boosting = true;
                thrustParticles.Stop();
                StartCoroutine(Boost());
            }
            else if (boostInput == 0) {
                boosting = false;
                StartCoroutine(Cooldown());
                boostingParticles.Stop();
                if (thrustInput > 0) thrustParticles.Play();
            }
        }
    }

}
