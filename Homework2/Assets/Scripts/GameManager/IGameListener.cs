using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameListener { }

public interface IGameResolveDependenciesListener : IGameListener
{
    public void OnGameResolvingDependencies();
}

public interface IGameStartListener : IGameListener
{
    public void OnGameStarted();
}

public interface IGameFinishListener : IGameListener
{
    public void OnGameFinished();
}