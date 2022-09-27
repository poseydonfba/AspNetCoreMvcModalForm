using AspNetCoreMvcModalForm.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AspNetCoreMvcModalForm.Data.Mappings
{
    public class SolicitacaoMapping : IEntityTypeConfiguration<Solicitacao>
    {
        public void Configure(EntityTypeBuilder<Solicitacao> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(c => c.Descricao)
                .IsRequired();

            builder.Property(c => c.Data)
                .IsRequired();

            builder.Property(c => c.Quantidade)
                .IsRequired();

            builder.Property(c => c.Valor)
                .IsRequired();

            builder.ToTable(nameof(Solicitacao));

            builder.HasOne(x => x.TipoSolicitacao);
        }
    }
}
