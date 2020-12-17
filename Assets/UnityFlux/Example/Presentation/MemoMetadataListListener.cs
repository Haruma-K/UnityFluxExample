using UnityEngine;
using UnityFlux.Example.Actions;
using UnityFlux.Example.View;

namespace UnityFlux.Example.Presentation
{
    public class MemoMetadataListListener : MonoBehaviour
    {
        [SerializeField] private MemoMetadataListView _view;

        private MemoActionCreator _actionCreator;

        public void Initialize(MemoActionCreator actionCreator)
        {
            _actionCreator = actionCreator;
            _view.ButtonClicked += (sender, modelId) =>
            {
                var _ = _actionCreator.ChangeEditingMemoAsync(modelId);
            };

            var __ = _actionCreator.InitializeAsync();
        }
    }
}