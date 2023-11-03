using Observing.Subscribers;
using UnityEngine;
using Utils.Extensions;

namespace Observing.GameObjects
{
  public static class GameObjectLifeExtensions
  {
    public static ISubscriber OnDestroy(this GameObject gameObject) => 
      gameObject.GetOrAdd<GameObjectLifeSubject>().WhenDestroy();
  }
}