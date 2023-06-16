using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public class BulletSpawner : MonoBehaviour, IGameResolveDependenciesListener
    {
        [SerializeField] private Bullet _prefab;
        [SerializeField] private Transform _worldTransform;

        public Action<Bullet> OnBulletSpawned;

        private BulletPool _bulletPool;

        void IGameResolveDependenciesListener.OnGameResolvingDependencies()
        {
            _bulletPool = ServiceLocator.Shared.GetService<BulletPool>();
        }

        public Bullet SpawnBulletIn(Transform container)
        {
            return Instantiate(_prefab, container);
        }

        public void FlyBulletByArgs(BulletData data)
        {
            var bullet = _bulletPool.Bullet();

            bullet.transform.SetParent(_worldTransform);

            bullet.SetData(data);
            
            OnBulletSpawned?.Invoke(bullet);
        }

        internal void RemoveBullet(Bullet bullet)
        {
            _bulletPool.RemoveBullet(bullet);
        }
    }
}