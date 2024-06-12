using Assets.Scripts.Infrastructure.Services;
using UnityEngine;

namespace Assets.Scripts.Infrastructure
{
    public interface IGameFactory : IService
    {
        GameObject CreatePlayer(GameObject initialPoint);
        GameObject CreateScoreDisplay();
    }
}