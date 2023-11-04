using System;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Assets;
using Infrastructure.GameCore;
using Infrastructure.GameObjectsManagement;
using JetBrains.Annotations;
using Observing.Handlers;
using Observing.Subscribers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using Utils.Extensions;
using Zenject;
using static UnityEngine.InputSystem.InputAction;
using Object = UnityEngine.Object;

namespace Input
{
  public struct InputContext
  {
    public InputAction Action;
    public InputActionPhase Phase;
  }

  [UsedImplicitly]
  public class InputService : IInputService, IBootstrapable, IDisposable, ITickable, IFixedTickable
  {
    private readonly Handler<InputContext> _actionsHandler = new();
    private readonly Handler<Vector2> _vectorHandler = new();

    private readonly InputActions _actions = new();
    private readonly IGameObjectBuilderFactory _factory;
    
    private EventSystem _eventSystem;

    private InputActions.MainActions MainActions => _actions.Main;
    public Vector2Int InputDirection { get; private set; }

    public InputAction Move => _actions.Main.Move;
    public InputAction Act => _actions.Main.Act;
    public InputAction Left => _actions.Main.Left;
    public InputAction Right => _actions.Main.Right;
    public InputAction Up => _actions.Main.Top;
    public InputAction Down => _actions.Main.Down;

    public EventSystem EventSystem => _eventSystem;

    public bool Enabled
    {
      get => _actions.Main.enabled;
      set
      {
        if (value)
          _actions.Enable();
        else
          _actions.Disable();
        
        EventSystem.enabled = value;
      }
    }

    public InputService(IGameObjectBuilderFactory factory)
    {
      _factory = factory;
    }

    public async Task Bootstrap()
    {
      GameObjectBuilder eventSystem = await _factory.Create(AssetsAddress.EventSystem);
      _eventSystem = eventSystem.Instantiate<EventSystem>();
      Object.DontDestroyOnLoad(EventSystem.gameObject);

      MainActions.Get().actionTriggered += OnActionTriggered;
    }

    public void Dispose()
    {
      _actionsHandler?.Dispose();
      _vectorHandler?.Dispose();

      MainActions.Get().actionTriggered -= OnActionTriggered;
    }

    public void Tick() =>
      RaisePerformed();

    public void FixedTick() =>
      UpdateVectors();

    public ISubscriber<InputContext> On(InputAction action) =>
      new Subscriber<InputContext>(_actionsHandler).When(x => x.Action == action);

    public ISubscriber<Vector2> OnVector() =>
      new Subscriber<Vector2>(_vectorHandler);

    public InputActionPhase GetPhase(InputAction reference) =>
      reference.phase;

    private void RaisePerformed()
    {
      foreach (InputAction inputAction in MainActions.Get().Where(x => x.phase == InputActionPhase.Performed))
      {
        _actionsHandler.Raise(new InputContext
        {
          Action = inputAction,
          Phase = InputActionPhase.Performed
        });
      }
    }

    private void UpdateVectors()
    {
      Vector2 vector = Move.ReadValue<Vector2>();
      _vectorHandler.Raise(vector);

      Vector2Int direction = new(vector.x.GetClearDirection(), vector.y.GetClearDirection());
      InputDirection = direction;
    }

    private void OnActionTriggered(CallbackContext context)
    {
      if (context.phase == InputActionPhase.Performed)
        return;

      _actionsHandler.Raise(new InputContext
      {
        Action = context.action,
        Phase = context.phase
      });
    }
  }
}