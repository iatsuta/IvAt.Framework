using CommonFramework;

namespace SecuritySystem.DiTests;

public class XUnitDefaultCancellationTokenSource : IDefaultCancellationTokenSource
{
    public CancellationToken CancellationToken => TestContext.Current.CancellationToken;
}