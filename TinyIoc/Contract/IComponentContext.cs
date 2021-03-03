using System.Collections.Generic;

namespace TinyIoc.Contract
{
    public interface IComponentContext
    {
        IComponentRegistry ComponentRegistry { get; }

        object ResolveComponent(IComponentRegistration registration);
    }
}
