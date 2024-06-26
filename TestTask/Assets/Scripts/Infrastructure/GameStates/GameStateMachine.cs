﻿using Assets.Scripts.Infrastructure.Bootstrap;
using Assets.Scripts.Infrastructure.Factory;
using Assets.Scripts.Infrastructure.Services;
using Assets.Scripts.Infrastructure.Services.ProgressService;
using Assets.Scripts.Infrastructure.Services.SaveLoad;
using Assets.Scripts.Logic;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.Infrastructure.GameStates
{
    public class GameStateMachine
    {
        private Dictionary<Type, IExitableState> _states;
        private IExitableState _activeState;

        public GameStateMachine(SceneLoader sceneLoader, LoadingScreen loadingScreen, DependencyContainer container)
        {
            _states = new Dictionary<Type, IExitableState>()
            {
                [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, container),
                [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader, loadingScreen, container.Single<IGameFactory>(), container.Single<IProgressService>()),
                [typeof(LoadProgressState)] = new LoadProgressState(this, container.Single<IProgressService>(), container.Single<ISaveLoadService>()),
                [typeof(GameLoopState)] = new GameLoopState(this),
            };
        }

        public void Enter<TState>() where TState : class, IState
        {
            IState state = ChangeState<TState>();
            state.Enter();
        }

        public void Enter<TState, TPayLoad>(TPayLoad payLoad) where TState : class, IPayLoadedState<TPayLoad>
        {
            TState state = ChangeState<TState>();
            state.Enter(payLoad);
        }

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _activeState?.Exit();

            TState state = GetStated<TState>();
            _activeState = state;

            return state;
        }

        private TState GetStated<TState>() where TState : class, IExitableState =>
            _states[typeof(TState)] as TState;
    }
}
