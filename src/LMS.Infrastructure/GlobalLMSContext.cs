using LMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class GlobalLMSContext : DbContext
{
    public DbSet<Course> Courses { get; set; }
    public DbSet<Enrollment> Enrollments { get; set; }
    public DbSet<Step> Steps { get; set; }
    public DbSet<StepProgress> StepProgresses { get; set; }
    public DbSet<Lecture> Lectures { get; set; }
    public DbSet<Quiz> Quizes { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<QuizResponse> QuizResponse { get; set; }
    public DbSet<QuizProgress> QuizProgress { get; set; }

    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    // {
    //     optionsBuilder.UseNpgsql("Host=localhost;Database=lms;Username=postgres;Password=root");
    // }
    
    public GlobalLMSContext(DbContextOptions<GlobalLMSContext> options) : base(options)
    {
    }
}