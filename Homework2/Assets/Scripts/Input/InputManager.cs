using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class InputManager : MonoBehaviour
    {
        public Action OnFirePressed;
        public Action<float> OnHorizontalDirectionChanged;

        [SerializeField]
        private CharacterController characterController;

        private void Update()
        {
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