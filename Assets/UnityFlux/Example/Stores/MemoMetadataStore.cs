using System;
using System.Collections.Generic;
using UnityEngine;
using UnityFlux.Core;
using UnityFlux.Example.Actions;
using UnityFlux.Example.Domain;

namespace UnityFlux.Example.Stores
{
    public class MemoMetadataStore : StoreBase
    {
        private readonly Dictionary<string, MemoMetadata> _items = new Dictionary<string, MemoMetadata>();

        public MemoMetadataStore(IDispatcher dispatcher) : base(dispatcher)
        {
        }

        public IReadOnlyDictionary<string, MemoMetadata> Items => _items;

        public event EventHandler<MemoMetadata> Added;
        public event EventHandler<MemoMetadata> Removed;
        public event EventHandler<MemoMetadata> Updated;

        protected override void OnDispatch(Payload payload)
        {
            switch (payload.ActionId)
            {
                case MemoActionTypes.FetchAllMetadata:
                    var fetchedMetadataList = (MemoMetadata[]) payload.Body;
                    foreach (var data in fetchedMetadataList)
                    {
                        _items.Add(data.MemoId, data);
                        Added?.Invoke(this, data);
                    }
                    break;
                case MemoActionTypes.ChangeEditingMemo:
                    break;
                case MemoActionTypes.UpdateTitle:
                    var updateTitleResult = ((string memoId, string title)) payload.Body;
                    var updateTitleItem = _items[updateTitleResult.memoId];
                    updateTitleItem.Title = updateTitleResult.title;
                    Updated?.Invoke(this, updateTitleItem);
                    break;
                case MemoActionTypes.UpdateContents:
                    break;
                case MemoActionTypes.Failure:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}