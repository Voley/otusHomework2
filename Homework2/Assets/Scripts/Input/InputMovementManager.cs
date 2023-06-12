using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class InputMovementManager : MonoBehaviour, IGameStartListener, IGameFinishListener
    {
        public Action<float> OnHorizontalDirectionChanged;

        private void Awake()
        {
            enabled = false;
        }

        void IGameStartListener.OnGameStarted()
        {
            enabled = true;
        }

        void IGameFinishListener.OnGameFinished()
        {
            enabled = false;
        }

        private void Update()
        {
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