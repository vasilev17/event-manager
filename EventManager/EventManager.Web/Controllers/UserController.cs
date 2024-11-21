﻿using AutoMapper;
using EventManager.Services.Factories.Interfaces;
using EventManager.Services.Models.User;
using EventManager.Services.Services.Interfaces;
using EventManager.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventManager.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserServiceFactory userServiceFactory, IMapper mapper)
        {
            _userService = userServiceFactory.CreateUserService();
            _mapper = mapper;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterWebModel registerWebModel)
        {
            RegisterUserServiceModel user = _mapper.Map<RegisterUserServiceModel>(registerWebModel);

            return new CreatedResult("Register", await _userService.RegisterUser(user));
        }
    }
}
