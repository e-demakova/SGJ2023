using System.Threading.Tasks;
using Infrastructure.Assets;
using Infrastructure.GameCore;

namespace GameplayLogic.Map
{
  public interface IDayTimeService
  {
    DayTimeConfig Config { get; set; }
  }

  public class DayTimeService : IDayTimeService, IBootstrapable
  {
    private readonly IAssetProvider _assetProvider;
    public DayTimeConfig Config { get; set; }

    public DayTimeService(IAssetProvider assetProvider)
    {
      _assetProvider = assetProvider;
    }
    
    public async Task Bootstrap() =>
      Config = await _assetProvider.LoadAsset<DayTimeConfig>(AssetsAddress.StartDayTime);
  }
}