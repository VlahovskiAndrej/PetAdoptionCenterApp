using domain.Identity;
using domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PetAdoptionCenter.Domain.Models;
using PetAdoptionCenter.Domain.OtherModels;
using System.Collections.Generic;

namespace repository
{
    public class ApplicationDbContext : IdentityDbContext<PetAdoptionCenterUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
      
        public DbSet<Pet> Pet { get; set; } = default!;
        public DbSet<AdoptionApplication> AdoptionApplication { get; set; } = default!;
        public DbSet<Agency> Agencies { get; set; } = default!;
        public DbSet<TravelPackage> TravelPackages { get; set; } = default!;



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                   "Server=SAKICASA\\SQLEXPRESS;Database=PAC;Trusted_Connection=True;TrustServerCertificate=True;",
                    sqlServerOptionsAction: sqlOptions =>
                    {
                        sqlOptions.MigrationsAssembly("PetAdoptionCenter.Web");

                        // Enabling retry on transient failures
                        sqlOptions.EnableRetryOnFailure(
                            maxRetryCount: 5, // Optional: Customize the number of retry attempts
                            maxRetryDelay: TimeSpan.FromSeconds(30), // Optional: Customize the delay between retries
                            errorNumbersToAdd: null // Optional: Specify additional SQL error numbers to retry on
                        );
                    });
            }

        }

    }


}
