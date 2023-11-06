using System;
using Observing.GameObjects.Physics;
using UnityEngine;
using UnityEngine.Rendering;

namespace GameplayLogic.Map
{
  public class MomBody : MonoBehaviour
  {
    [SerializeField]
    private AudioSource _audio;
    
    [SerializeField]
    private Collider2D _collider;
    
    [SerializeField]
    private VolumeProfile _profile;
    
    [SerializeField]
    private Volume _volume;

    private IDisposable _subscriber;

    private void Awake() =>
      _subscriber = _collider.TriggerEnter2D().Subscribe(UpdateVolume);

    private void OnDestroy() =>
      _subscriber.Dispose();

    private void UpdateVolume()
    {
      _subscriber.Dispose();
      _volume.profile = _profile;
      _audio.Play();
    }
  }
}