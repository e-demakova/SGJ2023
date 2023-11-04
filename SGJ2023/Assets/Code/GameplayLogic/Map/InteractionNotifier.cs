using System;
using UnityEngine;
using Zenject;

namespace GameplayLogic.Map
{
  public class InteractionNotifier : MonoBehaviour
  {
    private IMapInteractionService _interactionService;
    private IDisposable _subscriber;

    [Inject]
    private void Construct(IMapInteractionService interactionService)
    {
      _interactionService = interactionService;
    }

    private void Start()
    {
      _subscriber = _interactionService.CanInteract.OnChange().Subscribe(UpdateVisibility);
      UpdateVisibility();
    }

    private void OnDestroy() =>
      _subscriber.Dispose();

    private void UpdateVisibility() =>
      gameObject.SetActive(_interactionService.CanInteract.Value);
  }
}