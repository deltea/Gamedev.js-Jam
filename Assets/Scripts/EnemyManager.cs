using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    [SerializeField] private float startingEnemyPoints = 100;
    [SerializeField] private float enemySpawnMin = 1;
    [SerializeField] private float enemySpawnMax = 2;
    [SerializeField] private EnemyObject[] enemies;

    public float enemyPoints;
    private int currentWave = 0;

    BoxCollider2D box;

    #region Singleton
    
    static public EnemyManager Instance = null;
    void Awake() {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);
    }
    
    #endregion

    void Start() {
        box = GetComponent<BoxCollider2D>();

        enemyPoints = startingEnemyPoints;

        NextWave();
        StartCoroutine(EnemySpawnRoutine());
    }

    private IEnumerator EnemySpawnRoutine() {
        while (true)
        {
            if (enemyPoints > 0)
            {
                EnemyObject randomEnemy = enemies[Random.Range(0, enemies.Length)];
                GetRandomPosition(out float x, out float y);
                // TODO: Add probability
                if (randomEnemy.enemyPoints <= enemyPoints)
                {
                    print("Spawned: " + randomEnemy.name + " enemy");
                    
                    for (int i = 0; i < randomEnemy.count; i++)
                    {
                        Vector2 flockOffset = randomEnemy.count > 1 ? Random.insideUnitCircle * 0.01f : Vector2.zero;
                        Instantiate(randomEnemy.prefab, new Vector2(x, y) + flockOffset, Quaternion.identity);
                    }

                    enemyPoints -= randomEnemy.enemyPoints;
                }

                yield return new WaitForSeconds(Random.Range(enemySpawnMin, enemySpawnMax));
            } else
            {
                break;
            }
        }

        print("Choose upgrade");
    }

    private void NextWave() {
        currentWave++;
        print("Wave: " + currentWave);
    }

    private void GetRandomPosition(out float x, out float y) {
        x = Random.Range(box.bounds.min.x, box.bounds.max.x);
        y = Random.Range(box.bounds.min.y, box.bounds.max.y);
    }

}
