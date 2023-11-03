using UnityEngine;
using Utils.Extensions;
using Zenject;

namespace Infrastructure.GameObjectsManagement
{
  public class GameObjectBuilder
  {
    private readonly GameObject _source;

    private Vector3 Position { get; set; } = Vector3.zero;
    private Quaternion Rotation { get; set; } = Quaternion.identity;
    private DiContainer Container { get; set; }
    private Transform Parent { get; set; }

    public GameObjectBuilder(GameObject source) =>
      _source = source;

    public GameObjectBuilder At(Vector3 position)
    {
      Position = position;
      return this;
    }

    public GameObjectBuilder With(Quaternion rotation)
    {
      Rotation = rotation;
      return this;
    }

    public GameObjectBuilder With(Transform parent)
    {
      Parent = parent;
      return this;
    }

    public GameObjectBuilder WithInjection(DiContainer container)
    {
      Container = container;
      return this;
    }

    public T Instantiate<T>() =>
      Instantiate().GetComponent<T>();

    public GameObject Instantiate()
    {
      GameObject gameObject = Object.Instantiate(_source, Position, Rotation, Parent);
      if (Container != null)
        gameObject.Inject(Container);
      
      return gameObject;
    }
  }
}