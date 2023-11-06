using System;
using Infrastructure.GameCore;
using Infrastructure.GameCore.States;
using Input;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace GameplayLogic.MiniGames
{
  public class Window : MonoBehaviour
  {
    [SerializeField]
    private AssetReference _scene;

    private IInputService _input;
    private IGameStateMachine _gameStateMachine;
    private IDisposable _subscriber;

    [Inject]
    private void Construct(IInputService input, IGameStateMachine gameStateMachine)
    {
      _input = input;
      _gameStateMachine = gameStateMachine;
    }

    private void Start() =>
      _subscriber = _input.On(_input.Act).Down().Subscribe(EndMiniGame);

    private void OnDestroy() =>
      _subscriber?.Dispose();

    private void EndMiniGame()
    {
      _subscriber?.Dispose();
      _gameStateMachine.Enter<LoadSceneState, AssetReference>(_scene);
    }
  }
}