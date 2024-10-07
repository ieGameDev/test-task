using Assets.Scripts.Infrastructure.Services;
using Assets.Scripts.Infrastructure.Services.ProgressService;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        GameObject PlayerGameObject { get; }
        List<ISavedProgressReader> ProgressReaders { get; }
        List<ISavedProgress> ProgressWriters { get; }

        GameObject CreatePlayer(GameObject initialPoint);
        GameObject CreateEnemy(GameObject initialPoint);
        GameObject CreateScoreDisplay();

        void CleanUp();
    }
}