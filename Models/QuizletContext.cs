using Microsoft.EntityFrameworkCore;

namespace M133.Models;

public class QuizletContext : DbContext
{
    public QuizletContext()
    {
        
    }
    
    public QuizletContext(DbContextOptions<QuizletContext> options)
        : base(options)
    {
    }
    
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<StudySet> StudySets { get; set; } = null!;
    public DbSet<Card> Cards { get; set; } = null!;
    public DbSet<Pool> Pools { get; set; } = null!;
    public DbSet<LearnCard> LearnCards { get; set; } = null!;
    public DbSet<Learn> Learns { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-VPK5DHD;Database=Quizlet;User Id=tester;Password=123;");
        }
    }
}