using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public class BulletSpawner : MonoBehaviour
    {
        [SerializeField] private Bullet _prefab;

        private void Awake()
        {
            ServiceLocator.Shared.AddService(this);
        }

        public Bullet SpawnBulletIn(Transform container)
        {
            return Instantiate(_prefab, container);
        }
    }
}