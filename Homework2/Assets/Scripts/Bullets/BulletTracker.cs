using System;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class BulletTracker : MonoBehaviour, IGameResolveDependenciesListener
    {
        [SerializeField] private LevelBounds _levelBounds;

        private readonly HashSet<Bullet> _activeBullets = new();
        private readonly List<Bullet> _bulletCache = new();

        private BulletSpawner _bulletSpawner;

        void IGameResolveDependenciesListener.OnGameResolvingDependencies()
        {
            _bulletSpawner = ServiceLocator.Shared.GetService<BulletSpawner>();
            _bulletSpawner.OnBulletSpawned += BulletSpawned;
        }

        private void FixedUpdate()
        {
            _bulletCache.Clear();
            _bulletCache.AddRange(_activeBullets);

            for (int i = 0, count = _bulletCache.Count; i < count; i++)
            {
                var bullet = _bulletCache[i];

                if (!_levelBounds.InBounds(bullet.transform.position))
                {
                    RemoveBullet(bullet);
                }
            }
        }
        
        private void OnBulletCollision(Bullet bullet, Collision2D collision)
        {
            BulletUtils.DealDamage(bullet, collision.gameObject);
            RemoveBullet(bullet);
        }

        private void BulletSpawned(Bullet bullet)
        {
            if (_activeBullets.Add(bullet))
            {
                bullet.OnCollisionEntered += OnBulletCollision;
            }
        }

        private void RemoveBullet(Bullet bullet)
        {
            if (_activeBullets.Remove(bullet))
            {
                bullet.OnCollisionEntered -= OnBulletCollision;
            }

            _bulletSpawner.RemoveBullet(bullet);
        }
    }
}