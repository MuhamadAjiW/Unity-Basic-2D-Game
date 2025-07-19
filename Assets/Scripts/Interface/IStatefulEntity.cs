using System;

public interface IStatefulEntity
{
    int State { get; }
    EntityStateController StateController { get; }
    Action AddOnStateChange { set; }
}
