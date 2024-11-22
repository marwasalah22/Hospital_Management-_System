namespace Hospital_Management__System.Data
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Nurse> Nurses { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<MedicalRecord> MedicalRecords { get; set; }
        public DbSet<Admin> Admins { get; set; }
        //public DbSet<Notification> Notifications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<IdentityRole>().HasData(
            //    new IdentityRole()
            //    {
            //        Id = Guid.NewGuid().ToString(),
            //        Name = "Doctor",
            //        NormalizedName = "doctor",
            //        ConcurrencyStamp = Guid.NewGuid().ToString()
            //    }, new IdentityRole()
            //    {
            //        Id = Guid.NewGuid().ToString(),
            //        Id = Guid.NewGuid().ToString(),
            //        Name = "Nurse",
            //        NormalizedName = "nurse",
            //        ConcurrencyStamp = Guid.NewGuid().ToString()
            //    }, new IdentityRole()
            //    {
            //        Id = Guid.NewGuid().ToString(),
            //        Name = "Patient",
            //        NormalizedName = "patient",
            //        ConcurrencyStamp = Guid.NewGuid().ToString()
            //    }
            //);
        }
    }
}
