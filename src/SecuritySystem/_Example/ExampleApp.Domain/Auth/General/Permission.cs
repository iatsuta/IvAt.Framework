namespace ExampleApp.Domain.Auth.General;

public class Permission
{
    public virtual Guid Id { get; init; }

    public virtual Principal Principal { get; init; } = null!;

    public virtual SecurityRole SecurityRole { get; init; } = null!;

    public virtual Permission? DelegatedFrom { get; init; }

    public virtual DateTime StartDate { get; set; }

    public virtual DateTime? EndDate { get; set; }

    public virtual string Comment { get; set; } = "";

    public virtual string ExtendedValue { get; set; } = "";
}