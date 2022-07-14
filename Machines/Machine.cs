using System.Runtime.CompilerServices;
using machines.Jobs;

namespace machines;

public class Machine
{
    public string name { get; set; }
    public Guid machineId { get; set; }
    public MachineStatus status { get; set; }
    
    public List<Job> jobs { get; set; }

    public Machine(string name)
    {
        this.name = name;
        this.machineId = new Guid();
        this.status = MachineStatus.Inactive;
        this.jobs = new List<Job>();
    }
}