namespace CommonFramework.Parsing;

public static class ParserExtensions
{
    public static TValue Parse<TInput, TValue>(this Parser<TInput, TValue> parser, TInput value) =>
        parser.Parse(value, unparsedRest => new Exception($"Can't parse: {unparsedRest}"));

    public static TValue Parse<TInput, TValue>(this Parser<TInput, TValue> parser, TInput value, Func<TInput, Exception> getUnparsedRestException)
    {
        var parsingResult = parser(value);

        if (parsingResult.HasError)
        {
            throw getUnparsedRestException(parsingResult.Rest);
        }
        else
        {
            return parsingResult.Value;
        }
    }
}