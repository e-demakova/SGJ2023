using System;
using Infrastructure.GameCore;
using Infrastructure.GameCore.States;
using UnityEngine;
using Zenject;

namespace GameplayLogic.Map
{
  public class ChangeDayTimeAction : MonoBehaviour, IAction
  {
    [SerializeField]
    private DayTimeConfig _dayTime;
    
    private IGameStateMachine _stateMachine;

    [Inject]
    private void Construct(IGameStateMachine stateMachine)
    {
      _stateMachine = stateMachine;
    }
    
    public void Act() =>
      _stateMachine.Enter<ChangeDayTimeState, DayTimeConfig>(_dayTime);
  }
}