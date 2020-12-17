using System;
using UnityEngine;
using UnityEngine.UI;

namespace UnityFlux.Example.View
{
    public class MemoEditorView : MonoBehaviour
    {
        [SerializeField] private InputField _titleField;
        [SerializeField] private InputField _contentsField;

        private string _currentId;
        public EventHandler<(string id, string title)> TitleChanged;
        public EventHandler<(string id, string contents)> ContentsChanged;

        private void Start()
        {
            _titleField.onValueChanged.AddListener(x => TitleChanged?.Invoke(_titleField, (_currentId, x)));
            _contentsField.onValueChanged.AddListener(x => ContentsChanged?.Invoke(_contentsField, (_currentId, x)));
        }

        public void Setup(string id, string title, string contents)
        {
            _currentId = id;
            _titleField.SetTextWithoutNotify(title);
            _contentsField.SetTextWithoutNotify(contents);
        }
    }
}