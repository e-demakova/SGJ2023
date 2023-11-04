using Infrastructure.StateMachine.States;
using Input;
using SceneLoading;
using UnityEngine.AddressableAssets;

namespace Infrastructure.GameCore.States
{
  public class LoadSceneState : IGameState, IEnterState, IPayloadState<AssetReference>
  {
    private IGameStateMachine _stateMachine;
    private ISceneLoader _sceneLoader;
    private IInputService _inputService;
    
    public LoadSceneState(IGameStateMachine stateMachine, ISceneLoader sceneLoader, IInputService inputService)
    {
      _stateMachine = stateMachine;
      _sceneLoader = sceneLoader;
      _inputService = inputService;
    }

    public void Enter() =>
      StartLoad(_sceneLoader.Scenes.InitialScene);

    public void Enter(AssetReference payload) =>
      StartLoad(payload);

    private void StartLoad(AssetReference scene)
    {
      _inputService.Enabled = false;
      Load(scene);
    }

    private void Load(AssetReference scene) =>
      _sceneLoader.Load(scene, EnterNextState);

    private void EnterNextState() =>
      _stateMachine.Enter<GameLoopState>();
  }
}