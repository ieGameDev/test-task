using Assets.Scripts.Infrastructure.AssetsManagement;
using UnityEngine;

namespace Assets.Scripts.Infrastructure
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assets;

        public GameFactory(IAssetProvider assets)
        {
            _assets = assets;
        }

        public GameObject CreatePlayer(GameObject initialPoint) => 
            _assets.Instantiate(AssetPath.PlayerPath, initialPoint.transform.position + Vector3.up * 1f);

        public GameObject CreateScoreDisplay() => 
            _assets.Instantiate(AssetPath.ScoreDisplayPath);
    }
}
