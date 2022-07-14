namespace machines.Jobs;

public class Job
{
    public DateTime Start { get; set; }
    
    public DateTime End { get; set; }
    public int DurationSeconds { get; set; }
    public Guid JobId { get; set; }
    
    public JobStatus Status { get; set; }
    
    public string MachineName { get; set; }

    public Job(int durationSeconds, string machineName)
    {
        DurationSeconds = durationSeconds;
        MachineName = machineName;
        JobId = Guid.NewGuid();
        Start = DateTime.UtcNow;
        End = Start.AddSeconds(DurationSeconds);
        Status = JobStatus.NotStarted;
    }

    public void UpdateJobDuration(int durationSeconds)
    {
        DurationSeconds = durationSeconds;
    }

    public void StartJob()
    {
        Status = JobStatus.Running;
    }
}