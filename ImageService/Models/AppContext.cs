using System.Data.Entity;

namespace ImageService.Models
{
    public class AppContext: DbContext
    {
        public AppContext():base("Data Source = (localdb)\\MSSQLLocalDB ; Initial Catalog = Images.React ; Integrated Security=true;")
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<AppContext>());   
        }

        public DbSet<Images> Images { get; set; }

    }
}
