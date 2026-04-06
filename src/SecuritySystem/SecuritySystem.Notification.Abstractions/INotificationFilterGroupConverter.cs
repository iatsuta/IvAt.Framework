using SecuritySystem.Notification.Domain;

namespace SecuritySystem.Notification;

public interface INotificationFilterGroupConverter
{
    NotificationFilterGroup<TSecurityContextIdent> Convert<TSecurityContextIdent>(NotificationFilterGroup notificationFilterGroup)
        where TSecurityContextIdent : notnull;
}