using System.Transactions;
using machines.DataBase;
using machines.Jobs;
using Microsoft.EntityFrameworkCore;

namespace machines;

public class JobRepository: IJobRepository
{
    private readonly MachineDbContext _machineDbContext;
    private readonly IMachineRepository _machineRepository;

    public JobRepository(MachineDbContext machineDbContext, IMachineRepository machineRepository)
    {
        _machineDbContext = machineDbContext;
        _machineRepository = machineRepository;
    }
    
    public Job Create(string jobName, int duration, string machineName)
    {
        var machine = _machineRepository.GetMachine(machineName);
        var job = new Job(duration, machineName);

        if (machine.Jobs.Any())
        {
            JobTimeLoop(job, machine);
        }

        machine.Jobs.Add(job);
        _machineRepository.UpdateMachine(machine);
        return job;
    }

    public Job GetJob(Guid jobId)
    {
        throw new NotImplementedException();
    }

    public IQueryable<Job> GetJobs()
    {
        throw new NotImplementedException();
    }

    public Job UpdateJob(Guid jobId, JobStatus status)
    {
        throw new NotImplementedException();
    }

    public Task DeleteJob(Guid jobId)
    {
        throw new NotImplementedException();
    }

    private void JobTimeLoop(Job job, Machine machine)
    {
        foreach (var existingJob in machine.Jobs)
        {
            if (existingJob.End > job.Start)
            {
                var nextDuration = existingJob.End - job.Start + TimeSpan.FromSeconds(job.DurationSeconds);
                job.UpdateJobDuration(nextDuration.Seconds);
                JobTimeLoop(job, machine);
            }
        }
    }
}