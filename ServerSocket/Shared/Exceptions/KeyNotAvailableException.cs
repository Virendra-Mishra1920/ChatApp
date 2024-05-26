namespace ServerSocket.Exceptions
{
    public class KeyNotAvailableException : ICSException
    {
        public KeyNotAvailableException(string message) : base(message)
        {
        }
    }
}