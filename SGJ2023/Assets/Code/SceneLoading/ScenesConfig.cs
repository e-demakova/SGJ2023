using Infrastructure.Assets;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace SceneLoading
{
  [CreateAssetMenu(menuName = AssetsMenu.Scenes, fileName = "Scenes")]
  public class ScenesConfig : ScriptableObject
  {
    public AssetReference InitialScene;
  }
}