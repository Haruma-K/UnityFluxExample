using System;

namespace UnityFlux.Core
{
    public abstract class StoreBase : IStore, IDisposable
    {
        public StoreBase(IDispatcher dispatcher)
        {
            Dispatcher = dispatcher;
            DispatchToken = Dispatcher.Register(OnDispatch);
        }

        public void Dispose()
        {
            Dispatcher.Unregister(DispatchToken);
        }

        public IDispatcher Dispatcher { get; }
        public string DispatchToken { get; }

        protected abstract void OnDispatch(Payload payload);
    }
}