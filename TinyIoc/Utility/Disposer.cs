using System;
using System.Collections.Generic;
using System.Linq;
using TinyIoc.Contract;

namespace TinyIoc.Utility
{
    internal class Disposer : IDisposer
    {
        private HashSet<IDisposable> _items =
            new HashSet<IDisposable>();

        private readonly object _synchRoot = new object();

        private readonly object _caller;

        public Disposer(object caller)
        {
            _caller = caller;
        }

        public void AddInstanceForDisposal(IDisposable instance)
        {
            if (instance == null || instance.Equals(_caller)) return;

            lock (_synchRoot)
                _items.Add(instance);
        }

        public void Dispose()
        {
            lock (_synchRoot)
            {
                while (_items.Count > 0)
                {
                    var item = _items.First();

                    _items.Remove(item);

                    item.Dispose();
                }
                _items = null;
            }
        }
    }
}
