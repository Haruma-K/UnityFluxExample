using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UnityFlux.Example.View
{
    public class MemoMetadataListView : MonoBehaviour
    {
        [SerializeField] private VerticalLayoutGroup _layoutGroup;
        [SerializeField] private GameObject _itemPrefab;

        private readonly Dictionary<string, MemoMetadataButton> _items = new Dictionary<string, MemoMetadataButton>();
        public EventHandler<string> ButtonClicked;

        public void AddItem(string id, string title)
        {
            var item = Instantiate(_itemPrefab);
            var button = item.GetComponent<MemoMetadataButton>();
            button.SetText(title);
            _items.Add(id, button);
            button.transform.SetParent(transform, false);
            button.Clicked += () => ButtonClicked?.Invoke(button, id);
        }

        public void RemoveItem(string id)
        {
            _items.Remove(id);
        }

        public void UpdateItem(string id, string title)
        {
            _items[id].SetText(title);
        }
    }
}