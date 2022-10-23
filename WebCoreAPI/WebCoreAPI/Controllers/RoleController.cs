﻿using Microsoft.AspNetCore.Mvc;
using WebCoreAPI.Services;

namespace WebCoreAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _roleService.GetAll());
        }

        [HttpPost("Assign/{userId}/{roleId}")]
        public async Task<IActionResult> Assign(int userId, int roleId)
        {
            return Ok(await _roleService.Assign(userId, roleId));
        }
    }
}