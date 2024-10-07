using Assets.Scripts.Infrastructure.GameStates;
using Assets.Scripts.Logic;
using UnityEngine;

namespace Assets.Scripts.Infrastructure.Bootstrap
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        public LoadingScreen LoadingScreen;

        private Game _game;

        private void Awake()
        {
            _game = new Game(this, LoadingScreen);
            _game.StateMachine.Enter<BootstrapState>();

            DontDestroyOnLoad(this);
        }
    }
}