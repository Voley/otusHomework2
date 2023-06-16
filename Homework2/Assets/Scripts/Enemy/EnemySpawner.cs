using ShootEmUp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class EnemySpawner : MonoBehaviour, IGameResolveDependenciesListener
{
    [SerializeField] private GameObject _character;
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private EnemyPositions _enemyPositions;
    [SerializeField] private EnemyPool _pool;

    public Action<HitPointsComponent, EnemyAttackAgent> OnEnemySpawned;

    void IGameResolveDependenciesListener.OnGameResolvingDependencies() 
    {
        _pool = ServiceLocator.Shared.GetService<EnemyPool>();
    }

    public GameObject SpawnEnemy()
    {
        return Instantiate(_enemyPrefab);
    }

    public GameObject GetEnemy()
    {
        GameObject enemy = _pool.GetEnemy();

        var spawnPosition = _enemyPositions.RandomSpawnPosition();
        enemy.transform.position = spawnPosition.position;

        var attackPosition = _enemyPositions.RandomAttackPosition();
        enemy.GetComponent<EnemyMoveAgent>().SetDestination(attackPosition.position);

        enemy.GetComponent<EnemyAttackAgent>().SetTarget(_character);

        OnEnemySpawned?.Invoke(enemy.GetComponent<HitPointsComponent>(), enemy.GetComponent<EnemyAttackAgent>());

        return enemy;
    }

    public void DespawnEnemy(GameObject enemy)
    {
        _pool.RemoveEnemy(enemy);
    }
}
