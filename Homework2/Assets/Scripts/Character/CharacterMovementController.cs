using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(MoveComponent))]
    public class CharacterMovementController : MonoBehaviour
    {
        [SerializeField] InputManager _inputManager;
        [SerializeField] MoveComponent _moveComponent;

        private Vector2 _movementVector;

        private void Awake()
        {
            _inputManager.OnHorizontalDirectionChanged += ChangeMovementVector;
        }

        private void OnDestroy()
        {
            _inputManager.OnHorizontalDirectionChanged -= ChangeMovementVector;
        }

        private void ChangeMovementVector(float horizontalDirection)
        {
            _movementVector = new Vector2(horizontalDirection, 0);
        }

        private void FixedUpdate()
        {
            _moveComponent.MoveByVelocity(_movementVector * Time.fixedDeltaTime);

        }
    }
}
