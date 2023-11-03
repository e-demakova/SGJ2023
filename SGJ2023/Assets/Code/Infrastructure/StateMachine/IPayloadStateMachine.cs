using Infrastructure.StateMachine.States;

namespace Infrastructure.StateMachine
{
  public interface IPayloadStateMachine<in T>
  {
    void Enter<TState, TPayload>(TPayload payload) where TState : class, T, IPayloadState<TPayload>;
  }
}