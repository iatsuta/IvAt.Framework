using Anch.Core;

namespace Anch.Workflow.Serialization.Memory;

public class MemorySpecificWorkflowExternalStorageSource(
    IServiceProxyFactory serviceProxyFactory,
    IWorkflowSource workflowSource)
    : SpecificWorkflowExternalStorageSource<MemorySpecificWorkflowExternalStorage>(serviceProxyFactory, workflowSource);