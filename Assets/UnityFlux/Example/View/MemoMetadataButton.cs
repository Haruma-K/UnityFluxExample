using System;
using UnityEngine;
using UnityEngine.UI;

namespace UnityFlux.Example.View
{
    public class MemoMetadataButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Text _text;

        public string Id { get; set; }

        private void Start()
        {
            _button.onClick.AddListener(OnButtonClicked);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(OnButtonClicked);
        }

        public event Action Clicked;

        public void SetText(string text)
        {
            _text.text = text;
        }

        private void OnButtonClicked()
        {
            Clicked?.Invoke();
        }
    }
}