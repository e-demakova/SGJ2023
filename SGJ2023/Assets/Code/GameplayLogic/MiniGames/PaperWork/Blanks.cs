using System;
using System.Collections;
using System.Collections.Generic;
using Infrastructure.GameCore;
using Infrastructure.GameCore.States;
using Infrastructure.GameObjectsManagement;
using Input;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Utils.Extensions;
using Zenject;
using Random = UnityEngine.Random;

namespace GameplayLogic.MiniGames.PaperWork
{
  public class Blanks : MonoBehaviour
  {
    private const float Shuffle = 0.15f;

    private readonly List<IDisposable> _subscribers = new();
    private readonly Queue<Blank> _blanks = new();

    [SerializeField, Min(1)]
    private int _blanksCount;

    [SerializeField]
    private Transform _right;

    [SerializeField]
    private Transform _left;

    [SerializeField]
    private GameObject _blankPrefab;

    [SerializeField]
    private GameObject _addMoneyPrefab;
    
    [SerializeField]
    private GameObject _removeMoneyPrefab;
    
    [SerializeField]
    private AssetReference _scene;

    [SerializeField, Min(0)]
    private float _miniGameEndingDuration = 0.2f;
    
    private IGameObjectBuilderFactory _factory;
    private IInputService _input;
    private IGameStateMachine _gameStateMachine;

    private IPool<MoneyAdder> _addMoneyPool;
    private IPool<MoneyAdder> _removeMoneyPool;

    [Inject]
    private void Construct(IGameObjectBuilderFactory factory, IInputService input, IGameStateMachine gameStateMachine)
    {
      _factory = factory;
      _input = input;
      _gameStateMachine = gameStateMachine;
    }

    private void Start()
    {
      _addMoneyPool = new Pool<MoneyAdder>(_factory.Create(_addMoneyPrefab));
      _removeMoneyPool = new Pool<MoneyAdder>(_factory.Create(_removeMoneyPrefab));
      
      SubscribeOnInput();

      CreateBlanks();
    }

    private void OnDestroy() =>
      _subscribers.DisposeAll();

    private void SubscribeOnInput()
    {
      _input.On(_input.Left).Down().Subscribe(MoveBlankLeft).AddTo(_subscribers);
      _input.On(_input.Right).Down().Subscribe(MoveBlankRight).AddTo(_subscribers);
    }

    private void CreateBlanks()
    {
      for (int i = 0; i < _blanksCount; i++)
      {
        _factory.Create(_blankPrefab)
                .With(parent: transform)
                .At(GetShuffledPosition(transform.position))
                .Instantiate<Blank>()
                .Init(GetBlankType(), i)
                .EnqueueTo(_blanks);
      }
    }

    private void MoveBlankRight() =>
      MoveBlank(BlankType.Right, _right.position);

    private void MoveBlankLeft() =>
      MoveBlank(BlankType.Left, _left.position);

    private void MoveBlank(BlankType type, Vector3 position)
    {
      Blank blank = _blanks.Dequeue();
      AddMoney(blank.Type == type ? _addMoneyPool : _removeMoneyPool);
      blank.MoveTo(GetShuffledPosition(position));

      if (_blanks.Count == 0)
        EndMiniGame();
    }

    private void EndMiniGame()
    {
      _subscribers.DisposeAll();
      StartCoroutine(Ending());
    }

    private IEnumerator Ending()
    {
      yield return new WaitForSeconds(_miniGameEndingDuration);
      _gameStateMachine.Enter<LoadSceneState, AssetReference>(_scene);
    }

    private void AddMoney(IPool<MoneyAdder> pool) =>
      pool.Get();

    private static BlankType GetBlankType() =>
      Random.Range(0f, 1f) switch
      {
        > 0.5f => BlankType.Left,
        _ => BlankType.Right
      };

    private static Vector3 GetShuffledPosition(Vector3 position) =>
      position + new Vector3(Random.Range(Shuffle, -Shuffle), Random.Range(Shuffle, -Shuffle));
  }
}