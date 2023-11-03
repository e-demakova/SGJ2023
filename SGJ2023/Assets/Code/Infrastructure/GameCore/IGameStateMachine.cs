using Infrastructure.StateMachine;
using Infrastructure.StateMachine.States;
using Observing.SubjectProperties;

namespace Infrastructure.GameCore
{
  public interface IGameStateMachine : IStateMachine<IGameState>, IPayloadStateMachine<IGameState>
  {
    ISubjectProperty<IGameState> State { get; }
  }
}