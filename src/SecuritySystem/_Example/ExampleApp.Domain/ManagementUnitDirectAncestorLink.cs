namespace ExampleApp.Domain;

public class ManagementUnitDirectAncestorLink
{
    public virtual Guid Id { get; set; }

    public virtual required ManagementUnit Ancestor { get; init; }

    public virtual required ManagementUnit Child { get; init; }
}