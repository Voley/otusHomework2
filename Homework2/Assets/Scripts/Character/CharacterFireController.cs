using ShootEmUp;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{

    [RequireComponent(typeof(WeaponComponent))]
    public class CharacterFireController : MonoBehaviour
    {
        [SerializeField] private BulletSystem _bulletSystem;
        [SerializeField] private BulletConfig _bulletConfig;
        [SerializeField] private InputManager _inputManager;
        [SerializeField] private WeaponComponent _weaponComponent;

        private bool _shouldShootNextFrame;

        private void Awake()
        {
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
            _bulletSystem.FlyBulletByArgs(bulletData);
        }
    }
}