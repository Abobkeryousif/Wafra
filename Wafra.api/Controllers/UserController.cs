﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using Wafra.Application.Feature.Commands.User;
using Wafra.Application.Feature.DTOs.User;
using Wafra.Application.Feature.Quires.User;

namespace Wafra.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ISender _sender;

        public UserController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("Register")]

        public async Task<IActionResult> CreateAsync(UserDto userDto) 
        {
            return Ok(await _sender.Send(new RegisterUserCommand(userDto)));
        }

        [HttpGet]

        public async Task<IActionResult> GetAllUser() 
        {
            return Ok(await _sender.Send(new GetUserQuery()));
        }

        [HttpPost("CompleteRegister")]

        public async Task<IActionResult> CompelteRegisterAsync([FromBody] UserOTP userOTP) 
        {
            return Ok(await _sender.Send(new CompleteRegisterCommand(userOTP)));
        }
        [HttpPost("Login")]

        public async Task<IActionResult> LoginAsync(LoginUserDto loginUserDto) 
        {
            return Ok(await _sender.Send(new LoginCommand(loginUserDto)));
        }

    }
}










