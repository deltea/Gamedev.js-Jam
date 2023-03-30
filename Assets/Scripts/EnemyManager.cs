using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    [SerializeField] private int enemyCount = 5;
    [SerializeField] private FollowEnemy enemyPrefab;
    [SerializeField] private float enemySpawnDelay = 1;

    BoxCollider2D box;

    void Start() {
        box = GetComponent<BoxCollider2D>();
        StartCoroutine(EnemySpawnRoutine());

        for (int i = 0; i < enemyCount; i++)
        {
            SpawnEnemy();
        }
    }

    private void SpawnEnemy() {
        float x = Random.Range(box.bounds.min.x, box.bounds.max.x);
        float y = Random.Range(box.bounds.min.y, box.bounds.max.y);

        FollowEnemy enemy = Instantiate(enemyPrefab, new Vector2(x, y), Quaternion.identity);
    }

    private IEnumerator EnemySpawnRoutine() {
        while (true)
        {
            yield return new WaitForSeconds(enemySpawnDelay);
            SpawnEnemy();
        }
    }

}
