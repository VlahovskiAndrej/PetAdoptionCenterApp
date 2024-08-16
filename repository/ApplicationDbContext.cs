using domain.Identity;
using domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PetAdoptionCenter.Domain.Models;
using System.Collections.Generic;

namespace repository
{
    public class ApplicationDbContext : IdentityDbContext<PetAdoptionCenterUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Product> Product { get; set; } = default!;
        public DbSet<ShoppingCart> ShoppingCart { get; set; } = default!;
        public DbSet<ProductInShoppingCart> ProductInShoppingCart { get; set; } = default!;
        public DbSet<Order> Order { get; set; } = default!;
        public DbSet<ProductInOrder> ProductInOrder { get; set; } = default!;
        public DbSet<Probaa> Probaa { get; set; } = default!;
        public DbSet<Pet> Pet { get; set; } = default!;



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=PetAdoptionCenter-7bb318c5-dd1c-4371-80d7-43ccb19c121b;Trusted_Connection=True;MultipleActiveResultSets=true", b => b.MigrationsAssembly("PetAdoptionCenter.Web"));
            }
        }

    }


}
