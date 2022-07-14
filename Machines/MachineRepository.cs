namespace machines;

public class MachineRepository: IMachineRepository
{
    private readonly Dictionary<string, Machine> Machines = new ();
    private readonly SemaphoreSlim _semaphoreSlim = new (1, 1);

    public async Task AddMachineAsync(Machine machine)
    {
        await _semaphoreSlim.WaitAsync();
        Machines.Add(machine.name, machine);
        _semaphoreSlim.Release();
    }

    public async Task<Machine> GetMachineAsync(string machineName)
    {
        await _semaphoreSlim.WaitAsync();
        var machine = Machines[machineName];
        _semaphoreSlim.Release();
        return machine;
    }
}
