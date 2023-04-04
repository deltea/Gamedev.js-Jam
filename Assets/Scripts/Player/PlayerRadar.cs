using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRadar : MonoBehaviour
{

    [SerializeField] private RectTransform arrowPrefab;
    [SerializeField] private RectTransform canvasTransform;
    [SerializeField] private float offset = 0;
    [SerializeField] private float radius = 20;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private LayerMask radarBoundsLayer;

    Camera cam;

    void Start() {
        cam = Camera.main;

        Instantiate(arrowPrefab, canvasTransform);
    }

    void LateUpdate() {
        GameObject[] radarArrows = GameObject.FindGameObjectsWithTag("Radar Arrow");
        foreach (GameObject radarArrow in radarArrows)
        {
            Destroy(radarArrow);
        }

        Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(transform.position, radius, enemyLayer);
        foreach (Collider2D enemyCollider in enemiesInRange)
        {
            Vector2 direction = enemyCollider.transform.position - cam.transform.position;
            RaycastHit2D hit = Physics2D.Raycast(cam.transform.position, direction, 50, radarBoundsLayer);
            
            if (direction.magnitude > hit.distance)
            {
                Vector3 position = ((Vector3)hit.point - cam.transform.position);
                Utilities.DirectionToRotation(hit.normal, out Quaternion facing);
                Instantiate(arrowPrefab, Vector3.zero, facing, canvasTransform).localPosition = position * (18 - offset);
            }
        }
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

}
