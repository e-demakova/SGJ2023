using Infrastructure.StateMachine.States;

namespace Infrastructure.StateMachine
{
  public interface IStateMachine<in T>
  {
    void Enter<TState>() where TState : class, T, IEnterState;
    void RegisterState(T state);
  }
}