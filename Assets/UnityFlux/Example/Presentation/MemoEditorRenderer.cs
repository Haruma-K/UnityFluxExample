using UnityEngine;
using UnityFlux.Example.Stores;
using UnityFlux.Example.View;

namespace UnityFlux.Example.Presentation
{
    public class MemoEditorRenderer : MonoBehaviour
    {
        [SerializeField] private MemoEditorView _view;
        
        public void Initialize(MemoStore memoStore)
        {
            memoStore.EditingMemoChanged += (sender, memo) =>
            {
                _view.Setup(memo.Id, memo.Metadata.Title, memo.Contents);
            };
        }
    }
}
