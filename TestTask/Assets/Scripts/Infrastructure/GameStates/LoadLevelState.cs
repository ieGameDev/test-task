using Assets.Scripts.CameraLogic;
using Assets.Scripts.Infrastructure.Bootstrap;
using Assets.Scripts.Infrastructure.Factory;
using Assets.Scripts.Logic;
using Assets.Scripts.Player;
using Assets.Scripts.UI;
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
            GameObject player = InitialPlayer();

            InitialScoreDisplay(player);

            CameraFollow(player);

            _gameStateMachine.Enter<GameLoopState>();
        }

        private GameObject InitialPlayer() => 
            _gameFactory.CreatePlayer(GameObject.FindWithTag(InitialPointTag));

        private void InitialScoreDisplay(GameObject player)
        {
            GameObject score = _gameFactory.CreateScoreDisplay();
            score.GetComponent<ActorUI>().Construct(player.GetComponent<PlayerScoreCounter>());
        }

        private void CameraFollow(GameObject player)
        {
            Camera.main
                .GetComponent<CameraFollow>()
                .Follow(player);
        }
    }
}
