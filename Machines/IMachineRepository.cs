namespace machines;

public interface IMachineRepository
{
    Machine CreateAsync(string machineName);
    
    Machine GetMachineAsync(string machineName);

    IQueryable<Machine> GetMachinesAsync();

    Machine UpdateMachineAsync(string machineName, MachineStatus status);

    Task DeleteMachineAsync(string machineName, bool force);
}