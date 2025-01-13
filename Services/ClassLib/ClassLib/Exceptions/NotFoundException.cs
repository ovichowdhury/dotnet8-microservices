
namespace ClassLib.Exceptions
{
    public class NotFoundException(string Message) : Exception(Message)
    {
        public NotFoundException(string name, object key) : this($"Entity \"{name}\" ({key}) was not found.")
        {

        }
    }
}
