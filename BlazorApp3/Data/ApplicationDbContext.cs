namespace BlazorApp3.Data;

using Microsoft.EntityFrameworkCore; // Required for DbContext base class

public class ApplicationDbContext : DbContext
{
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {  }
    public DbSet<User> Users { get; set; }
    public DbSet<Topic> Topics { get; set; }
    public DbSet<Question> Questions { get; set; }
    
    public DbSet<TestResult> TestResults { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Question>().ToTable("Questions"); // The base table
        modelBuilder.Entity<MultipleChoiceQuestion>().ToTable("MultipleChoiceQuestions");
        modelBuilder.Entity<TrueFalseQuestion>().ToTable("TrueFalseQuestions");
        modelBuilder.Entity<TypeInQuestion>().ToTable("TypeInQuestions");
        
        modelBuilder.Entity<Question>()
            .Property(q => q.QuestionType)
            .HasConversion<string>();
        
        modelBuilder.Entity<Question>()
            .HasOne(q => q.Topic) // A Question has one Topic
            .WithMany(t => t.Questions) // A Topic has many Questions
            .HasForeignKey(q => q.TopicId) // The foreign key property in Question
            .OnDelete(DeleteBehavior.Cascade); // If a topic is deleted, delete its questions
    }


}