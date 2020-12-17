namespace UnityFlux.Core
{
    public interface IStore
    {
        IDispatcher Dispatcher { get; }
        string DispatchToken { get; }
    }
}