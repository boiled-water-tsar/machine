using System.Transactions;
using machines.DataBase;
using machines.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace machines;

public class MachineRepository : IMachineRepository
{
    private readonly MachineDbContext _machineDbContext;

    public MachineRepository(MachineDbContext machineDbContext)
    {
        _machineDbContext = machineDbContext;
    }

    public Machine Create(string machineName)
    {
        var machine = new Machine(machineName);
        _machineDbContext.Machines.Add(machine);
        _machineDbContext.SaveChanges();

        return machine;
    }

    public Machine GetMachine(string machineName)
    {
        var machine = _machineDbContext.Machines.Include(machine => machine.Jobs)
            .FirstOrDefault(machine => machine.Name == machineName);

        if (machine is null)
        {
            throw new NotFoundException($"Machine '{machineName}' not found");
        }

        return machine;
    }
    
    public Machine GetMachine(Guid machineId)
    {
        var machine = _machineDbContext.Machines.Include(machine => machine.Jobs)
            .FirstOrDefault(machine => machine.MachineId == machineId);

        if (machine is null)
        {
            throw new NotFoundException($"Machine '{machineId}' not found");
        }

        return machine;
    }

    public IQueryable<Machine> GetMachines()
    {
        return _machineDbContext.Machines.Include(machine => machine.Jobs);
    }

    public Machine UpdateMachine(string machineName, MachineStatus status)
    {
        var machine = GetMachine(machineName);
        machine.SetStatus(status);
        _machineDbContext.Machines.Attach(machine);
        _machineDbContext.Machines.Update(machine);
        _machineDbContext.SaveChanges();
        return machine;
    }
    
    public Machine UpdateMachine(Machine machine)
    {
        _machineDbContext.Machines.Attach(machine);
        _machineDbContext.Machines.Update(machine);
        _machineDbContext.SaveChanges();
        return machine;
    }

    public async Task DeleteMachine(string machineName)
    {
        var machine = GetMachine(machineName);
        _machineDbContext.Machines.Attach(machine);
        _machineDbContext.Machines.Remove(machine);
        await _machineDbContext.SaveChangesAsync();
    }
}