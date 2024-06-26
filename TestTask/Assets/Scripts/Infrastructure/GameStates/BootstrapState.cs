﻿using Assets.Scripts.Infrastructure.AssetsManagement;
using Assets.Scripts.Infrastructure.Bootstrap;
using Assets.Scripts.Infrastructure.Factory;
using Assets.Scripts.Infrastructure.Services;
using Assets.Scripts.Infrastructure.Services.Input;
using Assets.Scripts.Infrastructure.Services.ProgressService;
using Assets.Scripts.Infrastructure.Services.SaveLoad;

namespace Assets.Scripts.Infrastructure.GameStates
{
    public class BootstrapState : IState
    {
        private const string InitialScene = "Initial";

        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly DependencyContainer _container;

        public BootstrapState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, DependencyContainer container)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _container = container;

            RegisterServices();
        }

        public void Enter() => 
            _sceneLoader.Load(InitialScene, EnterLoadLevel);

        public void Exit()
        {
        }

        private void EnterLoadLevel() =>
            _gameStateMachine.Enter<LoadProgressState>();

        private void RegisterServices()
        {
            _container.RegisterSingle<IInputService>(new StandaloneInput());
            _container.RegisterSingle<IAssetProvider>(new AssetProvider());
            _container.RegisterSingle<IProgressService>(new ProgressService());
            _container.RegisterSingle<IGameFactory>(new GameFactory(_container.Single<IAssetProvider>()));
            _container.RegisterSingle<ISaveLoadService>(new SaveLoadService(_container.Single<IProgressService>(), _container.Single<IGameFactory>()));
        }
    }
}
