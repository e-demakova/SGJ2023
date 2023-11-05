using System;
using DG.Tweening;
using Infrastructure.GameCore.States;
using UnityEngine;
using UnityEngine.UI;

namespace GameplayLogic
{
  public class SkipTimeScreenHelp : MonoBehaviour
  {
    [SerializeField]
    private Image _image;

    [SerializeField]
    private float _fadeDuration = 0.2f;
    
    private Sequence _sequence;

    private void Awake()
    {
      _sequence = DOTween.Sequence();
      _sequence.PrependInterval(ChangeDayTimeState.WaitDuration);
      _sequence.Append(_image.DOFade(1, _fadeDuration));
    }

    private void OnDestroy() =>
      _sequence?.Kill();
  }
}