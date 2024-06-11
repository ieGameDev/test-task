using Assets.Scripts.Infrastructure.GameStates;
using Assets.Scripts.Infrastructure.Services.Input;
using Assets.Scripts.Logic;
using Unity.VisualScripting;

namespace Assets.Scripts.Infrastructure.Bootstrap
{
    public class Game
    {
        public static IInputService InputService;
        public GameStateMachine StateMachine;

        public Game(ICoroutineRunner coroutineRunner, LoadingScreen loadingScreen)
        {
            StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), loadingScreen);
        }
    }
}