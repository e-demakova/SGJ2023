using Infrastructure.Assets;
using Infrastructure.GameCore;
using Infrastructure.GameCore.States;
using Infrastructure.GameObjectsManagement;
using Input;
using Map;
using SceneLoading;
using Utils.Coroutines;
using Utils.Extensions;
using Zenject;

namespace Infrastructure.Installers
{
  public class ProjectInstaller : MonoInstaller
  {
    public override void InstallBindings()
    {
      BindServices();
      BindStateMachine();
      Container.BindService<Game>();
    }

    private void BindServices()
    {
      Container.BindInterfacesTo<CoroutineRunner>()
               .FromNewComponentOnNewGameObject().AsSingle().NonLazy();

      Container.BindService<AssetProvider>();
      Container.BindService<SceneLoader>();
      Container.BindService<InputService>();
      Container.BindService<GameObjectBuilderFactory>();
      Container.BindService<MapInteractionService>();
    }

    private void BindStateMachine()
    {
      Container.BindService<GameStateMachine>();
    
      Container.FullBind<BootstrapState>();
      Container.FullBind<LoadSceneState>();
      Container.FullBind<GameLoopState>();
    }
  }
}