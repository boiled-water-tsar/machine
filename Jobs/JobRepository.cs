using machines.Exceptions;
using machines.Jobs;

namespace machines;

public class JobRepository: IJobRepository
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
            throw new NoSpaceInQueueException($"No space in machine '{machineName}' queue before after {jobTimes.Last()}");
        }

        machine.Jobs.Add(job);
        _machineRepository.UpdateMachine(machine);
        return job;
    }

    public Job GetJob(Guid jobId, string machineName)
    {
        throw new NotImplementedException();
    }

    public IQueryable<Job> GetJobs()
    {
        throw new NotImplementedException();
    }

    public Job UpdateJob(Guid jobId, string machineName, JobStatus status)
    {
        throw new NotImplementedException();
    }

    public Task DeleteJob(Guid jobId, string machineName)
    {
        throw new NotImplementedException();
    }
}