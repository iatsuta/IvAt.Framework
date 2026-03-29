// ReSharper disable once CheckNamespace
namespace SecuritySystem;

public interface ISecuritySystem
{
    ValueTask<bool> HasAccessAsync(DomainSecurityRule securityRule, CancellationToken cancellationToken = default);

    ValueTask CheckAccessAsync(DomainSecurityRule securityRule, CancellationToken cancellationToken = default);
}