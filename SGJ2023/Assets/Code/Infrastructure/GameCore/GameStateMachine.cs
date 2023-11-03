using System;
using System.Collections.Generic;
using Infrastructure.StateMachine.States;
using Observing.SubjectProperties;

namespace Infrastructure.GameCore
{
  public class GameStateMachine : IGameStateMachine
  {
    private readonly Dictionary<Type, IGameState> _states = new();
    private readonly SubjectProperty<IGameState> _state = new();

    public ISubjectProperty<IGameState> State => _state;

    public void RegisterState(IGameState state) =>
      _states.Add(state.GetType(), state);

    public void Enter<T>() where T : class, IGameState, IEnterState
    {
      if (_state.Value is IExitState exitState)
        exitState.Exit();

      _state.Value = _states[typeof(T)];

      if (_state.Value is IEnterState enterState)
        enterState.Enter();
    }

    public void Enter<T, TPayload>(TPayload payload) where T : class, IGameState, IPayloadState<TPayload>
    {
      if (_state.Value is IExitState exitState)
        exitState.Exit();

      _state.Value = _states[typeof(T)];
      IPayloadState<TPayload> state = (IPayloadState<TPayload>) _state.Value;
      state.Enter(payload);
    }
  }
}