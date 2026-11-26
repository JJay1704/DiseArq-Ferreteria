using StateMachine.Core;

namespace Ferreteria.States;

public sealed class VentaStateMachineConfig : StateMachineConfig<EstadoVenta>
{
    protected override void OnStatesCreating(StateBuilder<EstadoVenta> builder)
    {
        builder.From(EstadoVenta.Pendiente, EstadoVenta.Pagada, EstadoVenta.Anulada);
    }
}
