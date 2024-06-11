using Assets.Scripts.Infrastructure.Bootstrap;
using Assets.Scripts.Infrastructure.Services.Input;

namespace Assets.Scripts.Infrastructure.GameStates
{
    public class BootstrapState : IState
    {
        private const string InitialScene = "Initial";

        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;

        public BootstrapState(GameStateMachine gameStateMachine, SceneLoader sceneLoader)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter()
        {
            RegisterServices();
            _sceneLoader.Load(InitialScene, EnterLoadLevel);
        }

        public void Exit()
        {

        }

        private void EnterLoadLevel() => 
            _gameStateMachine.Enter<LoadLevelState, string>("BattleField");

        private void RegisterServices()
        {
            Game.InputService = new StandaloneInput();
        }
    }
}
