using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Infrastructure.GameObjectsManagement
{
  public interface IGameObjectBuilderFactory
  {
    Task<GameObjectBuilder> Create(AssetReference asset);
    Task<GameObjectBuilder> Create(string address);

    GameObjectBuilder Create(GameObject prefab);
    GameObjectBuilder Create(Component prefab);
  }
}