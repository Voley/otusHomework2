using ShootEmUp;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedEnemySpawner : MonoBehaviour, IGameResolveDependenciesListener, IGameStartListener
{
    [SerializeField] private float _enemySpawnInterval;

    private EnemySpawner _enemySpawner;

    void IGameResolveDependenciesListener.OnGameResolvingDependencies()
    {
        _enemySpawner = ServiceLocator.Shared.GetService<EnemySpawner>();
    }

    void IGameStartListener.OnGameStarted()
    {
        StartCoroutine(StartSpawning());
    }

    private IEnumerator StartSpawning()
    {
        while (true)
        {
            yield return new WaitForSeconds(_enemySpawnInterval);
            _ = _enemySpawner.GetEnemy();
        }
    }

    public void RemoveEnemy(GameObject enemy)
    {
        _enemySpawner.DespawnEnemy(enemy);
    }
}
