using UnityEngine;

namespace GameplayLogic.Map
{
  public class SoundPlayer : MonoBehaviour
  {
    [SerializeField]
    private AudioSource _audio;

    public void Play() =>
      _audio.Play();
  }
}