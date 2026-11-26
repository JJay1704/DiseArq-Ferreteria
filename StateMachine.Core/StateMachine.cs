namespace StateMachine.Core;

public class StateMachine<TState> where TState : struct, Enum
{
    private readonly Dictionary<TState, HashSet<TState>> _rules;

    public StateMachine(Dictionary<TState, HashSet<TState>> rules) => _rules = rules;

    public bool CanTransition(TState from, TState to) =>
        from.Equals(to) || (_rules.TryGetValue(from, out var next) && next.Contains(to));

    public IReadOnlyCollection<TState> NextStates(TState from) =>
        _rules.TryGetValue(from, out var next) ? next : Array.Empty<TState>();

    public bool IsFinal(TState state) => NextStates(state).Count == 0;

    public IEnumerable<TState> AvailableStates(TState current) =>
        NextStates(current).Prepend(current);
}

public class StateBuilder<TState> where TState : struct, Enum
{
    internal Dictionary<TState, HashSet<TState>> Rules { get; } = new();

    public StateBuilder<TState> From(TState from, params TState[] to)
    {
        if (!Rules.TryGetValue(from, out var set))
            Rules[from] = set = new HashSet<TState>();
        foreach (var t in to) set.Add(t);
        return this;
    }
}

public abstract class StateMachineConfig<TState> where TState : struct, Enum
{
    protected abstract void OnStatesCreating(StateBuilder<TState> builder);

    public StateMachine<TState> Build()
    {
        var builder = new StateBuilder<TState>();
        OnStatesCreating(builder);
        return new StateMachine<TState>(builder.Rules);
    }
}
