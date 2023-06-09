using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class HitPointsComponent : MonoBehaviour
    {
        public event Action<GameObject> OnHpEmpty;
        public bool IsAlive => _hitPoints > 0;
        
        [SerializeField] private int _hitPoints;

        public void TakeDamage(int damage)
        {
            _hitPoints -= damage;

            if (_hitPoints <= 0)
            {
                OnHpEmpty?.Invoke(gameObject);
            }
        }
    }
}