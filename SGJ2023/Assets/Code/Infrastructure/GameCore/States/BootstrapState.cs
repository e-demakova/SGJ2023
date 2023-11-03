using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.StateMachine.States;
using Utils.Coroutines;

namespace Infrastructure.GameCore.States
{
  public class BootstrapState : IGameState, IEnterState
  {
    private readonly List<Task> _tasks = new();

    private readonly IGameStateMachine _stateMachine;
    private readonly List<IBootstrapable> _bootstrapable;
    private readonly ICoroutineRunner _coroutineRunner;

    public BootstrapState(IGameStateMachine stateMachine, List<IBootstrapable> bootstrapable,
      ICoroutineRunner coroutineRunner)
    {
      _stateMachine = stateMachine;
      _bootstrapable = bootstrapable;
      _coroutineRunner = coroutineRunner;
    }

    public void Enter()
    {
      _coroutineRunner.StartCoroutine(Bootstrap());
    }

    private IEnumerator Bootstrap()
    {
      foreach (IBootstrapable service in _bootstrapable)
        _tasks.Add(service.Bootstrap());

      while (_tasks.Any(x => !x.IsCompleted))
        yield return null;

      _tasks.Clear();
      _stateMachine.Enter<LoadSceneState>();
    }
  }
}