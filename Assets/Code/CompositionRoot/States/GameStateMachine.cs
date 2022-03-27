using CompositionRoot.SceneLoad;
using Services;
using System;
using System.Collections.Generic;

namespace CompositionRoot.States
{
    public class GameStateMachine
    {
        private readonly Dictionary<Type, IState> _states;
        private IState _activeState;

        public GameStateMachine(Locator locator, SceneLoader sceneLoader)
        {
            _states = new Dictionary<Type, IState>
            {
                [typeof(BootstrapState)] = new BootstrapState(this, locator, sceneLoader),
                [typeof(MenuState)] = new MenuState(locator),
                [typeof(GameState)] = new GameState(locator)
            };
        }

        public void Enter<TState>() where TState : class, IState
        {
            IState state = ChangeState<TState>();
            state.Enter();
        }

        private TState ChangeState<TState>() where TState : class, IState
        {
            TryExit();

            TState state = GetState<TState>();
            _activeState = state;

            return state;
        }

        private void TryExit()
        {
            if (_activeState is IExitableState)
                (_activeState as IExitableState)?.Exit();
        }

        private TState GetState<TState>() where TState : class, IState =>
            _states[typeof(TState)] as TState;
    }
}