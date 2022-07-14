using machines.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace machines.Controllers;

[ApiController]
[Route("[controller]")]
public class MachinesController: ControllerBase
{
    private readonly IMachineRepository _machineRepository;

    public MachinesController(IMachineRepository machineRepository)
    {
        _machineRepository = machineRepository;
    }

    [HttpPost]
    [ProducesResponseType(typeof(Machine), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Route("{machineName}")]
    public async Task<IActionResult> CreateMachine(string machineName)
    {
        try
        {
            return this.Ok(_machineRepository.CreateAsync(machineName));
        }
        catch (MachineAlreadyExistsException e)
        {
            return this.Conflict(e.Message);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet]
    [ProducesResponseType(typeof(Machine), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Route("{machineName}")]
    public async Task<IActionResult> GetMachine(string machineName)
    {
        try
        {
            return this.Ok(_machineRepository.GetMachineAsync(machineName));
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
    [ProducesResponseType(typeof(IQueryable<Machine>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMachines()
    {
        try
        {
            return this.Ok(_machineRepository.GetMachinesAsync());
        }
        catch (Exception e)
        {
            return this.StatusCode(500, e.Message);
        }
        
    }

    [HttpPost]
    [ProducesResponseType(typeof(Machine), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Route("Update")]
    public async Task<IActionResult> UpdateMachine(string machineName, string status)
    {
        try
        {
            return this.Ok(_machineRepository.UpdateMachineAsync(machineName, Enum.Parse<MachineStatus>(status)));
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
    [Route("/delete/{machineName}")]
    public async Task<IActionResult> DeleteMachine(string machineName, bool force = false)
    {
        try
        {
            await _machineRepository.DeleteMachineAsync(machineName, force);
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