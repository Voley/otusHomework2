using ShootEmUp;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedEnemySpawner : MonoBehaviour, IGameResolveDependenciesListener, IGameStartListener
{
    public Action<HitPointsComponent, EnemyAttackAgent> OnEnemySpawned;

    private EnemyPool _enemyPool;

    void IGameResolveDependenciesListener.OnGameResolvingDependencies()
    {
        _enemyPool = ServiceLocator.Shared.GetService<EnemyPool>();
    }

    void IGameStartListener.OnGameStarted()
    {
        StartCoroutine(StartSpawning());
    }

    private IEnumerator StartSpawning()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            var enemy = _enemyPool.GetEnemy();

            if (enemy != null)
            {
                OnEnemySpawned?.Invoke(enemy.GetComponent<HitPointsComponent>(), enemy.GetComponent<EnemyAttackAgent>());
            }
        }
    }
}
