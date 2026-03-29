namespace ExampleApp.Domain.Auth.Virtual;

public class Administrator
{
    public virtual Guid Id { get; set; }

    public virtual Employee Employee { get; set; } = null!;
}