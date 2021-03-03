using System;

namespace TinyIoc.Contract
{
    public interface IContainer : ILifetimeScope, IDisposable
    { }
}
