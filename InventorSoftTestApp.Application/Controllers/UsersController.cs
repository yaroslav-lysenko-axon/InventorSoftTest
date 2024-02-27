using InventorSoftTestApp.Application.Models.Commands.User;
using InventorSoftTestApp.Application.Models.Requests.User;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InventorSoftTestApp.Application.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserRequestModel requestModel)
    {
        var response = await mediator.Send(new CreateUserCommand
        {
            CreateUserRequestModel = requestModel,
        });

        return Ok(response);
    }
    
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var response = await mediator.Send(new GetUsersCommand());

        return Ok(response);
    }
}