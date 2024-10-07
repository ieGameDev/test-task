using Assets.Scripts.CameraLogic;
using Assets.Scripts.Enemy;
using Assets.Scripts.Infrastructure.Bootstrap;
using Assets.Scripts.Infrastructure.Factory;
using Assets.Scripts.Infrastructure.Services.ProgressService;
using Assets.Scripts.Logic;
using Assets.Scripts.Player;
using Assets.Scripts.UI;
using UnityEngine;

namespace Assets.Scripts.Infrastructure.GameStates
{
    public class LoadLevelState : IPayLoadedState<string>
    {        
        private const string PlayerInitialPointTag = "PlayerInitialPoint";
        private const string EnemyInitialPointTag = "EnemyInitialPoint";

        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingScreen _loadingScreen;
        private readonly IGameFactory _gameFactory;
        private readonly IProgressService _progressService;

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingScreen loadingScreen, IGameFactory gameFactory, IProgressService progressService)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _loadingScreen = loadingScreen;
            _gameFactory = gameFactory;
            _progressService = progressService;
        }

        public void Enter(string sceneName)
        {
            _loadingScreen.Show();
            _gameFactory.CleanUp();
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit() =>
            _loadingScreen.Hide();

        private void OnLoaded()
        {
            GameObject player = InitialPlayer();
            CameraFollow(player);
            GameObject enemy = InitialEnemy();
            InitialScoreDisplay(player, enemy);

            InformProgressReaders();

            _gameStateMachine.Enter<GameLoopState>();
        }

        private void InformProgressReaders()
        {
            foreach (var progressReader in _gameFactory.ProgressReaders)
                progressReader.LoadProgress(_progressService.Progress);
        }

        private GameObject InitialPlayer() => 
            _gameFactory.CreatePlayer(GameObject.FindWithTag(PlayerInitialPointTag));

        private GameObject InitialEnemy() => 
            _gameFactory.CreateEnemy(GameObject.FindWithTag(EnemyInitialPointTag));

        private void InitialScoreDisplay(GameObject player, GameObject enemy)
        {
            GameObject score = _gameFactory.CreateScoreDisplay();

            score.GetComponent<ActorUI>()
                .Construct(player.GetComponent<PlayerScoreCounter>(), enemy.GetComponent<EnemyScoreCounter>());
        }

        private void CameraFollow(GameObject player)
        {
            Camera.main
                .GetComponent<CameraFollow>()
                .Follow(player);
        }
    }
}
