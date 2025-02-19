﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NIBM_Job_Portal.Helpers;
using NIBM_Job_Portal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NIBM_Job_Portal.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Authorize]

    public class CompanyController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        public CompanyController(ApplicationDbContext applicationDbContext, UserManager<ApplicationUser> userManager)
        {
            _applicationDbContext = applicationDbContext;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var data = TempData["company"];
            if (data != null)
            {
                ViewData["company"] = "true";
            }

            var result =await _applicationDbContext.Company.OrderByDescending(x => x.Id).ToListAsync();
            List<AdminCompanyViewModel> model = new List<AdminCompanyViewModel>();
            foreach (var item in result)
            {
                AdminCompanyViewModel model1 = new AdminCompanyViewModel();
                model1.Id = item.Id;
                model1.Company_Name = item.Company_Name;
                model1.Email = item.Email;
                model1.IndustryId = item.IndustryId;
                model1.Status = item.IsEnable;

                model.Add(model1);
            }

            return View(model);
        }
        [HttpGet]
        [Route("Create")]
        public async Task<IActionResult> Create()
        {
            string pwd = RandomPassword();
           AdminCompanyViewModel model = new AdminCompanyViewModel();
            model.Industry =await _applicationDbContext.Industry.ToListAsync();
            string RandomPasssword = RandomPassword();
            model.DefaultPasssword = RandomPasssword;
            return View(model);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(AdminCompanyViewModel model)
        {
            model.Industry =await _applicationDbContext.Industry.ToListAsync();
            if (ModelState.IsValid)
            {
              

                var user = new ApplicationUser { UserName = model.Email, Email = model.Email,TemporyPassword=model.DefaultPasssword };
                var result = await _userManager.CreateAsync(user, model.DefaultPasssword);
                var res=await _userManager.FindByEmailAsync(model.Email);
                if (result.Succeeded)
                {
                    
                    Company company = new Company();
                    company.Company_Name = model.Company_Name;
                    company.IndustryId = (int)model.IndustryId;
                    company.IsEnable = (int)CompanyStatus.Enable;
                    company.Email = model.Email;
                    company.ApplicationUserId = user.Id;
                    _applicationDbContext.Company.Add(company);
                    _applicationDbContext.SaveChanges();
                    TempData["company"] = "success";
                    return RedirectToAction("Index");
                }
                else
                {
                    if (res != null)
                    {
                        ModelState.AddModelError("", "User already exist!");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Unable to create user!");
                    }
                   
                    return View("Create", model);
                }

            }
            else
            {
                if (model.IndustryId == 0)
                {
                    ModelState.ClearValidationState("IndustryId");
                    ModelState.AddModelError("IndustryId", "Select Industry");
                }
               
                return View("Create", model);

            }


        }

        [HttpPost]
        [Route("ChangeStatus")]
        public async Task<bool> ChangeStatus(int status,int Id)
        {
            try
            {
                Company company = await _applicationDbContext.Company.FindAsync(Id);
                company.IsEnable =status;
                _applicationDbContext.Company.Update(company);
                _applicationDbContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
            
           
        }


        public string RandomPassword()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(RandomString(4, true));
            builder.Append(RandomNumber(1, 10));
            builder.Append("@");
            builder.Append(RandomString(2, false));
            return builder.ToString();
        }

        public string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }

        public int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }
    }
}
