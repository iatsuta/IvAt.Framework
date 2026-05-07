using Anch.GenericRepository;
using Anch.Workflow.Domain.Runtime;
using Anch.Workflow.Serialization;

using Microsoft.Extensions.DependencyInjection;

namespace Anch.Workflow.IntegrationTests._Base;

public static class ServiceProviderExtensions
{
    extension(IServiceProvider serviceProvider)
    {
        public IWorkflowHost WorkflowHost => serviceProvider.GetRequiredService<IWorkflowHost>();

        public IWorkflowExecutor WorkflowExecutor => serviceProvider.WorkflowHost.CreateExecutor(WorkflowExecutionPolicy.TillTheEnd);

        public IGenericRepository GenericRepository => serviceProvider.GetRequiredService<IGenericRepository>();

        public IWorkflowRepository WorkflowRepository => serviceProvider.GetRequiredService<IWorkflowRepository>();

        public IQueryableSource QueryableSource => serviceProvider.GetRequiredService<IQueryableSource>();

        public async ValueTask<WorkflowInstance> StartWorkflow<TSource, TWorkflow>(TSource source, CancellationToken ct)
            where TWorkflow : IWorkflow<TSource>
            where TSource : notnull =>

            (await serviceProvider.WorkflowHost.CreateExecutor(WorkflowExecutionPolicy.TillTheEnd).Start<TSource, TWorkflow>(source, ct)).Modified.First();
    }
}