using InventorSoftTestApp.Application.Models.Commands.Task;
using InventorSoftTestApp.Application.Models.Requests.Task;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InventorSoftTestApp.Application.Controllers;

[ApiController]
[Route("[controller]")]
public class TasksController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTaskRequestModel requestModel)
    {
        var response = await mediator.Send(new CreateTaskCommand
        {
            CreateTaskRequestModel = requestModel,
        });

        return Ok(response);
    }
    
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var response = await mediator.Send(new GetTasksCommand());

        return Ok(response);
    }
}