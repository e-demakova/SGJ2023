namespace GameplayLogic.Map
{
  public interface IPoliceHairstyleClue
  {
    int DeclaredHairstyle { get; set; }
    int ActualHairstyle { get; set; }
  }

  public class PoliceHairstyleClue : IPoliceHairstyleClue
  {
    public int DeclaredHairstyle { get; set; }
    public int ActualHairstyle { get; set; }
  }
}