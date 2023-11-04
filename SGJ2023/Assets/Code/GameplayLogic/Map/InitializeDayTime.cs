using System;
using Infrastructure.GameCore;
using Infrastructure.GameCore.States;
using UnityEngine;
using Zenject;

namespace GameplayLogic.Map
{
  public class InitializeDayTime : MonoBehaviour
  {
    [SerializeField]
    private DayTimeConfig _dayTime;

    private IGameStateMachine _stateMachine;
    private IDisposable _subscriber;

    [Inject]
    private void Construct(IGameStateMachine stateMachine)
    {
      _stateMachine = stateMachine;
    }

    private void Start()
    {
      _subscriber = _stateMachine.State.OnChange()
                   .When(x => x.NewValue is GameLoopState)
                   .Subscribe(() =>
                      _stateMachine.Enter<ChangeDayTimeState, DayTimeConfig>(_dayTime));
    }

    private void OnDestroy()
    {
      _subscriber.Dispose();
    }
  }
}