namespace machines.Exceptions;

public class MachineDeletedException : Exception
{
    public MachineDeletedException(string message) : base(message)
    {
    }
}