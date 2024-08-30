using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetAdoptionCenter.Repository
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer("Server=tcp:pacsqlserver.database.windows.net,1433;Initial Catalog=PetAdoptionCenterAppDb;Persist Security Info=False;User ID=pacadmin;Password=Admin123-;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
