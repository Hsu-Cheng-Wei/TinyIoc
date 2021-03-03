using System;
using System.Reflection;
using TinyIoc.Contract;

namespace TinyIoc.Utility
{
    internal class DefaultConstructorFinder: IConstructorFinder
    {
        public ConstructorInfo[] FindConstructors(Type targetType)
        => targetType.GetConstructors();
    }
}
