using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class GameManager : MonoBehaviour
    {
        public GameState State => _state;
        private GameState _state;

        public void ResolveDependencies()
        {
            foreach (var listener in ServiceLocator.Shared.GetServices<IGameResolveDependenciesListener>())
            {
                if (listener is IGameResolveDependenciesListener resolver)
                {
                    resolver.OnGameResolvingDependencies();
                }
            }

            _state = GameState.ResolvingDependencies;
        }

        public void StartGame()
        {
            foreach (var listener in ServiceLocator.Shared.GetServices<IGameStartListener>())
            {
                if (listener is IGameStartListener startListener)
                {
                    startListener.OnGameStarted();
                }
            }

            _state = GameState.Playing;
        }

        public void FinishGame()
        {
            foreach (var listener in ServiceLocator.Shared.GetServices<IGameFinishListener>())
            {
                if (listener is IGameFinishListener finishListener)
                {
                    finishListener.OnGameFinished();
                }
            }

            _state = GameState.Finished;

            Debug.Log("Game over!");
            Time.timeScale = 0;
        }

        private void Start()
        {
            ResolveDependencies();
            StartGame();
        }
    }
}