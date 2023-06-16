using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyTracker : MonoBehaviour, IGameResolveDependenciesListener
    {
        private BulletSpawner _bulletSpawner;
        private TimedEnemySpawner _timedEnemySpawner;
        private EnemySpawner _enemySpawner;
        private HashSet<GameObject> _activeEnemies = new();

        void IGameResolveDependenciesListener.OnGameResolvingDependencies()
        {
            _enemySpawner = ServiceLocator.Shared.GetService<EnemySpawner>();
            _enemySpawner.OnEnemySpawned += StartTrackingEnemy;

            _bulletSpawner = ServiceLocator.Shared.GetService<BulletSpawner>();
        }

        private void OnDestroy()
        {
            _enemySpawner.OnEnemySpawned -= StartTrackingEnemy;
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
                _timedEnemySpawner.RemoveEnemy(enemy);
            }
        }

        private void OnFire(GameObject enemy, Vector2 position, Vector2 direction)
        {
            _bulletSpawner.FlyBulletByArgs(BulletData.EnemyBulletData(position, direction));
        }
    }
}