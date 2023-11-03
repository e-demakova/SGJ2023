using System;
using System.Collections;
using System.Threading.Tasks;
using Infrastructure.Assets;
using Infrastructure.GameCore;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using Utils.Coroutines;

namespace SceneLoading
{
  public class SceneLoader : ISceneLoader, IBootstrapable
  {
    private readonly ICoroutineRunner _coroutineRunner;
    private readonly IAssetProvider _assetProvider;

    private Coroutine _levelLoading;
    public ScenesConfig Scenes { get; private set; }

    public SceneLoader(ICoroutineRunner coroutineRunner, IAssetProvider assetProvider)
    {
      _coroutineRunner = coroutineRunner;
      _assetProvider = assetProvider;
    }

    public async Task Bootstrap() =>
      Scenes = await _assetProvider.LoadAsset<ScenesConfig>(AssetsAddress.ScenesConfig);

    public void Load(AssetReference nextScene, Action onLoaded = null) =>
      _levelLoading ??= _coroutineRunner.StartCoroutine(LoadScene(nextScene, onLoaded));

    private IEnumerator LoadScene(AssetReference nextScene, Action onLoaded)
    {
      AsyncOperationHandle<SceneInstance> waitNextScene = Addressables.LoadSceneAsync(nextScene);

      while (!waitNextScene.IsDone)
        yield return null;

      _levelLoading = null;
     
      onLoaded?.Invoke();
    }
  }
}