using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class GameManager : MonoBehaviour
    {
        private List<IGameListener> _listeners;

        public GameState State => _state;

        private GameState _state;


        public void AddListener(IGameListener listener)
        {
            _listeners.Add(listener);
        }

        public void StartLoading()
        {
            foreach (var listener in _listeners)
            {
                if (listener is IGameLoadingListener loadingListener)
                {
                    loadingListener.OnGameLoading();
                }
            }

            _state = GameState.Loading;
        }

        public void StartGame()
        {
            foreach (var listener in _listeners)
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
            foreach (var listener in _listeners)
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

        private void Awake()
        { 
            _listeners = new List<IGameListener>();
            ServiceLocator.Shared.AddService(this);
        }

        private void Start()
        {
            StartLoading();
            StartGame();
        }
    }
}