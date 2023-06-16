using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyPool : MonoBehaviour, IGameResolveDependenciesListener
    {
        [SerializeField] private GameObject _character;
        [SerializeField] private Transform _worldTransform;
        [SerializeField] private Transform _container;
        [SerializeField] private int _enemyPoolSize;

        private EnemySpawner _enemySpawner;

        private readonly Queue<GameObject> _enemyQueue = new();
        
        void IGameResolveDependenciesListener.OnGameResolvingDependencies()
        {
            _enemySpawner = ServiceLocator.Shared.GetService<EnemySpawner>();

            for (var i = 0; i < _enemyPoolSize; i++)
            {
                var enemy = _enemySpawner.SpawnEnemy();
                enemy.transform.SetParent(_container);
                _enemyQueue.Enqueue(enemy);
            }
        }

        public GameObject GetEnemy()
        {
            if (!_enemyQueue.TryDequeue(out var enemy))
            {
                var spawnedEnemy = _enemySpawner.SpawnEnemy();
                spawnedEnemy.transform.SetParent(_worldTransform);
                return spawnedEnemy;
            }

            enemy.transform.parent = _worldTransform;
            return enemy;
        }

        public void RemoveEnemy(GameObject enemy)
        {
            enemy.transform.SetParent(_container);
            _enemyQueue.Enqueue(enemy);
        }
    }
}