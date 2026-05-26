namespace Anch.Testing.Database.ConnectionStringManagement;

public record TestConnectionStringRole(string Name)
{
    public static TestConnectionStringRole EmptySnapshot { get; } = new(nameof(EmptySnapshot));

    public static TestConnectionStringRole FilledSnapshot { get; } = new(nameof(FilledSnapshot));
}