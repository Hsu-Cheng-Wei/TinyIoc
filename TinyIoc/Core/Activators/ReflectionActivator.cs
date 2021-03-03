using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using TinyIoc.Contract;
using TinyIoc.Extensions;

namespace TinyIoc.Core.Registration.Activators
{
    public class ReflectionActivator : InstanceActivator
    {
        private readonly IConstructorFinder _constructorFinder;

        public ReflectionActivator(Type limitType, IConstructorFinder finder) :
            base(limitType)
        {
            _constructorFinder = finder;
        }

        public override object ActivateInstance(IComponentContext context)
        {
            var ctor = DefaultConstructor();

            var @params = ctor.GetParameters();

            var paramInstances = new object[@params.Length];

            for (int i = 0; i < @params.Length; i++)
            {
                var argType = @params[i].ParameterType;

                if(!context.TryResolve(argType, out var param))
                    throw new InvalidOperationException(
                        $"TinyIoc: Can't resolve argument {argType.FullName}");

                paramInstances[i] = param;
            }

            return CtorCompiler()(paramInstances);
        }

        private ConstructorInfo DefaultConstructor()
        {
            return _constructorFinder.FindConstructors(LimitType)
                .OrderBy(c => c.GetParameters().Count()).First();
        }

        private Func<object[], object> CtorCompiler()
        {
            var ctor = DefaultConstructor();

            var @params = ctor.GetParameters();

            var paramsExpression = Expression.Parameter(typeof(object[]), "args");

            var args = new Expression[@params.Length];

            for (var i = 0; i < @params.Length; i++)
            {
                var index = Expression.ArrayIndex(paramsExpression, Expression.Constant(i));

                args[0] = Expression.Convert(index, @params[i].ParameterType);
            }

            var ctorExpression = Expression.New(DefaultConstructor(), args);

            return Expression.Lambda<Func<object[], object>>(ctorExpression, paramsExpression)
                .Compile();
        }
    }
}
