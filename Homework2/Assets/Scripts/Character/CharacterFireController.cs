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

        private BulletSpawner _bulletSpawner;
        private InputFireManager _inputFireManager;

        private bool _shouldShootNextFrame;

        void IGameResolveDependenciesListener.OnGameResolvingDependencies()
        {
            _inputFireManager = ServiceLocator.Shared.GetService<InputFireManager>();
            _inputFireManager.OnFirePressed += SetShouldShootNextFixedUpdate;
            _bulletSpawner = ServiceLocator.Shared.GetService<BulletSpawner>();
        }

        private void OnDestroy()
        {
            _inputFireManager.OnFirePressed -= SetShouldShootNextFixedUpdate;
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
            BulletData bulletData = BulletData.PlayerBulletData(_bulletConfig, _weaponComponent.Position, velocity);
            _bulletSpawner.FlyBulletByArgs(bulletData);
        }
    }
}