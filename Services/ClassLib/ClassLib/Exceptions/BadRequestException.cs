
namespace ClassLib.Exceptions
{
    public class BadRequestException(string Message) : Exception(Message)
    {

        public BadRequestException(string message, string details) : this(message)
        {
            Details = details;
        }

        public string? Details { get; }
    }
}