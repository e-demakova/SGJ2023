using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Object = UnityEngine.Object;

namespace Infrastructure.Assets
{
  public class CashedAsyncHandle
  {
    private readonly string _key;
    private AsyncOperationHandle _cashedHandle;

    public CashedAsyncHandle(string key)
    {
      _key = key;
    }

    public async Task<T> Result<T>() where T : Object
    {
      if (TryGetResult(out T result))
        return result;

      AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(_key);
      AddHandle(handle);

      return await handle.Task;
    }

    public async Task<IList<TItem>> Results<TItem>() where TItem : Object
    {
      if (TryGetResult(out IList<TItem> result))
        return result;

      AsyncOperationHandle<IList<TItem>> handle = Addressables.LoadAssetsAsync<TItem>(_key, _ => { });
      AddHandle(handle);

      return await handle.Task;
    }

    private bool TryGetResult<T>(out T result)
    {
      result = default;

      bool done = _cashedHandle.IsDone && _cashedHandle.IsValid();
      if (done)
        result = (T) _cashedHandle.Result;

      return done;
    }

    private void AddHandle(AsyncOperationHandle handle)
    {
      if (_cashedHandle.IsValid())
        handle.Completed += Addressables.Release;
      else
        _cashedHandle = handle;
    }

    public void Release() =>
      Addressables.Release(_cashedHandle);
  }
}