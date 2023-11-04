using Infrastructure.Assets;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace GameplayLogic.Map
{
  [CreateAssetMenu(menuName = AssetsMenu.DayTime, fileName = "DayTime", order = 0)]
  public class DayTimeConfig : ScriptableObject
  {
    public AssetReference Scene;
    public string TimeText;
    public Sprite BottleStatus;
  }
}