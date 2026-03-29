using CommonFramework.DependencyInjection;

using SecuritySystem.DependencyInjection;

namespace SecuritySystem.Notification.DependencyInjection;

public static class SecuritySystemBuilderExtensions
{
    extension(ISecuritySystemBuilder securitySystemBuilder)
    {
        public ISecuritySystemBuilder AddNotification(Action<INotificationBuilder>? setupAction = null) =>
            securitySystemBuilder.Initialize<ISecuritySystemBuilder, NotificationBuilder>(new (), setupAction);
    }
}