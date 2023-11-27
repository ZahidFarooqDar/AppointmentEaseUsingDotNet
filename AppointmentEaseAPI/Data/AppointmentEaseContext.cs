using AppointmentEaseAPI.DomainModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace AppointmentEaseAPI.Data
{
    public class AppointmentEaseContext : IdentityDbContext<AuthenticUser>
    {
        public AppointmentEaseContext(DbContextOptions<AppointmentEaseContext> options)
            : base(options)
    {
        Database.EnsureCreated();
    }
    public DbSet<PatientDM> Patients { get; set; }
    public DbSet<DoctorDM> Doctors { get; set; }
    public DbSet<UserDM> UserDM { get; set; }
    public DbSet<AppointmentDM> Appointments { get; set; }
    

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=AppointmentEase;Trusted_Connection=True;MultipleActiveResultSets=true");
        //optionsBuilder.UseSqlServer("Server=192.168.29.71;Database=HeroAPIDB;User Id=sa;Password=123@Reno;MultipleActiveResultSets=true;Encrypt=False;");
        base.OnConfiguring(optionsBuilder);
        //For deployment we use below method for database creation
        /*optionsBuilder.UseSqlServer("Server=192.168.29.71;Database=HeroDB;User Id=sa;Password=123@Reno;MultipleActiveResultSets=true;Encrypt=False;");
        base.OnConfiguring(optionsBuilder);*/
    }

    }
}
