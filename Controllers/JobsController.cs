using machines.Exceptions;
using machines.Jobs;
using Microsoft.AspNetCore.Mvc;

namespace machines.Controllers;

[ApiController]
[Route("[controller]")]
public class JobsController : ControllerBase
{
    private readonly IJobRepository _jobRepository;

    public JobsController(IJobRepository jobRepository)
    {
        _jobRepository = jobRepository;
    }
    
    [HttpPost]
    [ProducesResponseType(typeof(Job), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Route("")]
    public async Task<IActionResult> CreateJob(int duration, string machineName)
    {
        try
        {
            return this.Ok(_jobRepository.Create(duration, machineName));
        }
        catch (NoSpaceInQueueException e)
        {
            return this.Conflict(e.Message);
        }
        catch (AlreadyExistsException e)
        {
            return this.Conflict(e.Message);
        }
        catch (NotFoundException e)
        {
            return this.NotFound(e.Message);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet]
    [ProducesResponseType(typeof(Job), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Route("{jobId:guid}")]
    public async Task<IActionResult> GetJob(Guid jobId, string machineName)
    {
        try
        {
            return this.Ok(_jobRepository.GetJob(jobId, machineName));
        }
        catch (NotFoundException e)
        {
            return this.NotFound(e.Message);
        }
        catch (Exception e)
        {
            return this.StatusCode(500, e.Message);
        }
        
    }
    
    [HttpGet]
    [ProducesResponseType(typeof(IQueryable<Job>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetJobs()
    {
        try
        {
            return this.Ok(_jobRepository.GetJobs());
        }
        catch (Exception e)
        {
            return this.StatusCode(500, e.Message);
        }
        
    }

    [HttpPost]
    [ProducesResponseType(typeof(Job), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Route("Update")]
    public async Task<IActionResult> UpdateJob(Guid jobId, string machineName, string status)
    {
        try
        {
            return this.Ok(_jobRepository.UpdateJob(jobId, machineName, Enum.Parse<JobStatus>(status)));
        }
        catch (NotFoundException e)
        {
            return this.NotFound(e.Message);
        }
        catch (Exception e)
        {
            return this.StatusCode(500, e.Message);
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Route("delete/{jobId:guid}")]
    public async Task<IActionResult> DeleteJob(Guid jobId, string machineName)
    {
        try
        {
            await _jobRepository.DeleteJob(jobId, machineName);
            return this.Ok();
        }
        catch (NotFoundException e)
        {
            return this.NotFound(e.Message);
        }
        catch (Exception)
        {
            return this.StatusCode(500);
        }
    }
}