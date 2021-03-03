using System;
using TinyIoc.Core;

namespace TinyIoc.Contract
{
    public interface IServiceWithType
    {
        Type ServiceType { get; }

        Service ChangeType(Type newType);
    }
}
