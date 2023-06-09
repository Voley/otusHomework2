using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class Bullet : MonoBehaviour
    {
        public bool IsPlayer => _isPlayer;
        public int Damage => _damage;

        public event Action<Bullet, Collision2D> OnCollisionEntered;

        [SerializeField]
        private Rigidbody2D _rigidbody2D;

        [SerializeField]
        private SpriteRenderer spriteRenderer;

        private bool _isPlayer;
        private int _damage;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            OnCollisionEntered?.Invoke(this, collision);
        }

        public void SetVelocity(Vector2 velocity)
        {
            _rigidbody2D.velocity = velocity;
        }

        public void SetPhysicsLayer(int physicsLayer)
        {
            gameObject.layer = physicsLayer;
        }

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        public void SetColor(Color color)
        {
            spriteRenderer.color = color;
        }

        internal void SetData(BulletData data)
        {
            SetPosition(data.position);
            SetColor(data.color);
            SetPhysicsLayer(data.physicsLayer);
            _damage = data.damage;
            _isPlayer = data.isPlayer;
            SetVelocity(data.velocity);
        }
    }
}