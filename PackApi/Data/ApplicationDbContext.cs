using Microsoft.EntityFrameworkCore;
using PackApi.Models;

namespace PackApi.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :base(options)
        {

        }

        public DbSet<NationalPack> NationPacks { get; set; }
    }
}
