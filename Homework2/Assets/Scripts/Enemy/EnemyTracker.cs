using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyTracker : MonoBehaviour, IGameResolveDependenciesListener
    {
        private BulletTracker _bulletTracker;
        private EnemyPool _enemyPool;
        private TimedEnemySpawner _timedEnemySpawner;

        private HashSet<GameObject> _activeEnemies = new();

        void IGameResolveDependenciesListener.OnGameResolvingDependencies()
        {
            _timedEnemySpawner = ServiceLocator.Shared.GetService<TimedEnemySpawner>();
            _timedEnemySpawner.OnEnemySpawned += StartTrackingEnemy;

            _bulletTracker = ServiceLocator.Shared.GetService<BulletTracker>();
            _enemyPool = ServiceLocator.Shared.GetService<EnemyPool>();
        }

        private void OnDestroy()
        {
            _timedEnemySpawner.OnEnemySpawned -= StartTrackingEnemy;
        }

        private void StartTrackingEnemy(HitPointsComponent hitpoints, EnemyAttackAgent agent)
        {
            if (_activeEnemies.Add(hitpoints.gameObject))
            {
                hitpoints.OnHpEmpty += OnDestroyed;
                agent.OnFire += OnFire;
            }
        }

        private void OnDestroyed(GameObject enemy)
        {
            if (_activeEnemies.Remove(enemy))
            {
                enemy.GetComponent<HitPointsComponent>().OnHpEmpty -= OnDestroyed;
                enemy.GetComponent<EnemyAttackAgent>().OnFire -= OnFire;

                _enemyPool.DespawnEnemy(enemy);
            }
        }

        private void OnFire(GameObject enemy, Vector2 position, Vector2 direction)
        {
            _bulletTracker.FlyBulletByArgs(new BulletData
            {
                isPlayer = false,
                physicsLayer = (int) PhysicsLayer.ENEMY,
                color = Color.red,
                damage = 1,
                position = position,
                velocity = direction * 2.0f
            });
        }
    }
}