using EcommereAPI.DomainModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace EcommereAPI.Data
{
    public class ProjectEcommerceContext : IdentityDbContext<EcommerceUser>
    {
        public ProjectEcommerceContext(DbContextOptions<ProjectEcommerceContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<UserDM> UserDM { get; set; }
        public DbSet<ProductDM> ProductDM { get; set; }
        public DbSet<OrderDM> OrderDM { get; set; }
        public DbSet<ProductCategoryDM> ProductCategories { get; set; }
        public DbSet<TrackingDetailsDM> TrackingDM { get; set; }
        public DbSet<UserProductOrderDM> UserProductOrderDM { get; set; }
        public DbSet<AddressDM> AddressDM { get; set; }
        public DbSet<BuyerDM> BuyerDM { get; set; }
        public DbSet<IndividualSellerDM> IndividualSellers { get; set; }
        public DbSet<CompanySellerDM> CompaniesSellers { get; set; }
        public DbSet<SellerDM> SellerDM { get; set; }
        public DbSet<TrackerDM> Trackers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=EcommerceDB;Trusted_Connection=True;MultipleActiveResultSets=true");
            //optionsBuilder.UseSqlServer("Server=192.168.29.71;Database=HeroAPIDB;User Id=sa;Password=123@Reno;MultipleActiveResultSets=true;Encrypt=False;");
            base.OnConfiguring(optionsBuilder);
            //For deployment we use below method for database creation
            /*optionsBuilder.UseSqlServer("Server=192.168.29.71;Database=HeroDB;User Id=sa;Password=123@Reno;MultipleActiveResultSets=true;Encrypt=False;");
            base.OnConfiguring(optionsBuilder);*/
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderDM>()
                .HasOne(o => o.TrackingDetails)
                .WithOne(td => td.Order)
                .HasForeignKey<TrackingDetailsDM>(td => td.OrderId);

            // Other configurations...

            base.OnModelCreating(modelBuilder);
        }
    }
}
