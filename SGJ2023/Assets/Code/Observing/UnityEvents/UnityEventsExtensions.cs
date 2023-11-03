using Observing.Subscribers;
using UnityEngine.Events;

namespace Observing.UnityEvents
{
  public static class UnityEventsExtensions
  {
    public static ISubscriber<EmptyEvent> AsHandler(this UnityEvent unityEvent) =>
      new EmptyUnityEventSubscriber(unityEvent);

    public static ISubscriber<T> AsHandler<T>(this UnityEvent<T> unityEvent) =>
      new ParameterUnityEventSubscriber<T>(unityEvent);
  }
}