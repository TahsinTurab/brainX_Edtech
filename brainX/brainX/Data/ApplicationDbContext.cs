using brainX.Infrastructure.Domains;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace brainX.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Content> Contents { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }
        public DbSet<CommunityQuestion> Questions { get; set; }
        public DbSet<CommunityAnswer> Answers { get; set; }
        public DbSet<Reaction> Reactions { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<Quiz> Quizes { get; set; }
        public DbSet<Solution> Solutions { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<Tutorial> Tutorials { get; set; }
        public DbSet<Note> Notes { get; set; }
    }
}
