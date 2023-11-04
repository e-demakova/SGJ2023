using Observing.Subscribers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Input
{
  public interface IInputService
  {
    Vector2Int InputDirection { get; }
    
    InputAction Move { get; }
    InputAction Act { get; }
    EventSystem EventSystem { get; }
    bool Enabled { get; set; }

    ISubscriber<InputContext> On(InputAction action);
    ISubscriber<Vector2> OnVector();
    InputActionPhase GetPhase(InputAction reference);
  }
}