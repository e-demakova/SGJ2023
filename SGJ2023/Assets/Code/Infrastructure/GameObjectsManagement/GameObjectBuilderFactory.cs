using System.Threading.Tasks;
using Infrastructure.Assets;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Infrastructure.GameObjectsManagement
{
  [UsedImplicitly]
  public class GameObjectBuilderFactory : IGameObjectBuilderFactory
  {
    private readonly IAssetProvider _assets;

    public GameObjectBuilderFactory(IAssetProvider assets)
    {
      _assets = assets;
    }
    
    public async Task<GameObjectBuilder> Create(AssetReference asset)
    {
      GameObject prefab = await _assets.LoadAsset<GameObject>(asset);
      return new GameObjectBuilder(prefab);
    }

    public async Task<GameObjectBuilder> Create(string address)
    {
      GameObject prefab = await _assets.LoadAsset<GameObject>(address);
      return new GameObjectBuilder(prefab);
    }

    public GameObjectBuilder Create(GameObject prefab) => new(prefab);
    public GameObjectBuilder Create(Component prefab) => Create(prefab.gameObject);
  }
}