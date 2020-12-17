using System;
using UnityFlux.Core;
using UnityFlux.Example.Actions;
using UnityFlux.Example.Api;
using UnityFlux.Example.Presentation;
using UnityFlux.Example.Stores;

namespace UnityFlux.Example
{
    public class CompositionRoot : IDisposable
    {

        public CompositionRoot(MemoEditorRenderer memoEditorRenderer,
            MemoEditorListener memoEditorListener,
        MemoMetadataListRenderer memoMetadataListRenderer,
            MemoMetadataListListener memoMetadataListListener)
        {
            var dispatcher = new Dispatcher();
            
            var memoStore = new MemoStore(dispatcher);
            var memoMetadataStore = new MemoMetadataStore(dispatcher);
            var storeRepository = new StoreRepository(memoStore, memoMetadataStore);
            
            var actionCreator = new MemoActionCreator(dispatcher, new FakeMemoApiService());

            memoEditorRenderer.Initialize(memoStore);
            memoEditorListener.Initialize(actionCreator);
            memoMetadataListRenderer.Initialize(memoMetadataStore);
            memoMetadataListListener.Initialize(actionCreator);
        }

        public void Dispose()
        {
        }
    }
}