namespace machines.Jobs;

public class Job
{
    public DateTime start { get; set; }
    public int durationSeconds { get; set; }
    public Guid jobId { get; set; }

    public Job(int durationSeconds)
    {
        this.durationSeconds = durationSeconds;
        this.jobId = new Guid();
        this.start = DateTime.Now;
    }
}