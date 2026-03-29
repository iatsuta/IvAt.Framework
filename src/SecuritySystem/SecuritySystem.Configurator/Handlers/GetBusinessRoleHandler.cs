using Microsoft.AspNetCore.Http;

using SecuritySystem.Attributes;
using SecuritySystem.Configurator.Interfaces;
using SecuritySystem.Configurator.Models;
using SecuritySystem.ExternalSystem.ApplicationSecurity;
using SecuritySystem.ExternalSystem.Management;

namespace SecuritySystem.Configurator.Handlers;

public class GetBusinessRoleHandler(
    [WithoutRunAs] ISecuritySystem securitySystem,
    ISecurityRoleSource securityRoleSource,
    ISecurityOperationInfoSource securityOperationInfoSource,
    IRootPrincipalSourceService principalSourceService)
    : BaseReadHandler, IGetBusinessRoleHandler
{
    protected override async Task<object> GetDataAsync(HttpContext context, CancellationToken cancellationToken)
    {
        if (!await securitySystem.IsSecurityAdministratorAsync(cancellationToken))
        {
            return new BusinessRoleDetailsDto { Operations = [], Principals = [] };
        }
        else
        {
            var securityRole = securityRoleSource.GetSecurityRole(context.ExtractSecurityIdentity());

            var operations =
                securityRole
                    .Information
                    .Operations
                    .Select(o => new OperationDto
                    {
                        Name = o.Name,
                        Description = securityOperationInfoSource.GetSecurityOperationInfo(o).Description
                    })
                    .OrderBy(x => x.Name)
                    .ToArray();

            var principals = await principalSourceService.GetLinkedPrincipalsAsync([securityRole]).ToArrayAsync(cancellationToken);

            return new BusinessRoleDetailsDto { Operations = operations, Principals = principals };
        }
    }
}