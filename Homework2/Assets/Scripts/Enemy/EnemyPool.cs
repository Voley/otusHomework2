using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyPool : MonoBehaviour, IGameResolveDependenciesListener
    {
        [Header("Spawn")]
        [SerializeField] private EnemyPositions _enemyPositions;

        [SerializeField] private GameObject _character;

        [SerializeField] private Transform _worldTransform;

        [Header("Pool")]
        [SerializeField] private Transform _container;

        private EnemySpawner _enemySpawner;

        private readonly Queue<GameObject> _enemyQueue = new();
        
        void IGameResolveDependenciesListener.OnGameResolvingDependencies()
        {
            _enemySpawner = ServiceLocator.Shared.GetService<EnemySpawner>();

            for (var i = 0; i < 7; i++)
            {
                var enemy = _enemySpawner.SpawnEnemyIn(_container);
                _enemyQueue.Enqueue(enemy);
            }
        }

        public GameObject GetEnemy()
        {
            if (!_enemyQueue.TryDequeue(out var enemy))
            {
                return null;
            }

            enemy.transform.SetParent(_worldTransform);

            var spawnPosition = _enemyPositions.RandomSpawnPosition();
            enemy.transform.position = spawnPosition.position;
            
            var attackPosition = _enemyPositions.RandomAttackPosition();
            enemy.GetComponent<EnemyMoveAgent>().SetDestination(attackPosition.position);

            enemy.GetComponent<EnemyAttackAgent>().SetTarget(_character);

            return enemy;
        }

        public void DespawnEnemy(GameObject enemy)
        {
            enemy.transform.SetParent(_container);
            _enemyQueue.Enqueue(enemy);
        }
    }
}