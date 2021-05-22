using Microsoft.EntityFrameworkCore;
using TestsBackend.Entities;

namespace TestsBackend.Models
{
    public class TestsContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<SubjectType> SubjectTypes { get; set; }
        public DbSet<QuestionType> QuestionTypes { get; set; }
        public DbSet<Answer> Answers { get; set; }

        public TestsContext(DbContextOptions<TestsContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
