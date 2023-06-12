using ShootEmUp;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public class ServiceLocatorInstaller : MonoBehaviour
    {
        [SerializeField] private GameManager _gameManager;
        [SerializeField] private InputManager _inputManager;
        [SerializeField] private BulletSpawner _bulletSpawner;
        [SerializeField] private BulletTracker _bulletTracker;
        [SerializeField] private BulletPool _bulletPool;
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private CharacterFireController _characterFireController;
        [SerializeField] private CharacterMovementController _characterMovementController;
        [SerializeField] private EnemyPool _enemyPool;
        [SerializeField] private EnemySpawner _enemySpawner;
        [SerializeField] private EnemyTracker _enemyTracker;
        [SerializeField] private TimedEnemySpawner _timedEnemySpawner;

        private void Awake()
        {
            ServiceLocator.Shared.AddService(_gameManager);
            ServiceLocator.Shared.AddService(_inputManager);
            ServiceLocator.Shared.AddService(_bulletSpawner);
            ServiceLocator.Shared.AddService(_bulletTracker);
            ServiceLocator.Shared.AddService(_bulletPool);
            ServiceLocator.Shared.AddService(_characterController);
            ServiceLocator.Shared.AddService(_characterFireController);
            ServiceLocator.Shared.AddService(_characterMovementController);
            ServiceLocator.Shared.AddService(_enemyPool);
            ServiceLocator.Shared.AddService(_enemySpawner);
            ServiceLocator.Shared.AddService(_enemyTracker);
            ServiceLocator.Shared.AddService(_timedEnemySpawner);
        }
    }
}