namespace ExampleApp.Domain;

public class ManagementUnitUndirectAncestorLink
{
    /// <summary>
    /// Fake Id
    /// </summary>
    public virtual Guid Id { get; init; }

    public virtual required ManagementUnit Source { get; init; }

    public virtual required ManagementUnit Target { get; init; }
}