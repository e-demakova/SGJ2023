using System;
using System.Collections.Generic;
using Observing.Handlers;
using Observing.Subscribers;
using UnityEngine;

namespace Observing.GameObjects.Physics
{
  public interface ITriggerSubscriber2D : ISubscriber<Collider2D>
  {
    ITriggerSubscriber2D WithComponent<T>();
    ITriggerSubscriber2D WithComponent(Type component);
    ITriggerSubscriber2D WithLayer(LayerMask layerMask);
  }

  public class TriggerSubscriber2D : Subscriber<Collider2D>, ITriggerSubscriber2D
  {
    private readonly List<Type> _requiredComponents = new();
    private LayerMask _layerMask = 1;

    public TriggerSubscriber2D(IHandler<Collider2D> handler, Action<Collider2D> action = default)
      : base(handler, action) { }

    public ITriggerSubscriber2D WithComponent<T>()
    {
      _requiredComponents.Add(typeof(T));
      return this;
    }

    public ITriggerSubscriber2D WithComponent(Type component)
    {
      _requiredComponents.Add(component);
      return this;
    }

    public ITriggerSubscriber2D WithLayer(LayerMask layerMask)
    {
      _layerMask = layerMask;
      return this;
    }

    protected override bool CustomPredicate(Collider2D other)
    {
      if (_layerMask != (_layerMask | (1 << other.gameObject.layer)))
        return false;

      for (var i = 0; i < _requiredComponents.Count; i++)
      {
        if (!other.TryGetComponent(_requiredComponents[i], out _))
          return false;
      }

      return true;
    }
  }
}