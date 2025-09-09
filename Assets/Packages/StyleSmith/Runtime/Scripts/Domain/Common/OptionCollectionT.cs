using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace StyleSmith.Runtime.Domain
{
    [Serializable]
    public sealed class OptionCollection<T> : IList<T>, IList where T : IOption
    {
        [SerializeField] private List<T> _options = new();
        private Dictionary<string, T> OptionsByName => _options.ToDictionary(x => x.Name, x => x);

        // Dictionary-like access by name
        public T this[string name]
        {
            get => OptionsByName.GetValueOrDefault(name);
            set
            {
                if (value != null)
                {
                    if (OptionsByName.ContainsKey(name))
                    {
                        var existingIndex = _options.FindIndex(o => o.Name == name);
                        _options[existingIndex] = value;
                    }
                    else
                    {
                        _options.Add(value);
                    }
                }
            }
        }

        // IList<T> implementation
        public int IndexOf(T item)
        {
            return _options.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            if (item == null) return;

            if (OptionsByName.ContainsKey(item.Name))
            {
                var existingIndex = _options.FindIndex(o => o.Name == item.Name);
                _options.RemoveAt(existingIndex);
                if (existingIndex < index) index--;
            }

            _options.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            _options.RemoveAt(index);
        }

        public T this[int index]
        {
            get => _options[index];
            set => _options[index] = value;
        }

        // IList (non-generic) implementation
        object IList.this[int index]
        {
            get => _options[index];
            set => _options[index] = (T)value;
        }

        int IList.Add(object value)
        {
            Add((T)value);
            return Count - 1;
        }

        bool IList.Contains(object value)
        {
            return value is T item && Contains(item);
        }

        int IList.IndexOf(object value)
        {
            return value is T item ? IndexOf(item) : -1;
        }

        void IList.Insert(int index, object value)
        {
            if (value is T item)
                Insert(index, item);
        }

        void IList.Remove(object value)
        {
            if (value is T item)
                Remove(item);
        }

        bool IList.IsFixedSize => false;

        // ICollection<T> implementation
        public IEnumerator<T> GetEnumerator()
        {
            return _options.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(T item)
        {
            if (item == null) return;

            if (OptionsByName.ContainsKey(item.Name))
            {
                var existingIndex = _options.FindIndex(o => o.Name == item.Name);
                _options[existingIndex] = item;
            }
            else
            {
                _options.Add(item);
            }
        }

        public void Clear()
        {
            _options.Clear();
        }

        public bool Contains(T item)
        {
            return _options.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            _options.CopyTo(array, arrayIndex);
        }

        public bool Remove(T item)
        {
            return item != null && _options.Remove(item);
        }

        public bool Remove(string name)
        {
            if (OptionsByName.TryGetValue(name, out var option))
            {
                _options.Remove(option);
                return true;
            }

            return false;
        }

        // ICollection (non-generic) implementation
        void ICollection.CopyTo(Array array, int index)
        {
            ((ICollection)_options).CopyTo(array, index);
        }

        bool ICollection.IsSynchronized => false;
        object ICollection.SyncRoot => this;

        public int Count => _options.Count;
        public bool IsReadOnly => false;
    }
}