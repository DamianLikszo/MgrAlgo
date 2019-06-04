using System.Collections;
using System.Collections.Generic;

namespace magisterka.Models
{
    public class CoverageData : IList<List<int>>
    {
        private readonly IList<List<int>> _data;

        public CoverageData(IList<List<int>> data)
        {
            _data = data;
        }

        public IEnumerator<List<int>> GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(List<int> item)
        {
            _data.Add(item);
        }

        public void Clear()
        {
            _data.Clear();
        }

        public bool Contains(List<int> item)
        {
            return _data.Contains(item);
        }

        public void CopyTo(List<int>[] array, int arrayIndex)
        {
            _data.CopyTo(array, arrayIndex);
        }

        public bool Remove(List<int> item)
        {
            return _data.Remove(item);
        }

        public int Count => _data.Count;

        public bool IsReadOnly => _data.IsReadOnly;

        public int IndexOf(List<int> item)
        {
            return _data.IndexOf(item);
        }

        public void Insert(int index, List<int> item)
        {
            _data.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            _data.RemoveAt(index);
        }

        public List<int> this[int index]
        {
            get => _data[index];
            set => _data[index] = value;
        }
    }
}
