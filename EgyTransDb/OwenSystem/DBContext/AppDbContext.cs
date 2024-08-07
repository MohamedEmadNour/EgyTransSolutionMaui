using EgyTransDb.OwenSystem.DBContext.Entites;


using Microsoft.EntityFrameworkCore;

using System.Reflection.Emit;

namespace EgyTransDb.OwenSystem.DBContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
           
        }

        public DbSet<CarData> CarDatas { get; set; }
        public DbSet<ClientData> ClientDatas { get; set; }
        public DbSet<ClientTypeVisit> ClientTypes { get; set; }
        public DbSet<DriverData> DriverDatas { get; set; }
        public DbSet<SupplierData> SupplierDatas { get; set; }
        public DbSet<TravelData> TravelDatas { get; set; }
        public DbSet<TravelInfo> TravelInfos { get; set; }
        public DbSet<TourGuideClass> TourGuides { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TravelData>()
                .HasMany(t => t.Cars)
                .WithMany()
                .UsingEntity(j => j.ToTable("TravelCar"));

            modelBuilder.Entity<TravelData>()
                .HasMany(t => t.Drivers)
                .WithMany()
                .UsingEntity(j => j.ToTable("TravelDriver"));

            modelBuilder.Entity<TravelData>()
                .HasMany(t => t.Suppliers)
                .WithMany()
                .UsingEntity(j => j.ToTable("TravelSupplier"));


            modelBuilder.Entity<TravelData>()
                .HasMany(t => t.TourGuides)
                .WithMany()
                .UsingEntity(j => j.ToTable("GuideID"));

            modelBuilder.Entity<TravelData>()
                .HasMany(t => t.TravelInfos)
                .WithOne()
                .HasForeignKey("TravelDataID");

            modelBuilder.Entity<TravelData>()
                .HasOne(t => t.Client)
                .WithMany()
                .HasForeignKey(t => t.ClientID);

            modelBuilder.Entity<TravelData>()
                .HasOne(t => t.Type)
                .WithMany()
                .HasForeignKey(t => t.TypeID);

            modelBuilder.Entity<CarData>()
                .HasKey(C => C.CarID);

            modelBuilder.Entity<ClientData>()
                .HasKey(C => C.ClientID);
            
            modelBuilder.Entity<ClientTypeVisit>()
                .HasKey(C => C.TypeID);

            modelBuilder.Entity<DriverData>()
                .HasKey(C => C.DriverDataID);

            modelBuilder.Entity<SupplierData>()
                .HasKey(C => C.SupplieID);

            modelBuilder.Entity<TravelData>()
                .HasKey(C => C.TravelID);

            modelBuilder.Entity<TravelInfo>()
                .HasKey(C => C.TravelInfoID);
            
            modelBuilder.Entity<TourGuideClass>()
                .HasKey(C => C.GuideID);


        }


    }
}
