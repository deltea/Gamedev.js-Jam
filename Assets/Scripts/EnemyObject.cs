using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy", fileName = "New Enemy")]
public class EnemyObject : ScriptableObject
{

    public Enemy prefab;
    public int enemyPoints = 10;
    [Range(1, 5)] public int count = 1;

}
