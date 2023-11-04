namespace GameplayLogic.Map
{
  public interface IDayTimeService
  {
    DayTimeConfig Config { get; set; }
  }

  public class DayTimeService : IDayTimeService
  {
    public DayTimeConfig Config { get; set; }
  }
}