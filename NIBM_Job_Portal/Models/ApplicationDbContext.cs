﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NIBM_Job_Portal.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Job> Job { get; set; }
        public DbSet<JobCategory> JobCategory { get; set; }
        public DbSet<Industry> Industry { get; set; }
        public DbSet<Company> Company { get; set; }

        public DbSet<Company> UploadCompanyLogo { get; set; }
        public DbSet<StudentJobPost> StudentJobPost { get; set; }
        public DbSet<Student> Student { get; set; }
        public DbSet<StudentDetails> StudentDetails { get; set; }
        public DbSet<AppliedJob> AppliedJob { get; set; }
        public DbSet<CVDocs> CVDocs { get; set; }
        public DbSet<Skills> Skills { get; set; }
        public DbSet<Category> Category { get; set; }

    }
}
