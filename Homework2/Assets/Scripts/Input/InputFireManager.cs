using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class InputFireManager : MonoBehaviour, IGameStartListener, IGameFinishListener
    {
        public Action OnFirePressed;

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
            if (Input.GetKeyDown(KeyCode.Space))
            {
                OnFirePressed?.Invoke();
            }
        }
    }
}