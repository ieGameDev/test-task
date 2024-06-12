using Assets.Scripts.Infrastructure.GameStates;
using Assets.Scripts.Infrastructure.Services;
using Assets.Scripts.Logic;

namespace Assets.Scripts.Infrastructure.Bootstrap
{
    public class Game
    {
        public GameStateMachine StateMachine;

        public Game(ICoroutineRunner coroutineRunner, LoadingScreen loadingScreen)
        {
            StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), loadingScreen, DependencyContainer.Container);
        }
    }
}