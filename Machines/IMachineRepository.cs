namespace machines;

public interface IMachineRepository
{
    Task AddMachineAsync(Machine machine);

    Task<Machine> GetMachineAsync(string machineName);
}