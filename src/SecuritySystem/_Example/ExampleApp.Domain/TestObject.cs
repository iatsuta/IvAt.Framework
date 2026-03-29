namespace ExampleApp.Domain;

public class TestObject
{
    public virtual Guid Id { get; set; }

    public virtual required BusinessUnit BusinessUnit { get; set; }

    public virtual required ManagementUnit ManagementUnit { get; set; }

    public virtual required Location Location { get; set; }
}