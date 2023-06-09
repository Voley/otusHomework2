using UnityEngine;

namespace ShootEmUp
{
    public sealed class CharacterController : MonoBehaviour
    {
        [SerializeField] private GameManager _gameManager;
        [SerializeField] private HitPointsComponent _characterHealth;

        private void Awake()
        {
            
        }

        private void OnEnable()
        {
            _characterHealth.OnHpEmpty += OnCharacterDeath;
        }

        private void OnDestroy()
        {
            _characterHealth.OnHpEmpty -= OnCharacterDeath;
        }

        private void OnCharacterDeath(GameObject _)
        {
            _gameManager.FinishGame();
        }
    }
}