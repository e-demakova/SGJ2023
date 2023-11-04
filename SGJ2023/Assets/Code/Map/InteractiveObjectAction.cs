using UnityEngine;

namespace Map
{
  public interface IAction
  {
    void Act();
  }

  public class InteractiveObjectAction : MonoBehaviour, IAction
  {
    public void Act()
    {
      Debug.Log("Act");
    }
  }
}