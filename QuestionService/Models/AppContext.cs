using System.Data.Entity;

namespace LoginService.Models
{
    public class AppContext : DbContext
    {
        public AppContext():base("Data Source = (localdb)\\MSSQLLocalDB ; Initial Catalog = api1 ; Integrated Security=true;")
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<AppContext>());
        }

        public DbSet<UserData> LoginDatas { get; set; }

        public DbSet<UserInfo> UserInfos { get; set; }
    }
}
