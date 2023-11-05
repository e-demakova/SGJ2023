using System;
using System.Collections;
using GameplayLogic.Audio;
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
    public const float WaitDuration = 1f;
    
    private readonly IGameStateMachine _stateMachine;
    private readonly ISceneLoader _sceneLoader;
    private readonly IInputService _input;
    private readonly ICoroutineRunner _coroutineRunner;
    private readonly IDayTimeService _dayTimeService;
    private readonly IMusicService _musicService;
    
    private DayTimeConfig _dayTimeConfig;
    private IDisposable _subscriber;

    public ChangeDayTimeState(IGameStateMachine stateMachine, ISceneLoader sceneLoader, IInputService input,
      ICoroutineRunner coroutineRunner, IDayTimeService dayTimeService, IMusicService musicService)
    {
      _stateMachine = stateMachine;
      _sceneLoader = sceneLoader;
      _input = input;
      _coroutineRunner = coroutineRunner;
      _dayTimeService = dayTimeService;
      _musicService = musicService;
    }
    
    public void Enter(DayTimeConfig payload)
    {
      _musicService.StopMusic();
      _dayTimeConfig = payload;
      _dayTimeService.Config = _dayTimeConfig;
      
      _input.Enabled = false;
      _sceneLoader.Load(_sceneLoader.Scenes.DayTimeInfoScene, ShowDayTimeInfo);
    }

    private void ShowDayTimeInfo() =>
      _coroutineRunner.StartCoroutine(Showing());

    private IEnumerator Showing()
    {
      _input.Enabled = true;
      yield return new WaitForSeconds(WaitDuration);
      _subscriber = _input.On(_input.Act).Down().Subscribe(LoadNextDayTimeScene);
    }

    private void LoadNextDayTimeScene()
    {
      _subscriber.Dispose();
      
      _input.Enabled = false;
      _sceneLoader.Load(_dayTimeConfig.Scene, EnterNextState);
      
      _musicService.StartMusic(_dayTimeConfig.Music);
    }

    private void EnterNextState() =>
      _stateMachine.Enter<GameLoopState>();
  }
}