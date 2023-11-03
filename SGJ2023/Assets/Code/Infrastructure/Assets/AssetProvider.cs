using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Object = UnityEngine.Object;

namespace Infrastructure.Assets
{
  [UsedImplicitly]
  public class AssetProvider : IAssetProvider
  {
    private readonly Dictionary<string, CashedAsyncHandle> _completedCash = new();
    
    public void Cleanup()
    {
      foreach (CashedAsyncHandle handle in _completedCash.Values)
        handle.Release();

      _completedCash.Clear();
    }

    public async Task<T> LoadAsset<T>(AssetReference asset) where T : Object
    {
      CashedAsyncHandle handle = GetHandle(asset.AssetGUID);
      return await handle.Result<T>();
    }

    public async Task<T> LoadAsset<T>(string address) where T : Object
    {
      CashedAsyncHandle handle = GetHandle(address);
      T result = await handle.Result<T>();

      if (result == null)
        Debug.LogException(new Exception($"Asset by type {typeof(T).Name} at address {address} didn't exist"));

      return result;
    }

    public async Task<IList<TItem>> LoadAssets<TItem>(string label) where TItem : Object
    {
      CashedAsyncHandle handle = GetHandle(label);
      IList<TItem> result = await handle.Results<TItem>();

      if (result == null)
        Debug.LogException(new Exception($"Asset by type {typeof(IList<TItem>).Name} at address {label} didn't exist"));

      return result;
    }

    private CashedAsyncHandle GetHandle(string key)
    {
      if (!_completedCash.TryGetValue(key, out CashedAsyncHandle completedHandle))
      {
        completedHandle = new CashedAsyncHandle(key);
        _completedCash[key] = completedHandle;
      }

      return completedHandle;
    }
  }
}