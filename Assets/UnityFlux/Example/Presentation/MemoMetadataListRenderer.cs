using UnityEngine;
using UnityFlux.Example.Stores;
using UnityFlux.Example.View;

namespace UnityFlux.Example.Presentation
{
    public class MemoMetadataListRenderer : MonoBehaviour
    {
        [SerializeField] private MemoMetadataListView _view;

        public void Initialize(MemoMetadataStore store)
        {
            store.Added += (sender, metadata) => { _view.AddItem(metadata.MemoId, metadata.Title); };
            store.Removed += (sender, metadata) => { _view.RemoveItem(metadata.MemoId); };
            store.Updated += (sender, metadata) => { _view.UpdateItem(metadata.MemoId, metadata.Title); };
        }
    }
}