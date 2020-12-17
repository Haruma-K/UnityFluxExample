using System;
using UnityEngine;
using UnityFlux.Example.Actions;
using UnityFlux.Example.View;

namespace UnityFlux.Example.Presentation
{
    public class MemoEditorListener : MonoBehaviour
    {
        [SerializeField] private MemoEditorView _view;
        
        private MemoActionCreator _actionCreator;

        public void Initialize(MemoActionCreator actionCreator)
        {
            _actionCreator = actionCreator;

            _view.TitleChanged += (sender, item) =>
            {
                var _ = actionCreator.ChangeTitle(item.id, item.title);
            };
            _view.ContentsChanged += (sender, item) =>
            {
                var _ = actionCreator.ChangeContents(item.id, item.contents);
            };
        }
    }
}