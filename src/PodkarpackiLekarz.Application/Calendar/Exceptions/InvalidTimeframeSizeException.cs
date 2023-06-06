namespace PodkarpackiLekarz.Application.Calendar.Exceptions
{
    public class InvalidTimeframeSizeException
        : Exception
    {
        public InvalidTimeframeSizeException() 
            : base("Invalid timeframe size")
        {
            
        }
    }
}
