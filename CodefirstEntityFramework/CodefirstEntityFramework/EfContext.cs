using System.Data.Entity;

namespace CodefirstEntityFramework
{
    public class EfContext:DbContext
    {
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
    }
}