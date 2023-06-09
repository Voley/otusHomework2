using UnityEngine;

namespace ShootEmUp
{
    public sealed class CharacterController : MonoBehaviour, IGameLoadingListener
    {
        [SerializeField] private HitPointsComponent _characterHealth;

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