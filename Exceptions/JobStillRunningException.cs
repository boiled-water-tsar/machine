namespace machines.Exceptions;

public class JobStillRunningException : Exception
{
    public JobStillRunningException(string message) : base(message)
    {
    }
}