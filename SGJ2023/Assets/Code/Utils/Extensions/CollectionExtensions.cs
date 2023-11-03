using System;
using System.Collections.Generic;
using System.Linq;

namespace Utils.Extensions
{
  public static class CollectionExtensions
  {
    public static T Random<T>(this IReadOnlyCollection<T> list) =>
      list.ElementAt(UnityEngine.Random.Range(0, list.Count));
    
    public static T Random<T>(this T[] array) =>
      array[UnityEngine.Random.Range(0, array.Length)];
    
    public static void DisposeAll(this ICollection<IDisposable> list)
    {
      foreach (IDisposable disposable in list)
        disposable?.Dispose();

      list.Clear();
    }
  }
}