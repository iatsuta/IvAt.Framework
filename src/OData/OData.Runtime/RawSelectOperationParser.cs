using CommonFramework;
using CommonFramework.Parsing;

using OData.Domain;
using OData.Domain.QueryLanguage;
using OData.InternalParser;

using System.Globalization;

namespace OData;

public class RawSelectOperationParser(IODataCache<string, SelectOperation> cache, ICultureSource? cultureSource = null) : IRawSelectOperationParser
{
    private readonly RawSelectOperationParserParser rawParser = new(cultureSource?.Culture ?? CultureInfo.CurrentCulture, ParameterExpression.Default);

    public SelectOperation Parse(string input) =>

        cache.GetOrAdd(input, _ => rawParser.MainParser.Parse(input, unparsedRest => new ODataParsingException($"Can't parse: {unparsedRest}")));
}