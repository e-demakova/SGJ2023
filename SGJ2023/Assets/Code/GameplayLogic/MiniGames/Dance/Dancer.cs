using System;
using System.Collections.Generic;
using Infrastructure.GameCore;
using Infrastructure.GameCore.States;
using Input;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.InputSystem;
using Utils.Extensions;
using Zenject;

namespace GameplayLogic.MiniGames.Dance
{
  public class Dancer : MonoBehaviour
  {
    private readonly List<IDisposable> _subscribers = new();
    private readonly Queue<DanceDirection> _directionsQueue = new();

    [SerializeField]
    private DanceDirection[] _directions;

    [SerializeField]
    private AssetReference _scene;

    private IInputService _input;
    private IGameStateMachine _stateMachine;

    private Dictionary<InputAction, DanceDirectionType> _directionsTypes;

    [Inject]
    private void Construct(IInputService inputService, IGameStateMachine stateMachine)
    {
      _input = inputService;
      _stateMachine = stateMachine;
    }

    private void Start()
    {
      SubscribeOn(_input.Left);
      SubscribeOn(_input.Right);
      SubscribeOn(_input.Up);
      SubscribeOn(_input.Down);
      
      foreach (DanceDirection direction in _directions) 
        _directionsQueue.Enqueue(direction);
      
      _directionsTypes = new Dictionary<InputAction, DanceDirectionType>()
      {
        { _input.Left, DanceDirectionType.Left },
        { _input.Right, DanceDirectionType.Right },
        { _input.Up, DanceDirectionType.Up },
        { _input.Down, DanceDirectionType.Down },
      };
    }

    private void SubscribeOn(InputAction action) =>
      _input.On(action).Down().Subscribe(CheckDirection).AddTo(_subscribers);

    private void OnDestroy() =>
      _subscribers.DisposeAll();

    private void CheckDirection(InputContext context)
    {
      if (_directionsQueue.Peek().Direction == _directionsTypes[context.Action])
      {
        DanceDirection direction = _directionsQueue.Dequeue();
        direction.gameObject.SetActive(false);
      }

      if (_directionsQueue.Count == 0) 
        EndMiniGame();
    }

    private void EndMiniGame()
    {
      _subscribers.DisposeAll();
      _stateMachine.Enter<LoadSceneState, AssetReference>(_scene);
    }
  }
}