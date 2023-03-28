using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{

    [SerializeField] private float smoothing = 0.1f;
    [SerializeField] private Transform player;

    void FixedUpdate() {
        Vector3 targetPos = new Vector3(player.position.x, player.position.y, -10);
        transform.position = Vector3.Lerp(transform.position, targetPos, smoothing);
    }

}
