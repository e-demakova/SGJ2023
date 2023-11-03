using System.Collections;
using UnityEngine;

namespace Utils.Coroutines
{
  public interface ICoroutineRunner
  {
    Coroutine StartCoroutine(IEnumerator routine);
    void StopCoroutine(Coroutine coroutine);
    void StopAllCoroutines();
  }
}