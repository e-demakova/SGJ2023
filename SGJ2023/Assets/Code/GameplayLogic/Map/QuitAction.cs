using DG.Tweening;
using Infrastructure.GameCore;
using Infrastructure.GameCore.States;
using Input;
using SceneLoading;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;
using Zenject;

namespace GameplayLogic.Map
{
  public class QuitAction : MonoBehaviour, IAction
  {
    [SerializeField]
    private Image _dark;

    [SerializeField, Min(0)]
    private float _duration = 1f;
    
    private IGameStateMachine _stateMachine;
    private ISceneLoader _sceneLoader;
    private IInputService _input;

    [Inject]
    private void Construct(IGameStateMachine stateMachine, ISceneLoader sceneLoader, IInputService input)
    {
      _stateMachine = stateMachine;
      _sceneLoader = sceneLoader;
      _input = input;
    }
    
    public void Act()
    {
      _input.Enabled = false;
      _dark.DOFade(1, _duration).OnComplete(Quit);
    }

    private void Quit()
    {
      _stateMachine.Enter<LoadSceneState, AssetReference>(_sceneLoader.Scenes.QuitScene);
    }
  }
}