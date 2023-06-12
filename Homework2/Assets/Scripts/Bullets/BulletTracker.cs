using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class BulletTracker : MonoBehaviour, IGameResolveDependenciesListener
    {
        [SerializeField] private LevelBounds _levelBounds;
        [SerializeField] private Transform _worldTransform;

        private readonly HashSet<Bullet> _activeBullets = new();
        private readonly List<Bullet> _bulletCache = new();
        
        private BulletPool _bulletPool;

        void IGameResolveDependenciesListener.OnGameResolvingDependencies()
        {
            _bulletPool = ServiceLocator.Shared.GetService<BulletPool>();
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

        public void FlyBulletByArgs(BulletData data)
        {
            var bullet = _bulletPool.Bullet();

            bullet.transform.SetParent(_worldTransform);

            bullet.SetData(data);
            
            if (_activeBullets.Add(bullet))
            {
                bullet.OnCollisionEntered += OnBulletCollision;
            }
        }
        
        private void OnBulletCollision(Bullet bullet, Collision2D collision)
        {
            BulletUtils.DealDamage(bullet, collision.gameObject);
            RemoveBullet(bullet);
        }

        private void RemoveBullet(Bullet bullet)
        {
            if (_activeBullets.Remove(bullet))
            {
                bullet.OnCollisionEntered -= OnBulletCollision;
            }

            _bulletPool.RemoveBullet(bullet);
        }
    }
}