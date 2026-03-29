namespace ExampleApp.Domain;

public class Location : SecuritySystem.ISecurityContext
{
    public virtual int MyId { get; set; }

    public virtual required string Name { get; set; }
}