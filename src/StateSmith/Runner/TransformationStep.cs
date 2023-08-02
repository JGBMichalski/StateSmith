using StateSmith.SmGraph;
using System;

namespace StateSmith.Runner;

public class TransformationStep
{
    public string Id { get; init; }
    public Action<StateMachine> action;

    public TransformationStep(Enum id, Action<StateMachine> action) : this(id.ToString(), action)
    {
    }

    public TransformationStep(string id, Action<StateMachine> action)
    {
        Id = id;
        this.action = action;
    }

    public static implicit operator TransformationStep(Action<StateMachine> action)
    {
        return new TransformationStep("<unspecified transformation step id>", action);
    }

    public override string ToString()
    {
        return $"Id:{Id}";
    }
}
