namespace ExampleApp.Domain;

public class BusinessUnitUndirectAncestorLink
{
    /// <summary>
    /// Fake Id
    /// </summary>
    public virtual Guid Id { get; init; }

    public virtual required BusinessUnit Source { get; init; }

    public virtual required BusinessUnit Target { get; init; }
}