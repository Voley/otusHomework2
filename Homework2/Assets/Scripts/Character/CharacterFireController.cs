using ShootEmUp;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(WeaponComponent))]
    public class CharacterFireController : MonoBehaviour, IGameResolveDependenciesListener
    {
        [SerializeField] private BulletConfig _bulletConfig;
        [SerializeField] private WeaponComponent _weaponComponent;

        private BulletTracker _bulletTracker;
        private InputManager _inputManager;

        private bool _shouldShootNextFrame;

        public void OnGameResolvingDependencies()
        {
            _inputManager = ServiceLocator.Shared.GetService<InputManager>();
            _bulletTracker = ServiceLocator.Shared.GetService<BulletTracker>();
            _inputManager.OnFirePressed += SetShouldShootNextFixedUpdate;
        }

        private void OnDestroy()
        {
            _inputManager.OnFirePressed -= SetShouldShootNextFixedUpdate;
        }

        private void FixedUpdate()
        {
            if (_shouldShootNextFrame)
            {
                Shoot();
                _shouldShootNextFrame = false;
            }
        }

        private void SetShouldShootNextFixedUpdate()
        {
            _shouldShootNextFrame = true;
        }

        private void Shoot()
        {
            Vector2 velocity = _weaponComponent.Rotation * (Vector3.up * _bulletConfig.speed);
            BulletData bulletData = BulletData.BulletWithConfig(_bulletConfig, _weaponComponent.Position, velocity, true);
            _bulletTracker.FlyBulletByArgs(bulletData);
        }
    }
}