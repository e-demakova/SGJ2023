﻿using UnityEngine;
using Zenject;

namespace GameplayLogic.Audio
{
  public interface IMusicService
  {
    void StartMusic(AudioClip music);
    void StopMusic();
  }

  public class MusicService : IMusicService, IInitializable
  {
    private AudioSource _audio;

    public void Initialize()
    {
      _audio = new GameObject().AddComponent<AudioSource>();
      _audio.playOnAwake = false;
      _audio.loop = true;
      Object.DontDestroyOnLoad(_audio.gameObject);
    }

    public void StartMusic(AudioClip music)
    {
      _audio.clip = music;
      _audio.Play();
    }

    public void StopMusic() =>
      _audio.Stop();
  }
}