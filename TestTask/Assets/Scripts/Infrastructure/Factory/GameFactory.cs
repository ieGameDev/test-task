using Assets.Scripts.Enemy;
using Assets.Scripts.Infrastructure.AssetsManagement;
using Assets.Scripts.Infrastructure.Services.ProgressService;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assets;

        public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
        public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();

        public GameObject PlayerGameObject { get; set; }

        public GameFactory(IAssetProvider assets)
        {
            _assets = assets;
        }

        public GameObject CreatePlayer(GameObject initialPoint)
        {
            PlayerGameObject = _assets.Instantiate(AssetPath.PlayerPath, initialPoint.transform.position + Vector3.up * 1f);
            RegisterProgressWatchers(PlayerGameObject);

            return PlayerGameObject;
        }

        public GameObject CreateEnemy(GameObject initialPoint)
        {
            GameObject enemy = _assets.Instantiate(AssetPath.EnemyPath, initialPoint.transform.position + Vector3.up * 1f);
            enemy.GetComponent<EnemyMoveToPlayer>().Construct(PlayerGameObject);
            enemy.GetComponent<EnemyAttack>().Construct(PlayerGameObject);

            RegisterProgressWatchers(enemy);

            return enemy;
        }

        public GameObject CreateScoreDisplay()
        {
            GameObject scoreDisplay = _assets.Instantiate(AssetPath.ScoreDisplayPath);
            RegisterProgressWatchers(scoreDisplay);
            return scoreDisplay;
        }

        public void CleanUp()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();
        }

        private void RegisterProgressWatchers(GameObject scoreDisplay)
        {
            foreach (var progressReader in scoreDisplay.GetComponentsInChildren<ISavedProgressReader>())
                Register(progressReader);
        }

        private void Register(ISavedProgressReader progressReader)
        {
            if (progressReader is ISavedProgress progressWriter)
                ProgressWriters.Add(progressWriter);

            ProgressReaders.Add(progressReader);
        }
    }
}
