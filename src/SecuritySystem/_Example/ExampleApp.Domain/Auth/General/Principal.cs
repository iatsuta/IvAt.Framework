namespace ExampleApp.Domain.Auth.General;

public class Principal
{
    public virtual Guid Id { get; init; }

    public virtual string Name { get; init; } = null!;

    public virtual Principal? RunAs { get; set; }
}