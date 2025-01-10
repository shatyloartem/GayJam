using _Scripts.Interfaces;

namespace _Scripts
{
    public class StateMachine
    {
        private IState CurrentState { get; set; }

        public StateMachine(IState initialState)
        {
            CurrentState = initialState;
            CurrentState.Enter();
        }

        public void ChangeState(IState newState)
        {
            CurrentState?.Exit();
            
            CurrentState = newState;
            CurrentState.Enter();
        }

        public void UpdateState()
        {
            CurrentState?.Stay();
        }
    }
}