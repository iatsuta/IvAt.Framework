namespace ExampleApp.Domain.Auth.General;

public class SecurityRole
{
    public virtual Guid Id { get; init; }

    public virtual string Name { get; set; } = null!;

    public virtual string Description { get; set; } = "";
}