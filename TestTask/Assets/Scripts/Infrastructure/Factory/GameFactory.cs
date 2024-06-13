using Assets.Scripts.Infrastructure.AssetsManagement;
using Assets.Scripts.Logic;
using Assets.Scripts.UI;
using System;
using UnityEngine;

namespace Assets.Scripts.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assets;
        public GameObject PlayerGameObject { get; set; }

        public event Action PlayerCreated;

        public GameFactory(IAssetProvider assets)
        {
            _assets = assets;
        }

        public GameObject CreatePlayer(GameObject initialPoint)
        {
            PlayerGameObject = _assets.Instantiate(AssetPath.PlayerPath, initialPoint.transform.position + Vector3.up * 1f);
            PlayerCreated?.Invoke();
            return PlayerGameObject;
        }

        public GameObject CreateScoreDisplay()
        {
            GameObject scoreDisplay = _assets.Instantiate(AssetPath.ScoreDisplayPath);
            return scoreDisplay;
        }
    }
}
