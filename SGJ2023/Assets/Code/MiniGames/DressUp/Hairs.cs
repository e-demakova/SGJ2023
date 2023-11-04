using System;
using System.Collections.Generic;
using Infrastructure.GameCore;
using Infrastructure.GameCore.States;
using Input;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Utils.Extensions;
using Zenject;

namespace MiniGames.DressUp
{
  public class Hairs : MonoBehaviour
  {
    private readonly List<IDisposable> _subscribers = new();

    [SerializeField]
    private AssetReference _scene;
    
    [SerializeField]
    private GameObject _default;

    [SerializeField]
    private GameObject[] _hairs;

    private IInputService _input;
    private IGameStateMachine _gameStateMachine;

    private int _hairIndex = -1;
    private bool _canApply;

    [Inject]
    private void Construct(IInputService input, IGameStateMachine gameStateMachine)
    {
      _input = input;
      _gameStateMachine = gameStateMachine;
    }

    private void Start()
    {
      _input.On(_input.Left).Down().Subscribe(LeftHairVariant).AddTo(_subscribers);
      _input.On(_input.Right).Down().Subscribe(RightHairVariant).AddTo(_subscribers);

      _input.On(_input.Act).Down()
            .When(_ => _canApply)
            .Subscribe(LoadScene)
            .AddTo(_subscribers);
    }

    private void OnDestroy() =>
      _subscribers.DisposeAll();

    private void LoadScene() =>
      _gameStateMachine.Enter<LoadSceneState, AssetReference>(_scene);

    private void LeftHairVariant()
    {
      _canApply = true;
      DisablePrevious();
      _hairIndex--;

      if (_hairIndex < 0)
        _hairIndex = _hairs.Length - 1;

      EnableCurrent();
    }

    private void RightHairVariant()
    {
      _canApply = true;
      DisablePrevious();
      _hairIndex++;

      if (_hairIndex >= _hairs.Length)
        _hairIndex = 0;

      EnableCurrent();
    }

    private void DisablePrevious()
    {
      if (_hairIndex < 0)
        _default.SetActive(false);
      else
        _hairs[_hairIndex].SetActive(false);
    }

    private void EnableCurrent() =>
      _hairs[_hairIndex].SetActive(true);
  }
}