using Microsoft.AspNetCore.Http;

using SecuritySystem.Attributes;
using SecuritySystem.Configurator.Interfaces;
using SecuritySystem.ExternalSystem.ApplicationSecurity;
using SecuritySystem.ExternalSystem.Management;

namespace SecuritySystem.Configurator.Handlers;

public class CreatePrincipalHandler(
    [WithoutRunAs]ISecuritySystem securitySystem,
    IPrincipalManagementService principalManagementService,
    IConfiguratorIntegrationEvents? configuratorIntegrationEvents = null)
    : BaseWriteHandler, ICreatePrincipalHandler
{
    public async Task Execute(HttpContext context, CancellationToken cancellationToken)
    {
        await securitySystem.CheckAccessAsync(ApplicationSecurityRule.SecurityAdministrator, cancellationToken);

        var name = await this.ParseRequestBodyAsync<string>(context);

        var principal = await principalManagementService.CreatePrincipalAsync(name, [], cancellationToken);

        if (configuratorIntegrationEvents != null)
            await configuratorIntegrationEvents.PrincipalCreatedAsync(principal, cancellationToken);
    }
}
