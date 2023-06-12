using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(MoveComponent))]
    public class CharacterMovementController : MonoBehaviour, IGameResolveDependenciesListener
    {
        [SerializeField] MoveComponent _moveComponent;

        private InputMovementManager _inputMovementManager;
        private Vector2 _movementVector;

        void IGameResolveDependenciesListener.OnGameResolvingDependencies()
        {
            _inputMovementManager = ServiceLocator.Shared.GetService<InputMovementManager>();
            _inputMovementManager.OnHorizontalDirectionChanged += ChangeMovementVector;
        }

        private void OnDestroy()
        {
            _inputMovementManager.OnHorizontalDirectionChanged -= ChangeMovementVector;
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
