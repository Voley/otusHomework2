using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public class BulletSpawner : MonoBehaviour
    {
        [SerializeField] private Bullet _prefab;

        public Bullet SpawnBulletIn(Transform container)
        {
            return Instantiate(_prefab, container);
        }
    }
}