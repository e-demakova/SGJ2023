using UnityEngine;
using Utils.Extensions;

namespace Observing.GameObjects.Physics
{
  public static class TriggerObservingExtensions
  {
    public static ITriggerSubscriber2D TriggerEnter2D(this GameObject gameObject) => 
      gameObject.GetOrAdd<Trigger2DSubject>().OnEnter();

    public static ITriggerSubscriber2D TriggerEnter2D(this Component component) => 
      component.GetOrAdd<Trigger2DSubject>().OnEnter();

    public static ITriggerSubscriber2D TriggerExit2D(this GameObject gameObject) => 
      gameObject.GetOrAdd<Trigger2DSubject>().OnExit();

    public static ITriggerSubscriber2D TriggerExit2D(this Component component) => 
      component.GetOrAdd<Trigger2DSubject>().OnExit();
  }
}