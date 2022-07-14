namespace machines.Jobs;

public interface IJobRepository
{
    Job Create(int duration, string machineName);
    
    Job GetJob(Guid jobId, string machineName);

    IQueryable<Job> GetJobs();

    Job UpdateJob(Guid jobId, string machineName, JobStatus status);

    Task DeleteJob(Guid jobId, string machineName);
}