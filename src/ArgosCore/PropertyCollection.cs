using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArgosCore
{
    public class PropertyCollection : IDictionary<string, string>
    {
        IDictionary<string, string> dict = new Dictionary<string, string>();
        public string this[string key]
        {
            get
            {
                return dict[key];
            }

            set
            {
                dict[key] = value;
            }
        }

        public int Count
        {
            get
            {
                return dict.Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public ICollection<string> Keys
        {
            get
            {
                return dict.Keys;
            }
        }

        public ICollection<string> Values
        {
            get
            {
                return dict.Values;
            }
        }

        public void Add(KeyValuePair<string, string> item)
        {
            dict.Add(item.Key, item.Value);
        }

        public void Add(string key, string value)
        {
            dict.Add(key, value);
        }

        public void Clear()
        {
            dict.Clear();
        }

        public bool Contains(KeyValuePair<string, string> item)
        {
            return dict.Contains(item);
        }

        public bool ContainsKey(string key)
        {
            return dict.ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<string, string>[] array, int arrayIndex)
        {
            dict.CopyTo(array, arrayIndex);
        }

        public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
        {
            return dict.GetEnumerator();
        }

        public bool Remove(KeyValuePair<string, string> item)
        {
            return dict.Remove(item);
        }

        public bool Remove(string key)
        {
            return dict.Remove(key);
        }

        public bool TryGetValue(string key, out string value)
        {
            return dict.TryGetValue(key, out value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return dict.GetEnumerator();
        }
    }
}
