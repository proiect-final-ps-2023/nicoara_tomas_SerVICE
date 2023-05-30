namespace SerVICE.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            //optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=serviceDB;Trusted_Connection=true;TrustServerCertificate=true;");
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=SerVICE_DB;Trusted_Connection=true;TrustServerCertificate=true;");
        }


        public DbSet<Service> Services => Set<Service>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Order> Orders => Set<Order>();
    }
}
