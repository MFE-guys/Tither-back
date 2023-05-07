namespace Tither.Shared.Exceptions
{
    public class TitherValidationException : Exception
    {
        public Dictionary<string, IEnumerable<string>> Errors { get; }

        public TitherValidationException(Dictionary<string, IEnumerable<string>> errors)
            : base("Invalid data")
            => Errors = errors;
    }
}
