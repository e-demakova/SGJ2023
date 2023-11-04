using Infrastructure.GameCore;
using Infrastructure.GameCore.States;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace GameplayLogic.Map
{
  public class LoadSceneAction : MonoBehaviour, IAction
  {
    [SerializeField]
    private AssetReference _scene;

    private IGameStateMachine _stateMachine;

    [Inject]
    private void Construct(IGameStateMachine stateMachine)
    {
      _stateMachine = stateMachine;
    }
    
    public void Act() =>
      _stateMachine.Enter<LoadSceneState, AssetReference>(_scene);
  }
}