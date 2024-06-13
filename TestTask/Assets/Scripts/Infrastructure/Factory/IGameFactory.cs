using Assets.Scripts.Infrastructure.Services;
using System;
using UnityEngine;

namespace Assets.Scripts.Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        GameObject PlayerGameObject { get; }
        event Action PlayerCreated;

        GameObject CreatePlayer(GameObject initialPoint);
        GameObject CreateScoreDisplay();
    }
}