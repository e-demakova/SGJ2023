﻿using GameplayLogic.Audio;
using GameplayLogic.Map;
using Infrastructure.Assets;
using Infrastructure.GameCore;
using Infrastructure.GameCore.States;
using Infrastructure.GameObjectsManagement;
using Input;
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
      Container.BindService<DayTimeService>();
      Container.BindService<MusicService>();
      Container.BindService<PoliceHairstyleClue>();
    }

    private void BindStateMachine()
    {
      Container.BindService<GameStateMachine>();
    
      Container.FullBind<BootstrapState>();
      Container.FullBind<LoadSceneState>();
      Container.FullBind<GameLoopState>();
      Container.FullBind<ChangeDayTimeState>();
    }
  }
}