using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using Infrastructure.GameObjectsManagement;
using UnityEngine;

namespace GameplayLogic.MiniGames.PaperWork
{
  public class MoneyAdder : MonoBehaviour, IPoolable<MoneyAdder>
  {
    [SerializeField]
    private float _duration;

    [SerializeField]
    private SpriteRenderer _renderer;

    [SerializeField]
    private float _startY;

    [SerializeField]
    private float _targetY;

    private Pool<MoneyAdder> _pool;
    private Tween _moveTween;
    private Tween _fadeTween;

    public void OnDequeue(Pool<MoneyAdder> pool)
    {
      _pool = pool;
      transform.position = new Vector3(transform.position.x, _startY);
      _renderer.color = new Color(1, 1, 1, 0);
      gameObject.SetActive(true);
      
      _moveTween = transform.DOMoveY(_targetY, _duration);
      _fadeTween = _renderer.DOFade(1, _duration).OnComplete(() =>_pool.Return(this));
    }

    public void OnEnqueue() =>
      gameObject.SetActive(false);

    private void OnDisable()
    {
      _moveTween?.Kill();
      _fadeTween?.Kill();
    }
  }
}