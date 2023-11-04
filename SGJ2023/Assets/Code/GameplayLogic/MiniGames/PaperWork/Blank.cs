﻿using System;
using DG.Tweening;
using UnityEngine;

namespace GameplayLogic.MiniGames.PaperWork
{
  public enum BlankType
  {
    Left = 0,
    Right = 1,
  }

  public class Blank : MonoBehaviour
  {
    public BlankType Type { get; private set; }

    [SerializeField]
    private SpriteRenderer _renderer;

    [SerializeField]
    private Sprite _left;

    [SerializeField]
    private Sprite _right;

    [SerializeField]
    private float _moveDuration = 0.1f;

    public Blank Init(BlankType type, int sortingOrder)
    {
      Type = type;

      _renderer.sortingOrder = -sortingOrder;
      _renderer.sprite = Type switch
      {
        BlankType.Left => _left,
        BlankType.Right => _right,
        _ => throw new ArgumentOutOfRangeException()
      };

      return this;
    }

    public void MoveTo(Vector3 position)
    {
      transform.DOMove(position, _moveDuration);
      _renderer.sortingOrder *= -1;
    }
  }
}