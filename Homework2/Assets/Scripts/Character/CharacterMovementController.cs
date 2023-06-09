using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(MoveComponent))]
    public class CharacterMovementController : MonoBehaviour, IGameLoadingListener
    {
        [SerializeField] MoveComponent _moveComponent;

        private InputManager _inputManager;
        private Vector2 _movementVector;

        private void Awake()
        {
            FindObjectOfType<GameManager>().AddListener(this);
            ServiceLocator.Shared.AddService(this);
        }

        public void OnGameLoading()
        {
            _inputManager = ServiceLocator.Shared.GetService<InputManager>();
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
