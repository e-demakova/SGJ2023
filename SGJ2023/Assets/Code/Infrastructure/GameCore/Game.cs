using System.Collections.Generic;
using Infrastructure.GameCore.States;
using Infrastructure.StateMachine.States;
using Zenject;

namespace Infrastructure.GameCore
{
  public interface IGame { }

  public class Game : IGame, IInitializable
  {
    private readonly List<IGameState> _states;
    private readonly IGameStateMachine _stateMachine;

    public Game(IGameStateMachine stateMachine, List<IGameState> states)
    {
      _states = states;
      _stateMachine = stateMachine;
    }

    public void Initialize()
    {
      InitStateMachine();
      StartGame();
    }

    private void InitStateMachine()
    {
      foreach (IGameState state in _states)
        _stateMachine.RegisterState(state);
    }

    private void StartGame() =>
      _stateMachine.Enter<BootstrapState>();
  }
}