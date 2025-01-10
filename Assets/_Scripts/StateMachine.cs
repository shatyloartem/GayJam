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
            if (newState.GetType() == CurrentState.GetType())
                return;
            
            CurrentState?.Exit();
            
            CurrentState = newState;
            CurrentState.Enter();
        }

        public void UpdateState() => CurrentState?.Stay();
        public void OnDestroy() => CurrentState?.OnDestroy();
    }
}