using UnityEngine;
using Zenject;

namespace Utils.Extensions
{
  public static class ZenjectExtensions
  {
    public static GameObject Inject(this GameObject gameObject, DiContainer container)
    {
      container.InjectGameObject(gameObject);
      return gameObject;
    }

    public static T Inject<T>(this T component, DiContainer container) where T : Component
    {
      component.gameObject.Inject(container);
      return component;
    }

    public static void FullBind<T>(this DiContainer container, T instance)
    {
      container.BindInterfacesAndSelfTo<T>().FromInstance(instance).AsSingle().NonLazy();
    }

    public static void BindService<T>(this DiContainer container)
    {
      container.BindInterfacesTo<T>().AsSingle().NonLazy();
    }

    public static void BindService<T>(this DiContainer container, params object[] args)
    {
      container.BindInterfacesTo<T>().AsSingle().WithArguments(args).NonLazy();
    }

    public static void FullBind<T>(this DiContainer container)
    {
      container.BindInterfacesAndSelfTo<T>().AsSingle().NonLazy();
    }
  }
}