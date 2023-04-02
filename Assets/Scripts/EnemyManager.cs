using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    Basic,
    Ace,
    Heavy
}

public class EnemyManager : MonoBehaviour
{

    [SerializeField] private int totalEnemiesInWave = 5;
    [SerializeField] private int basicEnemyFlock = 3;
    [SerializeField] private int maxAces = 2;
    [SerializeField] private int maxHeavies = 1;
    [SerializeField] private float enemySpawnDelay = 1;

    [Header("Enemies")]
    [SerializeField] private DroneEnemy basicEnemyPrefab;
    [SerializeField] private AceEnemy aceEnemyPrefab;
    [SerializeField] private HeavyEnemy heavyEnemyPrefab;

    private int acesDeployed = 0;
    private int heaviesDeployed = 0;

    private int enemiesDeployed = 0;
    private int enemiesDestroyed = 0;
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

        NextWave();
        StartCoroutine(EnemySpawnRoutine());
    }

    public void DestroyedEnemy() {
        enemiesDestroyed++;
    }

    private void SpawnBasicEnemies(int num) {
        GetRandomPosition(out float x, out float y);
        for (int i = 0; i < num; i++)
        {
            Instantiate(basicEnemyPrefab, new Vector2(x, y) + Random.insideUnitCircle * 0.01f, Quaternion.identity);

            enemiesDeployed++;
        }
    }

    private void SpawnAceEnemy() {
        if (acesDeployed < maxAces)
        {
            GetRandomPosition(out float x, out float y);
            Instantiate(aceEnemyPrefab, new Vector2(x, y), Quaternion.identity);

            acesDeployed++;
            enemiesDeployed++;
        } else
        {
            SpawnBasicEnemies(basicEnemyFlock);
        }
    }

    private void SpawnHeavyEnemy() {
        if (heaviesDeployed < maxHeavies)
        {
            GetRandomPosition(out float x, out float y);
            Instantiate(heavyEnemyPrefab, new Vector2(x, y), Quaternion.identity);

            heaviesDeployed++;
            enemiesDeployed++;
        } else
        {
            SpawnAceEnemy();
        }
    }

    private IEnumerator EnemySpawnRoutine() {
        while (true)
        {
            if (enemiesDestroyed < totalEnemiesInWave)
            {
                EnemyType randomEnemyType = (EnemyType)Random.Range(0, System.Enum.GetValues(typeof(EnemyType)).Length);
                switch (randomEnemyType)
                {
                    case EnemyType.Ace: { SpawnAceEnemy(); break; }
                    case EnemyType.Heavy: { SpawnHeavyEnemy(); break; }
                    default: { SpawnBasicEnemies(basicEnemyFlock); break; }
                }

                yield return new WaitForSeconds(enemySpawnDelay);
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

        acesDeployed = 0;
        enemiesDestroyed = 0;
        enemiesDeployed = 0;
    }

    private void GetRandomPosition(out float x, out float y) {
        x = Random.Range(box.bounds.min.x, box.bounds.max.x);
        y = Random.Range(box.bounds.min.y, box.bounds.max.y);
    }

}
