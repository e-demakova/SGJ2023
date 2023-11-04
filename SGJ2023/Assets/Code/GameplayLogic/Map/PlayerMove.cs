using Input;
using UnityEngine;
using Zenject;

namespace GameplayLogic.Map
{
  public class PlayerMove : MonoBehaviour
  {
    [SerializeField, Min(0)]
    private float _speed;

    private IInputService _input;

    [Inject]
    private void Construct(IInputService inputService)
    {
      _input = inputService;
    }

    private void FixedUpdate()
    {
      Vector3 direction = (Vector2) _input.InputDirection;
      transform.position += Time.fixedDeltaTime * _speed * direction;
    }
  }
}