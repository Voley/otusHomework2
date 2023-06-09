using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class BulletSystem : MonoBehaviour, IGameLoadingListener
    {
        [SerializeField]
        private int _initialCount = 50;
        
        [SerializeField] private Transform _container;
        [SerializeField] private Transform _worldTransform;
        [SerializeField] private LevelBounds _levelBounds;

        [SerializeField] private BulletSpawner _bulletSpawner;

        private readonly Queue<Bullet> _bulletPool = new();
        private readonly HashSet<Bullet> _activeBullets = new();
        private readonly List<Bullet> _bulletCache = new();
        
        private void Awake()
        {
            FindObjectOfType<GameManager>().AddListener(this);
            ServiceLocator.Shared.AddService(this);
        }

        public void OnGameLoading()
        {
            for (var i = 0; i < _initialCount; i++)
            {
                var bullet = _bulletSpawner.SpawnBulletIn(_container);
                _bulletPool.Enqueue(bullet);
            }
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
            if (_bulletPool.TryDequeue(out var bullet))
            {
                bullet.transform.SetParent(_worldTransform);
            }
            else
            {
                bullet = _bulletSpawner.SpawnBulletIn(_worldTransform);
            }

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
                bullet.transform.SetParent(_container);
                _bulletPool.Enqueue(bullet);
            }
        }
    }
}