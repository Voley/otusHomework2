using ShootEmUp;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;

    public GameObject SpawnEnemyIn(Transform container)
    {
        return Instantiate(_enemyPrefab, container);
    }
}
