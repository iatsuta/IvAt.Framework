namespace ExampleApp.Domain.Auth.Virtual;

public class TestManager
{
    public virtual Guid Id { get; set; }

    public virtual required Employee Employee { get; set; }

    public virtual required BusinessUnit BusinessUnit { get; set; }

    public virtual required Location Location { get; set; }
}