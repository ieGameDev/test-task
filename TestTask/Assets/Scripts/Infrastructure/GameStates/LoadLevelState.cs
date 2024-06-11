using Assets.Scripts.CameraLogic;
using Assets.Scripts.Infrastructure.Bootstrap;
using Assets.Scripts.Logic;
using UnityEngine;

namespace Assets.Scripts.Infrastructure.GameStates
{
    public class LoadLevelState : IPayLoadedState<string>
    {
        private const string PlayerPath = "Characters/Player";
        private const string ScoreDisplayPath = "UI/ScoreDisplay";
        private const string InitialPointTag = "PlayerInitialPoint";

        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingScreen _loadingScreen;

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingScreen loadingScreen)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _loadingScreen = loadingScreen;
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
            GameObject initialPoint = GameObject.FindWithTag(InitialPointTag);

            GameObject player = Instantiate(PlayerPath, initialPoint.transform.position + Vector3.up * 1f);

            Instantiate(ScoreDisplayPath);

            CameraFollow(player);

            _gameStateMachine.Enter<GameLoopState>();
        }

        private void CameraFollow(GameObject player)
        {
            Camera.main
                .GetComponent<CameraFollow>()
                .Follow(player);
        }

        private static GameObject Instantiate(string path)
        {
            GameObject prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab);
        }

        private static GameObject Instantiate(string path, Vector3 initialPoint)
        {
            GameObject prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab, initialPoint, Quaternion.identity);
        }
    }
}
