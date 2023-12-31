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
    private const string GlobalAudioSource = "GlobalAudioSource";
    
    private AudioSource _audio;

    public void Initialize() =>
      CreateAudio();

    private void CreateAudio()
    {
      _audio = new GameObject(GlobalAudioSource).AddComponent<AudioSource>();
      _audio.playOnAwake = false;
      _audio.loop = true;
      _audio.volume = 0.5f;

      Object.DontDestroyOnLoad(_audio.gameObject);
    }

    public void StartMusic(AudioClip music)
    {
      _audio.clip = music;
      _audio.Play();
    }

    public void StopMusic()
    {
      _audio.Stop();
    }
  }
}