using ShootEmUp;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour, IGameResolveDependenciesListener
{
    [SerializeField] private Transform _container;
    [SerializeField] private int _initialCount = 50;

    private Queue<Bullet> _bulletQueue;
    private BulletSpawner _bulletSpawner;

    public Bullet Bullet()
    {
        if (_bulletQueue.TryDequeue(out var bullet))
        {
            return bullet;
        }

        var extraBullet = _bulletSpawner.SpawnBulletIn(_container);

        return extraBullet;
    }

    public void OnGameResolvingDependencies()
    {
        _bulletQueue = new Queue<Bullet>(_initialCount);
        _bulletSpawner = ServiceLocator.Shared.GetService<BulletSpawner>();

        for (var i = 0; i < _initialCount; i++)
        {
            var bullet = _bulletSpawner.SpawnBulletIn(_container);
            _bulletQueue.Enqueue(bullet);
        }
    }

    public void RemoveBullet(Bullet bullet)
    {
        bullet.transform.SetParent(_container);
        _bulletQueue.Enqueue(bullet);
    }
}
