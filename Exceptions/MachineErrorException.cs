namespace machines.Exceptions;

public class MachineErrorException : Exception
{
    public MachineErrorException(string message) : base(message)
    {
    }
}