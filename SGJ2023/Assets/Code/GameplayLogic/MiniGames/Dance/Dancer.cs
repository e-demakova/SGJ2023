﻿using System;
using System.Collections;
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
    private AudioSource _audio;

    [SerializeField]
    private SpriteRenderer _renderer;

    [SerializeField]
    private DanceDirection[] _directions;

    [SerializeField]
    private AssetReference _scene;

    [SerializeField, Min(0)]
    private float _miniGameEndingDuration = 0.2f;

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
      _input.On().Down().Subscribe(CheckDirection).AddTo(_subscribers);

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

    private void OnDestroy() =>
      _subscribers.DisposeAll();

    private void CheckDirection(InputContext context)
    {
      bool directionCorrect = _directionsTypes.TryGetValue(context.Action, out DanceDirectionType directionType) &&
                              _directionsQueue.Peek().Direction == directionType;

      if (directionCorrect)
        Dance();

      if (_directionsQueue.Count == 0)
        EndMiniGame();
    }

    private void Dance()
    {
      DanceDirection direction = _directionsQueue.Dequeue();
      direction.gameObject.SetActive(false);
      _renderer.flipX = !_renderer.flipX;
      _audio.clip = direction.Clip;
      _audio.Play();
    }

    private void EndMiniGame()
    {
      _subscribers.DisposeAll();
      StartCoroutine(Ending());
    }
    
    private IEnumerator Ending()
    {
      yield return new WaitForSeconds(_miniGameEndingDuration);
      _stateMachine.Enter<LoadSceneState, AssetReference>(_scene);
    }
  }
}