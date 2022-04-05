using System.ComponentModel.DataAnnotations;

namespace M133.Models;

public class Pool
{
    public Pool(string name)
    {
        Name = name;
    }

    public Pool()
    {
        
    }
    
    public int PoolId { get; set; }
    public string Name { get; set; } = null!;

    public List<LearnCard>? LearnCards { get; set; }
}