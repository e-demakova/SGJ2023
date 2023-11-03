using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Infrastructure.Assets
{
  public interface IAssetProvider
  {
    Task<T> LoadAsset<T>(AssetReference asset) where T : Object;
    Task<T> LoadAsset<T>(string address) where T : Object;

    void Cleanup();
    Task<IList<TItem>> LoadAssets<TItem>(string label) where TItem : Object;
  }
}