using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class InputManager : MonoBehaviour, IGameLoadingListener
    {
        public Action OnFirePressed;
        public Action<float> OnHorizontalDirectionChanged;

        private GameManager _gameManager;

        private void Awake()
        {
            FindObjectOfType<GameManager>().AddListener(this);
            ServiceLocator.Shared.AddService(this);
        }

        public void OnGameLoading()
        {
            _gameManager = ServiceLocator.Shared.GetService<GameManager>();
        }

        private void Update()
        {
            if (_gameManager == null || _gameManager.State != GameState.Playing) return;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                OnFirePressed?.Invoke();
                Debug.Log("Space pressed");
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