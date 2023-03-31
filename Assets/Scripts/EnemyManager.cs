using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    Basic,
    Ace
}

public class EnemyManager : MonoBehaviour
{

    [SerializeField] private int totalEnemiesInWave = 5;
    [SerializeField] private int basicEnemyFlock = 3;
    [SerializeField] private int maxAces = 2;
    [SerializeField] private float enemySpawnDelay = 1;

    [Header("Starting enemies")]
    [SerializeField] private int startingBasicEnemyCount = 5;
    [SerializeField] private int startingAceCount = 1;

    [Header("Enemies")]
    [SerializeField] private FollowEnemy basicEnemyPrefab;
    [SerializeField] private FollowEnemy aceEnemyPrefab;

    private int acesDeployed = 0;
    private int enemiesDeployed = 0;
    private int currentWave = 1;

    BoxCollider2D box;

    void Start() {
        box = GetComponent<BoxCollider2D>();
        StartCoroutine(EnemySpawnRoutine());

        SpawnBasicEnemies(startingBasicEnemyCount);
        for (int i = 0; i < startingAceCount; i++) SpawnAceEnemy();
    }

    private void SpawnBasicEnemies(int num) {
        GetRandomPosition(out float x, out float y);
        for (int i = 0; i < num; i++)
        {
            FollowEnemy enemy = Instantiate(basicEnemyPrefab, new Vector2(x, y) + Random.insideUnitCircle * 0.01f, Quaternion.identity);
        }
    }

    private void SpawnAceEnemy() {
        if (acesDeployed < maxAces)
        {
            GetRandomPosition(out float x, out float y);
            FollowEnemy enemy = Instantiate(aceEnemyPrefab, new Vector2(x, y), Quaternion.identity);
            acesDeployed++;
        } else
        {
            SpawnBasicEnemies(basicEnemyFlock);
        }
    }

    private IEnumerator EnemySpawnRoutine() {
        while (true)
        {
            if (enemiesDeployed < totalEnemiesInWave)
            {
                yield return new WaitForSeconds(enemySpawnDelay);
                EnemyType randomEnemyType = (EnemyType)Random.Range(0, System.Enum.GetValues(typeof(EnemyType)).Length);
                switch (randomEnemyType)
                {
                    case EnemyType.Ace: { SpawnAceEnemy(); break; }
                    default: { SpawnBasicEnemies(basicEnemyFlock); break; }
                }

                enemiesDeployed++;
            } else
            {
                NextWave();
                break;
            }
        }
    }

    private void NextWave() {
        currentWave++;
        print("Wave: " + currentWave);

        acesDeployed = 0;
        enemiesDeployed = 0;
    }

    private void GetRandomPosition(out float x, out float y) {
        x = Random.Range(box.bounds.min.x, box.bounds.max.x);
        y = Random.Range(box.bounds.min.y, box.bounds.max.y);
    }

}
