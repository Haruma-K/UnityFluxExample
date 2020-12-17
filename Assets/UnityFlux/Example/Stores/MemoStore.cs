using System;
using System.Collections.Generic;
using UnityEngine;
using UnityFlux.Core;
using UnityFlux.Example.Actions;
using UnityFlux.Example.Domain;

namespace UnityFlux.Example.Stores
{
    public class MemoStore : StoreBase
    {
        private readonly Dictionary<string, Memo> _items = new Dictionary<string, Memo>();

        private string _editingId;
        
        public MemoStore(IDispatcher dispatcher) : base(dispatcher)
        {
        }

        public IReadOnlyDictionary<string, Memo> Items => _items;
        public string EditingId => _editingId;

        public event EventHandler<Memo> Updated;
        public event EventHandler<Memo> EditingMemoChanged;

        protected override void OnDispatch(Payload payload)
        {
            switch (payload.ActionId)
            {
                case MemoActionTypes.FetchAllMetadata:
                    // こうやれば他のストアの処理を待つことができる
                    //Dispatcher.WaitFor(StoreRepository.Instance.MemoMetadataStore.DispatchToken);
                    break;
                case MemoActionTypes.ChangeEditingMemo:
                    var memo = (Memo) payload.Body;
                    _items[memo.Id] = memo;
                    _editingId = memo.Id;
                    EditingMemoChanged?.Invoke(this, memo);
                    break;
                case MemoActionTypes.UpdateTitle:
                    var updateTitleResult = ((string memoId, string title)) payload.Body;
                    var updateTitleItem = _items[updateTitleResult.memoId];
                    updateTitleItem.Metadata.Title = updateTitleResult.title;
                    Updated?.Invoke(this, updateTitleItem);
                    break;
                case MemoActionTypes.UpdateContents:
                    var updateContentsResult = ((string memoId, string contents)) payload.Body;
                    var updateContentsItem = _items[updateContentsResult.memoId];
                    updateContentsItem.Contents = updateContentsResult.contents;
                    Updated?.Invoke(this, updateContentsItem);
                    break;
                case MemoActionTypes.Failure:
                    Debug.LogError((string) payload.Body);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}