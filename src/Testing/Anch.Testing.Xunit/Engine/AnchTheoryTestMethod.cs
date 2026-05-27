using Xunit.v3;

namespace Anch.Testing.Xunit.Engine;

public class AnchTheoryTestMethod : XunitTestMethod, IXunitTestMethod
{
    private readonly IServiceProviderPool? serviceProviderPool;

    public AnchTheoryTestMethod()
    {
    }

    public AnchTheoryTestMethod(IXunitTestMethod baseMethod, IServiceProviderPool? serviceProviderPool)
        : base(baseMethod.TestClass, baseMethod.Method, baseMethod.TestMethodArguments, baseMethod.UniqueID)
    {
        this.serviceProviderPool = serviceProviderPool;
    }

    IReadOnlyCollection<IDataAttribute> IXunitTestMethod.DataAttributes => field ??=
    [
        .. base.DataAttributes.Select(attr =>
        {
            if (attr is IServiceProviderPoolAttribute serviceProviderPoolAttribute)
            {
                serviceProviderPoolAttribute.ServiceProviderPool = serviceProviderPool;
            }

            return attr;
        })
    ];

    string IXunitTestMethod.GetDisplayName(
        string baseDisplayName,
        string? label,
        object?[]? testMethodArguments,
        Type[]? methodGenericTypes)
    {
        var displayName = base.GetDisplayName(baseDisplayName, label, testMethodArguments, methodGenericTypes);

        if (testMethodArguments != null && base.Method.LastParameterIsCt() && base.Method.LastParameterIsCt() && displayName.EndsWith("???)"))
        {
            var skipPattern = $", {base.Method.GetParameters().Last().Name}: ???)";

            if (displayName.EndsWith(skipPattern))
            {
                return displayName[..^skipPattern.Length] + ")";
            }
        }

        return displayName;
    }
}