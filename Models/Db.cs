using Microsoft.EntityFrameworkCore;

namespace M133.Models;

public class Db : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Lernset> Lernsets { get; set; }
    public DbSet<Card> Cards { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=DESKTOP-VPK5DHD;Database=Quizlet;User Id=tester;Password=123;");
    }
}