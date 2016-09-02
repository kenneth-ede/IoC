using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace IOC {
    public class Container
    {

        private Dictionary<Type, Type> _map = new Dictionary<Type, Type>();

        public ContainerBuilder For<TSource>()
        {
            return For(typeof(TSource));
        }

        public ContainerBuilder For(Type sourceType) 
        {
            return new ContainerBuilder(this, sourceType);
        }

        public TSource Resolve<TSource>()
        {
            return (TSource)Resolve(typeof(TSource));
        }

        public object Resolve(Type sourceType)
        {
            if (_map.ContainsKey(sourceType))
            {
                var destinationType = _map[sourceType];
                return CreateInstance(destinationType);
            }

            if (sourceType.IsGenericType && _map.ContainsKey(sourceType.GetGenericTypeDefinition()))
            {
                var destination = _map[sourceType.GetGenericTypeDefinition()];
                var destinationType = destination.MakeGenericType(sourceType.GenericTypeArguments);
                return CreateInstance(destinationType);
            }

            if (!sourceType.IsInterface)
            {
                return CreateInstance(sourceType);
            }

            throw new InvalidOperationException("Could not resolve " + sourceType.FullName);
        }

        private object CreateInstance(Type destinationType)
        {
            var parameters = destinationType.GetConstructors()
                                .OrderByDescending(c => c.GetParameters().Length)
                                .First()
                                .GetParameters()
                                    .Select(p => Resolve(p.ParameterType))
                                    .ToArray();

            return Activator.CreateInstance(destinationType, parameters);
        }

        public class ContainerBuilder
        {
            private readonly Container _container;
            private readonly Type _sourceType;

            public ContainerBuilder(Container container, Type sourceType)
            {
                _container = container;
                _sourceType = sourceType;
            }

            public ContainerBuilder Use<TDestination>() {
                return Use(typeof(TDestination));
            }

            public ContainerBuilder Use(Type destinationType)
            {
                _container._map.Add(_sourceType, destinationType);
                return this;
            }
        }
    }
}
