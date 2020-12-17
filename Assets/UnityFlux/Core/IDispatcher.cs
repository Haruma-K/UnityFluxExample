using System;

namespace UnityFlux.Core
{
    public interface IDispatcher
    {
        bool IsDispatching { get; }

        string Register(Action<Payload> callback);
        bool Unregister(string dispatchToken);
        void Dispatch(Payload payload);
        void WaitFor(params string[] dispatchTokens);
    }
}