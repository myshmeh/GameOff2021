using System;

namespace MonoBehaviourWatcher
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Method)]
    public class Watchable : Attribute
    {
    }
}