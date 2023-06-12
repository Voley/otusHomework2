using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class InputManager : MonoBehaviour, IGameResolveDependenciesListener
    {
        public Action OnFirePressed;
        public Action<float> OnHorizontalDirectionChanged;

        private GameManager _gameManager;

        public void OnGameResolvingDependencies()
        {
            _gameManager = ServiceLocator.Shared.GetService<GameManager>();
        }

        private void Update()
        {
            if (_gameManager == null || _gameManager.State != GameState.Playing) return;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                OnFirePressed?.Invoke();
            }

            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                OnHorizontalDirectionChanged?.Invoke(-1f);
            }
            else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                OnHorizontalDirectionChanged?.Invoke(1f);
            }
            else
            {
                OnHorizontalDirectionChanged?.Invoke(0f);
            }
        }
    }
}