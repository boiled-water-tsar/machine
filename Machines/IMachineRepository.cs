namespace machines;

public interface IMachineRepository
{
    Machine Create(string machineName);
    
    Machine GetMachine(string machineName);
    
    Machine GetMachine(Guid machineId);

    IQueryable<Machine> GetMachines();

    Machine UpdateMachine(string machineName, MachineStatus status);
    
    Machine UpdateMachine(Machine machine);

    Task DeleteMachine(string machineName);
}