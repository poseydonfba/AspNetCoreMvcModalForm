using AspNetCoreMvcModalForm.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AspNetCoreMvcModalForm.Data.Mappings
{
    public class TipoSolicitacaoMapping : IEntityTypeConfiguration<TipoSolicitacao>
    {
        public void Configure(EntityTypeBuilder<TipoSolicitacao> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(c => c.Descricao)
                .IsRequired();

            builder.ToTable(nameof(TipoSolicitacao));
        }
    }
}
