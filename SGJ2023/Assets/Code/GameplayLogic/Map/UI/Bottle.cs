using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace GameplayLogic.Map.UI
{
  public class Bottle : MonoBehaviour
  {
    [SerializeField]
    private Image _image;

    private IDayTimeService _dayTimeService;

    [Inject]
    private void Construct(IDayTimeService dayTimeService)
    {
      _dayTimeService = dayTimeService;
    }

    private void Start()
    {
      if (_dayTimeService.Config)
        _image.sprite = _dayTimeService.Config.BottleStatus;
    }
  }
}