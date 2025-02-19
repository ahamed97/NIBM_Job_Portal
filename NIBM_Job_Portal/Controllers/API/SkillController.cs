﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NIBM_Job_Portal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NIBM_Job_Portal.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillController : ControllerBase
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public SkillController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }



        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await _applicationDbContext.Skills.ToListAsync());
            }
            catch
            {
                return NotFound();
            }

        }
    }
}
