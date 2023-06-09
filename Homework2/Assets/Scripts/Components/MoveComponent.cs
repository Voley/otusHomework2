using UnityEngine;

namespace ShootEmUp
{
    public sealed class MoveComponent : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody2D _rigidbody2D;

        [SerializeField]
        private float _speed = 5.0f;
        
        public void MoveByVelocity(Vector2 vector)
        {
            var nextPosition = GetComponent<Rigidbody2D>().position + vector * _speed;
            GetComponent<Rigidbody2D>().MovePosition(nextPosition);
        }
    }
}