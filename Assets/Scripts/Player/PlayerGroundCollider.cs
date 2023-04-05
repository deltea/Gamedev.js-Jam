using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundCollider : MonoBehaviour
{

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Ground") && PlayerHealth.Instance.isDead)
        {
            FollowCamera.Instance.fallbackPosition = transform.position;
            Destroy(transform.parent.gameObject);
            SceneLoader.Instance.LoadSceneWithDelay("Dead", 2);
            ParticleManager.Instance.Play(ParticleManager.Instance.bigExplosion, transform.position, Quaternion.identity);
        }
    }

}
