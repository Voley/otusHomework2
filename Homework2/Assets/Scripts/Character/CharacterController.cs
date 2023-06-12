using UnityEngine;

namespace ShootEmUp
{
    public sealed class CharacterController : MonoBehaviour, IGameResolveDependenciesListener
    {
        [SerializeField] private HitPointsComponent _characterHealth;

        private GameManager _gameManager;

        public void OnGameResolvingDependencies()
        {
            _gameManager = ServiceLocator.Shared.GetService<GameManager>();
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