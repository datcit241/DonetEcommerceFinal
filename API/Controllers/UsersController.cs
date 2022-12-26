using API.DTOs.User;
using Application.Users;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class UsersController : BaseApiController
{
    [HttpPut]
    public async Task<IActionResult> UpdateBio(BioDTO bioDto)
    {
        return HandleResult(await Mediator.Send(new EditBio.Command { Bio = bioDto.Bio }));
    }
}