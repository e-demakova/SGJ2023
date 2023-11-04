using Infrastructure.StateMachine.States;
using Input;

namespace Infrastructure.GameCore.States
{
  public class GameLoopState : IGameState, IEnterState
  {
    private readonly IInputService _inputService;

    public GameLoopState(IInputService inputService)
    {
      _inputService = inputService;
    }

    public void Enter() =>
      StartGameLoop();

    private void StartGameLoop() =>
      _inputService.Enabled = true;
  }
}