using Microsoft.EntityFrameworkCore;

namespace SOAP_Services.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options):base(options)
        {
            
        }
        public DbSet<Book> Books { get; set; }
    }
}
