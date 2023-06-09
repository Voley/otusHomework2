using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameListener { }

public interface IGameLoadingListener : IGameListener
{
    public void OnGameLoading();
}

public interface IGameStartListener : IGameListener
{
    public void OnGameStarted();
}

public interface IGameFinishListener : IGameListener
{
    public void OnGameFinished();
}