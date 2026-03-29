using System.Collections.Immutable;

namespace SecuritySystem.Notification;

public interface INotificationPrincipalExtractor<out TPrincipal>
{
    IAsyncEnumerable<TPrincipal> GetPrincipalsAsync(ImmutableArray<SecurityRole> securityRoles, ImmutableArray<NotificationFilterGroup> notificationFilterGroups);
}
