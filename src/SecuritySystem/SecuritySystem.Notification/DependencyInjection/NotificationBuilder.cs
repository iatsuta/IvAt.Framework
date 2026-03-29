using CommonFramework.DependencyInjection;

using Microsoft.Extensions.DependencyInjection;

using SecuritySystem.DependencyInjection;

namespace SecuritySystem.Notification.DependencyInjection;

public class NotificationBuilder : INotificationBuilder, IServiceInitializer<ISecuritySystemBuilder>
{
    public void Initialize(ISecuritySystemBuilder securitySystemBuilder)
    {
        this.RegisterGeneralServices(securitySystemBuilder);
    }

    private ISecuritySystemBuilder RegisterGeneralServices(ISecuritySystemBuilder settings)
    {
        return settings
            .AddExtensions(services =>
            {
                services
                    .AddScoped(typeof(IDirectLevelExtractor<>), typeof(DirectLevelExtractor<>))
                    .AddScoped(typeof(INotificationPermissionExtractor<>), typeof(NotificationPermissionExtractor<>))
                    .AddScoped(typeof(INotificationPrincipalExtractor<>), typeof(NotificationPrincipalExtractor<>))

                    .AddScoped(typeof(INotificationGeneralPermissionFilterFactory<>), typeof(NotificationGeneralPermissionFilterFactory<>));
            });
    }
}