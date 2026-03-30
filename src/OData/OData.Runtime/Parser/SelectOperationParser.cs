using System.Collections.Concurrent;
using System.Globalization;

using CommonFramework;
using CommonFramework.Parsing;

using OData.Domain;
using OData.Domain.QueryLanguage;
using OData.Parser.Parsing;

namespace OData.Parser;

public class RawSelectOperationParser(ICultureSource? cultureSource) : IRawSelectOperationParser
{
    private readonly ConcurrentDictionary<string, SelectOperation> cache = [];

    public SelectOperation Parse(string input) =>

        this.cache.GetOrAdd(input, _ =>
            new SelectOperationInternalParser(cultureSource?.Culture ?? CultureInfo.CurrentCulture, ParameterExpression.Default)
                .MainParser
                .Parse(input, unparsedRest => new ODataParsingException($"Can't parse: {unparsedRest}")));
}