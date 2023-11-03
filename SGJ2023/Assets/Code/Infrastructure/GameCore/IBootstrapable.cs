using System.Threading.Tasks;

namespace Infrastructure.GameCore
{
  public interface IBootstrapable
  {
    public Task Bootstrap();
  }
}