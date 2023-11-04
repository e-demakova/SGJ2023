using System.Collections;
using GameplayLogic.Map;
using Infrastructure.StateMachine.States;
using Input;
using SceneLoading;
using UnityEngine;
using Utils.Coroutines;

namespace Infrastructure.GameCore.States
{
  public class ChangeDayTimeState : IGameState, IPayloadState<DayTimeConfig>
  {
    private readonly IGameStateMachine _stateMachine;
    private readonly ISceneLoader _sceneLoader;
    private readonly IInputService _inputService;
    private readonly ICoroutineRunner _coroutineRunner;
    private readonly IDayTimeService _dayTimeService;
    
    private DayTimeConfig _dayTimeConfig;

    public ChangeDayTimeState(IGameStateMachine stateMachine, ISceneLoader sceneLoader, IInputService inputService,
      ICoroutineRunner coroutineRunner, IDayTimeService dayTimeService)
    {
      _stateMachine = stateMachine;
      _sceneLoader = sceneLoader;
      _inputService = inputService;
      _coroutineRunner = coroutineRunner;
      _dayTimeService = dayTimeService;
    }
    
    public void Enter(DayTimeConfig payload)
    {
      _dayTimeConfig = payload;
      _dayTimeService.Config = _dayTimeConfig;
      
      _inputService.Enabled = false;
      _sceneLoader.Load(_sceneLoader.Scenes.DayTimeInfoScene, ShowDayTimeInfo);
    }

    private void ShowDayTimeInfo() =>
      _coroutineRunner.StartCoroutine(Showing());

    private IEnumerator Showing()
    {
      yield return new WaitForSeconds(1f);
      _sceneLoader.Load(_dayTimeConfig.Scene, EnterNextState);
    }
    
    private void EnterNextState() =>
      _stateMachine.Enter<GameLoopState>();
  }
}