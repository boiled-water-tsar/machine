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

    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateMachine(string machineName)
    {
        try
        {
            await _machineRepository.AddMachineAsync(new Machine(machineName));
            return StatusCode(201);
        }
        catch (Exception e)
        {
            return StatusCode(500);
        }
    }

    public async Task<IActionResult> GetMachine(string machineName)
    {
        throw new NotImplementedException();
    }

    public async Task<IActionResult> UpdateMachine(string machineName, string status)
    {
        throw new NotImplementedException();
    }

    public async Task<IActionResult> DeleteMachine(string machineName)
    {
        throw new NotImplementedException();
    }
}