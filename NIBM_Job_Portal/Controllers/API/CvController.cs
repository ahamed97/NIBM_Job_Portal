﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NIBM_Job_Portal.Models.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class CvController : ControllerBase
    {

        private readonly ApplicationDbContext _applicationDbContext;

        public CvController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpPost]
        [Route("save")]
        public async Task<IActionResult> Save(CVDocsRequest request)
        {
            try
            {
                CVDocs model = new CVDocs();
                model.added_date = request.added_date;
                model.alias = request.alias;
                model.document_url = request.document_url;
                model.file_name = request.file_name;
                model.StudentId = request.id;

                _applicationDbContext.CVDocs.Add(model);
                await _applicationDbContext.SaveChangesAsync();


                return Ok(request);
            }
            catch
            {
                return NotFound();
            }

        }

        [HttpGet]
        [Route("getallcv")]
        public async Task<IActionResult> GetAllCv(int id)
        {
            try
            {
                List<Dictionary<int, string>> cvs = new List<Dictionary<int, string>>();
                var students = await _applicationDbContext.CVDocs.Where(x => x.StudentId == id).ToListAsync();
                return Ok(students);
            }
            catch
            {
                return NotFound();
            }

        }

        [HttpGet]
        [Route("deletecv")]
        public async Task<IActionResult> DeleteCv(int id)
        {
            try
            {
                var res=await _applicationDbContext.CVDocs.Where(x => x.Id == id).FirstOrDefaultAsync();
                _applicationDbContext.CVDocs.Remove(res);
                await _applicationDbContext.SaveChangesAsync();
                Responses response = new Responses();
                response.Message = "Success";
                response.StatusCode = "200";
                return Ok(response);
            }
            catch
            {
                return NotFound();
            }

        }

    }
}
