using Assets.Scripts.CameraLogic;
using Assets.Scripts.Infrastructure.Bootstrap;
using Assets.Scripts.Logic;
using UnityEngine;

namespace Assets.Scripts.Infrastructure.GameStates
{
    public class LoadLevelState : IPayLoadedState<string>
    {        
        private const string InitialPointTag = "PlayerInitialPoint";

        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingScreen _loadingScreen;
        private readonly IGameFactory _gameFactory;

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingScreen loadingScreen, IGameFactory gameFactory)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _loadingScreen = loadingScreen;
            _gameFactory = gameFactory;
        }

        public void Enter(string sceneName)
        {
            _loadingScreen.Show();
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit() =>
            _loadingScreen.Hide();

        private void OnLoaded()
        {
            GameObject player = _gameFactory.CreatePlayer(GameObject.FindWithTag(InitialPointTag));

            _gameFactory.CreateScoreDisplay();

            CameraFollow(player);

            _gameStateMachine.Enter<GameLoopState>();
        }

        private void CameraFollow(GameObject player)
        {
            Camera.main
                .GetComponent<CameraFollow>()
                .Follow(player);
        }
    }
}
