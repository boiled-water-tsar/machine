using machines.Exceptions;
using machines.Jobs;
using Microsoft.EntityFrameworkCore;

namespace machines;

public class JobRepository : IJobRepository
{
    private readonly IMachineRepository _machineRepository;

    public JobRepository(IMachineRepository machineRepository)
    {
        _machineRepository = machineRepository;
    }

    public Job Create(int duration, string machineName)
    {
        var machine = _machineRepository.GetMachine(machineName);
        var job = new Job(duration, machineName);

        if (machine.Jobs.Any())
        {
            var jobTimes = new List<DateTime>();
            foreach (var existingJob in machine.Jobs)
            {
                if (existingJob.End < job.Start)
                {
                    jobTimes.Add(existingJob.End);
                }
            }

            jobTimes.Sort();
            throw new NoSpaceInQueueException(
                $"No space in machine '{machineName}' queue before after {jobTimes.Last()}");
        }

        machine.Jobs.Add(job);
        _machineRepository.UpdateMachine(machine);
        return job;
    }

    public Job GetJob(Guid jobId, string machineName)
    {
        var machine = _machineRepository.GetMachine(machineName);
        var job = machine.Jobs.FirstOrDefault(job => job.JobId == jobId);

        if (job is null)
        {
            throw new NotFoundException($"Job {jobId} not found in machine {machineName}");
        }

        return job;
    }

    public IQueryable<Job> GetJobs()
    {
        return _machineRepository.GetMachines().AsNoTracking().SelectMany(m => m.Jobs).AsQueryable();
    }

    public Job UpdateJob(Guid jobId, string machineName, JobStatus status)
    {
        var machine = _machineRepository.GetMachine(machineName);
        var job = machine.Jobs.FirstOrDefault(job => job.JobId == jobId);

        if (job is null)
        {
            throw new NotFoundException($"Job {jobId} not found in machine {machineName}");
        }

        job.SetStatus(status);
        _machineRepository.UpdateMachine(machine);
        return job;
    }

    public Task DeleteJob(Guid jobId, string machineName)
    {
        var machine = _machineRepository.GetMachine(machineName);
        var job = machine.Jobs.FirstOrDefault(job => job.JobId == jobId);

        if (job is null)
        {
            throw new NotFoundException($"Job {jobId} not found in machine {machineName}");
        }
        
        machine.RemoveJob(job);
        return Task.CompletedTask;
    }

    public IQueryable<Job> GetMachineJobs(string machineName)
    {
        return _machineRepository.GetMachine(machineName).Jobs.AsQueryable();
    }
}