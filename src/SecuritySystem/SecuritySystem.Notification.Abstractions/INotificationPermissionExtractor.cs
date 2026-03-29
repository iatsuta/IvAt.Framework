using System.Collections.Immutable;

namespace SecuritySystem.Notification;

public interface INotificationPermissionExtractor<out TPermission>
{
    IAsyncEnumerable<TPermission> GetPermissionsAsync(ImmutableArray<SecurityRole> securityRoles, ImmutableArray<NotificationFilterGroup> notificationFilterGroups);
}