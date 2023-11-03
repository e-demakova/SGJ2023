using System;
using UnityEngine.AddressableAssets;

namespace SceneLoading
{
  public interface ISceneLoader
  {
    ScenesConfig Scenes { get; }
    void Load(AssetReference nextScene, Action onLoaded = null);
  }
}