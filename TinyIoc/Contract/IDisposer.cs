using System;

namespace TinyIoc.Contract
{
    public interface IDisposer : IDisposable
    {
        void AddInstanceForDisposal(IDisposable instance);
    }
}
