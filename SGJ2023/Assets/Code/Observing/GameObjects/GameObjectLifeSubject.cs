using Observing.Handlers;
using Observing.Subscribers;
using UnityEngine;

namespace Observing.GameObjects
{
  public class GameObjectLifeSubject : MonoBehaviour
  {
    private readonly Handler _onDestroy = new();

    private void OnDestroy()
    {
      _onDestroy.Raise();
      
      _onDestroy.Dispose();
    }

    public ISubscriber WhenDestroy() =>
      new Subscriber(_onDestroy);
  }
}