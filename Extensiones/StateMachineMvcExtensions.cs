using Microsoft.AspNetCore.Mvc.Rendering;
using StateMachine.Core;

public static class StateMachineMvcExtensions
{
    public static SelectList ToSelectList<TState>(this StateMachine<TState> sm, TState current)
        where TState : struct, Enum =>
        new(sm.AvailableStates(current)
              .Select(s => new { Id = Convert.ToInt32(s), Nombre = s.ToString() }),
            "Id", "Nombre", Convert.ToInt32(current));

    public static IServiceCollection AddStateMachine<TConfig, TState>(this IServiceCollection services)
        where TConfig : StateMachineConfig<TState>, new()
        where TState : struct, Enum
        => services.AddSingleton(new TConfig().Build());
}
