using System;
using Input;
using Observing.SubjectProperties;

namespace GameplayLogic.Map
{
  public interface IMapInteractionService
  {
    ISubjectBool CanInteract { get; }
    void EnableInteraction(IAction action);
    void DisableInteraction(IAction action);
  }

  public class MapInteractionService : IMapInteractionService, IDisposable
  {
    private readonly SubjectBool _canInteract = new();
    private readonly IInputService _input;

    private IAction _action;

    private IDisposable _subscriber;

    public ISubjectBool CanInteract => _canInteract;

    public MapInteractionService(IInputService inputService)
    {
      _input = inputService;
    }

    public void Dispose()
    {
      _canInteract?.Dispose();
      _subscriber?.Dispose();
    }

    public void EnableInteraction(IAction action)
    {
      if (_action != null) 
        _subscriber.Dispose();

      _action = action;
      _canInteract.Value = true;

      _subscriber = _input.On(_input.Act).Down().Subscribe(Act);
    }

    public void DisableInteraction(IAction action)
    {
      if (_action != action)
        return;

      _subscriber.Dispose();
      _action = null;
      _canInteract.Value = false;
    }

    private void Act() =>
      _action.Act();
  }
}