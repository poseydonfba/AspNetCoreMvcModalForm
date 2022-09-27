using AspNetCoreMvcModalForm.Business.Models;
using Microsoft.EntityFrameworkCore;

// dotnet ef migrations add InitialCreate --project AspNetCoreMvcModalForm.Data --startup-project AspNetCoreMvcModalForm
// dotnet ef database update --project AspNetCoreMvcModalForm.Data --startup-project AspNetCoreMvcModalForm

/*
 
 dotnet tool uninstall --global dotnet-aspnet-codegenerator
 dotnet tool install --global dotnet-aspnet-codegenerator --version 6.0.9
 
 */

namespace AspNetCoreMvcModalForm.Data.Context
{
    public class DataDbContext : DbContext
    {
        public DataDbContext(DbContextOptions<DataDbContext> options) : base(options) { }

        public DbSet<Solicitacao> Solicitacoes { get; set; }
        public DbSet<TipoSolicitacao> TipoSolicitacoes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetProperties()
                    .Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataDbContext).Assembly);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

            base.OnModelCreating(modelBuilder);
        }
    }
}
