using OData.Domain;

namespace OData;

public interface ISelectOperationParser
{
    public SelectOperation<TDomainObject> Parse<TDomainObject>(string input);
}