namespace IntuitAssignment.Utils
{
    public class LRUCache<TKey, TValue>
    {
        private int _capacity;
        private Dictionary<TKey, (LinkedListNode<TKey> node, TValue value)> _cache;
        private LinkedList<TKey> _list;

        public LRUCache(int capacity)
        {
            _capacity = capacity;
            _cache = new Dictionary<TKey, (LinkedListNode<TKey> node, TValue value)>(capacity);
            _list = new LinkedList<TKey>();
        }

        public TValue Get(TKey key)
        {
            if (!_cache.ContainsKey(key))
                return default(TValue);

            var node = _cache[key];
            _list.Remove(node.node);
            _list.AddFirst(node.node);

            return node.value;
        }

        public void Put(TKey key, TValue value)
        {
            if (_cache.ContainsKey(key))
            {
                var node = _cache[key];
                _list.Remove(node.node);
                _list.AddFirst(node.node);

                _cache[key] = (node.node, value);
            }
            else
            {
                if (_cache.Count >= _capacity)
                {
                    var removeKey = _list.Last!.Value;
                    _cache.Remove(removeKey);
                    _list.RemoveLast();
                }

                // add cache
                _cache.Add(key, (_list.AddFirst(key), value));
            }
        }
    }
}
