using System.Collections.Generic;
using ModestTree;
using UnityEngine;

namespace Infrastructure.GameObjectsManagement
{
  public interface IPoolable<T> where T : Component
  {
    void OnDequeue(Pool<T> pool);
    void OnEnqueue();
  }

  public interface IPool<T> where T : Component
  {
    bool IsEmpty { get; }
    T Get();
    void Return(T item);
  }

  public class Pool<T> : IPool<T> where T : Component
  {
    private readonly Queue<T> _queue = new();
    private readonly GameObjectBuilder _builder;

    public bool IsEmpty => _queue.IsEmpty();

    public Pool(GameObjectBuilder builder) =>
      _builder = builder;

    public T Get()
    {
      T item = IsEmpty ? _builder.Instantiate<T>() : _queue.Dequeue();
      
      if (item is IPoolable<T> poolable)
        poolable.OnDequeue(this);
      
      return item;
    }

    public void Return(T item)
    {
      if (item is IPoolable<T> poolable)
        poolable.OnEnqueue();

      _queue.Enqueue(item);
    }
  }
}