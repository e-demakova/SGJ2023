using GameplayLogic.Map;
using TMPro;
using UnityEngine;
using Zenject;

namespace GameplayLogic
{
  public class CurrentTimeScreen : MonoBehaviour
  {
    [SerializeField]
    private TextMeshProUGUI _text;

    private IDayTimeService _dayTimeService;

    [Inject]
    private void Construct(IDayTimeService dayTimeService)
    {
      _dayTimeService = dayTimeService;
    }

    private void Start() =>
      _text.text = _dayTimeService.Config.TimeText;
  }
}