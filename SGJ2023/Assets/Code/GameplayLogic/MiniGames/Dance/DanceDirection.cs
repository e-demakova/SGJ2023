using UnityEngine;

namespace GameplayLogic.MiniGames.Dance
{
  public enum DanceDirectionType
  {
    Up = 0,
    Down = 1,
    Left = 2,
    Right = 3,
  }

  public class DanceDirection : MonoBehaviour
  {
    [field: SerializeField]
    public DanceDirectionType Direction { get; private set; }
  }
}