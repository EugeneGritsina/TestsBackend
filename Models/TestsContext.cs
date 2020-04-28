﻿using Microsoft.EntityFrameworkCore;

namespace WebApiAttempt1.Models
{
    public class TestsContext : DbContext
    {
        public DbSet<Test> Tests { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }

        public TestsContext(DbContextOptions<TestsContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}