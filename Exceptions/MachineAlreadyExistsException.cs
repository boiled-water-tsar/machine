namespace machines.Exceptions;

public class MachineAlreadyExistsException : Exception
{
    public MachineAlreadyExistsException(string message) : base(message)
    {
    }
}