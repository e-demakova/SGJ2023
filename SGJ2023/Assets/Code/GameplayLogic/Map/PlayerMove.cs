using Input;
using UnityEngine;
using Zenject;

namespace GameplayLogic.Map
{
  public class PlayerMove : MonoBehaviour
  {
    private readonly int _moveId = Animator.StringToHash("Move");

    [SerializeField, Min(0)]
    private float _speed;

    [SerializeField]
    private Animator _animator;

    [SerializeField]
    private SpriteRenderer _renderer;

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

      _animator.SetBool(_moveId, direction.magnitude > 0);

      _renderer.flipX = direction.x switch
      {
        > 0 => true,
        < 0 => false,
        _ => _renderer.flipX
      };
    }
  }
}