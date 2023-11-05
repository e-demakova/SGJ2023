using Infrastructure.GameCore;
using Infrastructure.GameCore.States;
using UnityEngine;
using Zenject;

namespace GameplayLogic.Map
{
  public class SelectEndingAction : MonoBehaviour, IAction
  {
    [SerializeField]
    private DayTimeConfig _policeEnd;

    [SerializeField]
    private DayTimeConfig _trainEnd;

    private IPoliceHairstyleClue _hairstyleClue;
    private IGameStateMachine _stateMachine;

    [Inject]
    private void Construct(IPoliceHairstyleClue hairstyleClue, IGameStateMachine stateMachine)
    {
      _hairstyleClue = hairstyleClue;
      _stateMachine = stateMachine;
    }

    public void Act()
    {
      _stateMachine.Enter<ChangeDayTimeState, DayTimeConfig>(
        _hairstyleClue.ActualHairstyle == _hairstyleClue.DeclaredHairstyle
          ? _policeEnd
          : _trainEnd);
    }
  }
}