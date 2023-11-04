using Observing.SubjectProperties;

namespace Map
{
  public interface IMapInteractionService
  {
    ISubjectBool CanInteract { get; }
    void EnableInteraction(IAction action);
    void DisableInteraction(IAction action);
  }

  public class MapInteractionService : IMapInteractionService
  {
    private readonly SubjectBool _canInteract = new();
    public ISubjectBool CanInteract => _canInteract;

    private IAction _action;

    public void EnableInteraction(IAction action)
    {
      _action = action;
      _canInteract.Value = true;
    }

    public void DisableInteraction(IAction action)
    {
      if (_action != action) 
        return;
      
      _action = null;
      _canInteract.Value = false;
    }
  }
}