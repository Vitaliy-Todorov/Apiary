public interface IState
{
    void OnEnter();
    void OnEnter<T>();
    void OnEnter<T>(object signal);
    void OnEnter(object signal);

    void OnExit();
}
