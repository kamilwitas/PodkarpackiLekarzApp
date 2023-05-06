namespace PodkarpackiLekarz.Shared.Exceptions
{
    public class ValidationErrorException : Exception
    {
        private readonly List<ValidationError> _validationErrors;

        public List<ValidationError> ValidationErrors => _validationErrors;

        public ValidationErrorException(List<ValidationError> validationErrors)
        : base("Validation error")
        {
            _validationErrors = validationErrors;
        }
    }
}