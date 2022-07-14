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

    public Machine CreateAsync(string machineName)
    {
        var machine = new Machine(machineName);
        var strategy = _machineDbContext.Database.CreateExecutionStrategy();
        strategy.ExecuteAsync(async () =>
        {
            using var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            _machineDbContext.Machines.Add(machine);
            await _machineDbContext.SaveChangesAsync();
            transaction.Complete();
        });

        return machine;
    }

    public Machine GetMachineAsync(string machineName)
    {
        var machine = _machineDbContext.Machines.Include(machine => machine.Jobs)
            .FirstOrDefault(machine => machine.Name == machineName);

        if (machine is null)
        {
            throw new NotFoundException($"Machine '{machineName}' not found");
        }

        return machine;
    }

    public IQueryable<Machine> GetMachinesAsync()
    {
        return _machineDbContext.Machines.Include(machine => machine.Jobs);
    }

    public Machine UpdateMachineAsync(string machineName, MachineStatus status)
    {
        var machine = GetMachineAsync(machineName);
        machine.SetStatus(status);
        var strategy = _machineDbContext.Database.CreateExecutionStrategy();
        strategy.ExecuteAsync(async () =>
        {
            using var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            _machineDbContext.Machines.Attach(machine);
            _machineDbContext.Machines.Update(machine);
            await _machineDbContext.SaveChangesAsync();
            transaction.Complete();
        });
        return machine;
    }

    public async Task DeleteMachineAsync(string machineName, bool force)
    {
        var machine = GetMachineAsync(machineName);
        var strategy = _machineDbContext.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            using var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            _machineDbContext.Machines.Attach(machine);
            _machineDbContext.Machines.Remove(machine);
            await _machineDbContext.SaveChangesAsync();
            transaction.Complete();
        });
    }
}