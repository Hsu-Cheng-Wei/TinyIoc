using System;
using System.Collections.Generic;
using TinyIoc.Contract;
using TinyIoc.Core;
using TinyIoc.Core.Feature;

namespace TinyIoc.Builder
{
    public class TinyContainerBuilder
    {
        private readonly IList<Action<IComponentRegistry>> _lazyConfiguration =
            new List<Action<IComponentRegistry>>();

        public void AddLazyConfiguration(Action<IComponentRegistry> action)
        {
            _lazyConfiguration.Add(action);
        }

        private bool _isBuild = false;
        public TinyContainer Build()
        {
            if (_isBuild) throw new InvalidOperationException("TinyContainerBuilder have builded");

            var container = CreateContainer();

            Configure(container.ComponentRegistry);

            _isBuild = true;

            return container;
        }

        private void Configure(IComponentRegistry componentRegistry)
        {
            foreach (var cfgAct in _lazyConfiguration)
                cfgAct(componentRegistry);
        }

        private static TinyContainer CreateContainer()
        {
            var container = new TinyContainer();

            container.ComponentRegistry.AddRegistrationSource(new CollectionRegistrationSource());

            return container;
        }
    }
}
