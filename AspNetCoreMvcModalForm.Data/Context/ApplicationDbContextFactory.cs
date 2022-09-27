using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace AspNetCoreMvcModalForm.Data.Context
{
    // Classe para contornar o erro de scaffold na criação de controllers
    // Essa classe de fábrica cria ApplicationDbContext em tempo de design e o scaffolding é executado corretamente.
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<DataDbContext>
    {
        public DataDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DataDbContext>();
            optionsBuilder.UseNpgsql("Server=localhost;Port=5433;Database=AspNetCoreMvcModalFormDb;User Id=postgres;Password=apocalipse;");

            return new DataDbContext(optionsBuilder.Options);
        }
    }
}
