using System.Collections;
using DG.Tweening;
using GameplayLogic.Audio;
using UnityEngine;
using Zenject;

namespace GameplayLogic
{
  public class GameQuitter : MonoBehaviour
  {
    [SerializeField]
    private CanvasGroup _canvasGroup;
    
    [SerializeField, Min(0)]
    private float _appearDuration = 0.5f;
    
    [SerializeField, Min(0)]
    private float _quitDuration = 1.5f;

    private IMusicService _music;

    [Inject]
    private void Construct(IMusicService music)
    {
      _music = music;
    }
    
    private void Start()
    {
      StartCoroutine(Quitting());
    }

    private IEnumerator Quitting()
    {
      _music.StopMusic();
      _canvasGroup.DOFade(1, _appearDuration);
      
      yield return new WaitForSeconds(_appearDuration + _quitDuration);
      Application.Quit();
    }
  }
}