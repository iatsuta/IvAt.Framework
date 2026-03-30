using OData.Domain;

namespace OData;

public interface IRawSelectOperationParser
{
    public SelectOperation Parse(string input);
}