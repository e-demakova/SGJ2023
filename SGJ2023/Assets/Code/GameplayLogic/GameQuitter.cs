using System.Collections;
using UnityEngine;

namespace GameplayLogic
{
  public class GameQuitter : MonoBehaviour
  {
    [SerializeField, Min(0)]
    private float _quitDuration = 1.5f;

    private void Awake()
    {
      StartCoroutine(Quitting());
    }

    private IEnumerator Quitting()
    {
      yield return new WaitForSeconds(_quitDuration);
      Application.Quit();
    }
  }
}