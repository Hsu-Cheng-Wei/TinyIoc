using System;
using System.Reflection;

namespace TinyIoc.Contract
{
    public interface IConstructorFinder
    {
        ConstructorInfo[] FindConstructors(Type targetType);
    }
}
