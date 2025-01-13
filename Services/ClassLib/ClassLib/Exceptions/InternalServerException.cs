
namespace ClassLib.Exceptions
{
    public class InternalServerException(string Message) : Exception(Message)
    {

        public InternalServerException(string message, string details) : this(message)
        {
            Details = details;
        }

        public string? Details { get; }
    }
}
