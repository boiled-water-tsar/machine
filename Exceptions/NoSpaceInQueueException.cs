namespace machines.Exceptions;

public class NoSpaceInQueueException : Exception
{
    public NoSpaceInQueueException(string message) : base(message)
    {
    }
}