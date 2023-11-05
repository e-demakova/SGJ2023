using System;
using System.Collections.Generic;
using GameplayLogic.Map;
using Infrastructure.GameCore;
using Infrastructure.GameCore.States;
using Input;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Utils.Extensions;
using Zenject;

namespace GameplayLogic.MiniGames.DressUp
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

    [SerializeField]
    private bool _isWantedSelection;
    
    private int _hairIndex = -1;
    private bool _canApply;
    
    private IPoliceHairstyleClue _hairstyleClue;

    [Inject]
    private void Construct(IInputService input, IGameStateMachine gameStateMachine, IPoliceHairstyleClue hairstyleClue)
    {
      _input = input;
      _gameStateMachine = gameStateMachine;

      _hairstyleClue = hairstyleClue;
    }

    private void Start()
    {
      _input.On(_input.Left).Down().Subscribe(LeftHairVariant).AddTo(_subscribers);
      _input.On(_input.Right).Down().Subscribe(RightHairVariant).AddTo(_subscribers);

      _input.On(_input.Act).Down()
            .When(_ => _canApply)
            .Subscribe(EndMiniGame)
            .AddTo(_subscribers);
    }

    private void OnDestroy() =>
      _subscribers.DisposeAll();

    private void EndMiniGame()
    {
      if (_isWantedSelection)
        _hairstyleClue.DeclaredHairstyle = _hairIndex;
      else
        _hairstyleClue.ActualHairstyle = _hairIndex;
      
      _subscribers.DisposeAll();
      _gameStateMachine.Enter<LoadSceneState, AssetReference>(_scene);
    }

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