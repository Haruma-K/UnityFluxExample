using UnityEngine;
using UnityEngine.Assertions;
using UnityFlux.Example.Presentation;

namespace UnityFlux.Example
{
    public class EntryPoint : MonoBehaviour
    {
        private CompositionRoot _compositionRoot;

        [SerializeField] private MemoEditorRenderer _memoEditorRenderer;
        [SerializeField] private MemoEditorListener _memoEditorListener;
        [SerializeField] private MemoMetadataListRenderer _memoMetadataListRenderer;
        [SerializeField] private MemoMetadataListListener _memoMetadataListListner;

        private void Start()
        {
            Assert.IsNotNull(_memoEditorRenderer);
            Assert.IsNotNull(_memoEditorListener);
            Assert.IsNotNull(_memoMetadataListRenderer);
            Assert.IsNotNull(_memoMetadataListListner); 
            
            _compositionRoot = new CompositionRoot(_memoEditorRenderer,
                _memoEditorListener, _memoMetadataListRenderer, _memoMetadataListListner);
        }

        private void OnDestroy()
        {
            _compositionRoot.Dispose();
        }
    }
}