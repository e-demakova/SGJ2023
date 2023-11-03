using UnityEngine;

namespace Utils.Coroutines
{
  public static class CoroutineExtensions
  {
    public static Coroutine StopCoroutine(this Coroutine coroutine, ICoroutineRunner runner)
    {
      if (coroutine != null)
        runner.StopCoroutine(coroutine);
      
      return null;
    }

    public static Coroutine StopCoroutine(this Coroutine coroutine, MonoBehaviour runner)
    {
      if (coroutine != null)
        runner.StopCoroutine(coroutine);
      
      return null;
    }
  }
}