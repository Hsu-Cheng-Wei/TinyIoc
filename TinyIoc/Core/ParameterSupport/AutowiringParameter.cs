using System;
using System.Reflection;
using TinyIoc.Contract;

namespace TinyIoc.Core.ParameterSupport
{
    public class AutowiringParameter : Parameter
    {
        public AutowiringParameter(IComponentContext context) : base(context) { }

        public override bool CanSupplyValue(ParameterInfo parameterInfo, out Func<object> activiator)
        {
            activiator = null;

            var type = new TypedService(parameterInfo.ParameterType);

            if (!Context.ComponentRegistry.TryGetRegistration(type, out var re)) return false;

            activiator = () => Context.ResolveComponent(re);

            return true;
        }
    }
}
