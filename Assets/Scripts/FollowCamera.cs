using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{

    public Vector2 fallbackPosition;

    [SerializeField] private float smoothing = 0.1f;
    [SerializeField] private float dynamicCamera = 5;
    [SerializeField] private Transform player;

    // Screen shake
    private float shakeDuration = 0f;
    private float shakeMagnitude = 0.7f;
    private float dampingSpeed = 1.0f;

    #region Singleton
    
    static public FollowCamera Instance = null;
    void Awake() {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);
    }
    
    #endregion

    void Update() {
        Vector3 initialPosition = transform.localPosition;

        if (shakeDuration > 0) {
            transform.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;
            shakeDuration -= Time.unscaledDeltaTime * dampingSpeed;
        } else {
            shakeDuration = 0f;
            transform.localPosition = initialPosition;
        }
    }

    void FixedUpdate() {
        Vector3 targetPos;
        if (player != null)
        {
            targetPos = new Vector3(player.position.x, player.position.y, -10);
        } else
        {
            targetPos = new Vector3(fallbackPosition.x, fallbackPosition.y, -10);
        }
        
        if (player != null) targetPos += player.rotation * Vector3.up * dynamicCamera;
        targetPos.z = -10;
        transform.position = Vector3.Lerp(transform.position, targetPos, smoothing);
    }

    public void ScreenShake(float duration, float magnitude) {
        shakeDuration = duration;
        shakeMagnitude = magnitude;
    }

    public void Hitstop(float duration) {
        StartCoroutine(HitstopRoutine(duration));
    }

    private IEnumerator HitstopRoutine(float duration) {
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = 1;
    }

}
