namespace CompositionRoot.States
{
    public interface IState
    {
        void Enter();
    }

    public interface IExitableState : IState
    {
        void Exit();
    }
}