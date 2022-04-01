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
    
    public DbSet<User> Users { get; set; }
    public DbSet<Lernset> Lernsets { get; set; }
    public DbSet<Card> Cards { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(@"Server=FLURIN-PC;Database=Quizlet;User Id=tester;Password=123;");
        }
    }
}