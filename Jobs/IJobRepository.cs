namespace machines.Jobs;

public interface IJobRepository
{
    Job Create(string jobName, int duration, string machineName);
    
    Job GetJob(Guid jobId);

    IQueryable<Job> GetJobs();

    Job UpdateJob(Guid jobId, JobStatus status);

    Task DeleteJob(Guid jobId);
}