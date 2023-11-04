using System;
using System.Collections.Generic;
using Observing.GameObjects.Physics;
using UnityEngine;
using Utils.Extensions;
using Zenject;

namespace GameplayLogic.Map
{
  public class InteractiveObject : MonoBehaviour
  {
    private readonly List<IDisposable> _subscribers = new();

    [SerializeField]
    private Collider2D _collider;

    private IMapInteractionService _mapInteraction;
    private IAction _action;

    [Inject]
    private void Construct(IMapInteractionService mapInteraction)
    {
      _mapInteraction = mapInteraction;
    }

    private void Awake()
    {
      _collider.TriggerEnter2D().Subscribe(EnableInteraction).AddTo(_subscribers);
      _collider.TriggerExit2D().Subscribe(DisableInteraction).AddTo(_subscribers);

      _action = GetComponent<IAction>();
    }

    private void OnDestroy()
    {
      _subscribers.DisposeAll();
      _mapInteraction.DisableInteraction(_action);
    }

    private void EnableInteraction() =>
      _mapInteraction.EnableInteraction(_action);

    private void DisableInteraction() =>
      _mapInteraction.DisableInteraction(_action);
  }
}