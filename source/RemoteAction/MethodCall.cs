﻿using System.Collections.Generic;
using Sync;

namespace RemoteAction
{
    /// <summary>
    ///     Represents a serializable call to a method including all invocation arguments. Method
    ///     pointer, instance and arguments have to resolved before execution. In order to resolve
    ///     a method call refer to <see cref="Registry" /> and <see cref="ArgumentFactory" />.
    /// </summary>
    public readonly struct MethodCall : ISynchronizedAction
    {
        public IEnumerable<Argument> Arguments { get; }
        public readonly MethodId Id;
        public readonly Argument Instance; // Instance to call the method on.

        public MethodCall(MethodId id, Argument instance, IEnumerable<Argument> arguments)
        {
            Arguments = arguments;
            Id = id;
            Instance = instance;
        }
        
        public override string ToString()
        {
            string sRet = Instance.EventType == EventArgType.Null ? "static " : $"{Instance} ";
            if (Registry.IdToMethod.TryGetValue(Id, out MethodAccess method))
            {
                sRet += $"{method}";
            }
            else
            {
                sRet += $"[UNREGISTERED] {Id.InternalValue}";
            }

            sRet += "(" + string.Join(", ", Arguments) + ")";
            return sRet;
        }

        public override bool Equals(object obj)
        {
            return obj is MethodCall call && this.Equals(call);
        }

        private bool Equals(MethodCall other)
        {
            return Equals(Arguments, other.Arguments) && Id.Equals(other.Id) && Instance.Equals(other.Instance);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = Id.GetHashCode();
                foreach (Argument argument in Arguments)
                {
                    hashCode = (hashCode * 397) ^ argument.GetHashCode();
                }
                hashCode = (hashCode * 397) ^ Instance.GetHashCode();
                return hashCode;
            }
        }
    }
}
