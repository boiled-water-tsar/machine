using machines.Exceptions;
using machines.Jobs;

namespace machines;

public class Machine
{
    public string Name { get; set; }
    public Guid MachineId { get; set; }
    public MachineStatus Status { get; set; }

    public HashSet<Job> Jobs { get; set; }

    public Machine(string name)
    {
        Name = name;
        MachineId = Guid.NewGuid();
        Status = MachineStatus.Inactive;
        Jobs = new HashSet<Job>();
    }

    public void AddJob(Job job)
    {
        if (Status == MachineStatus.Error)
        {
            throw new MachineErrorException($"{Name} is in state {Status} and cannot receive new jobs");
        }

        Jobs.Add(job);
    }

    public void RemoveJob(Job job)
    {
        if (!Jobs.Remove(job))
        {
            throw new NotFoundException($"Job '{job.JobId}' not found in machine '{Name}'");
        }
    }

    public void SetStatus(MachineStatus status)
    {
        Status = status;
    }
}