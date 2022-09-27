using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AspNetCoreMvcModalForm.Business.Models
{
    public class Solicitacao : Entity
    {
        [DisplayName("Descrição")]
        [Required(ErrorMessage = "Campo obrigatório.")]
        public string Descricao { get; set; } = string.Empty;

        [DisplayName("Data")]
        [Required(ErrorMessage = "Campo obrigatório.")]
        public DateTime Data { get; set; }

        [DisplayName("Quantidade")]
        [Required(ErrorMessage = "Campo obrigatório.")]
        public int Quantidade { get; set; }

        [DisplayName("Valor")]
        [Required(ErrorMessage = "Campo obrigatório.")]
        public decimal Valor { get; set; }

        [DisplayName("Tipo")]
        [Required(ErrorMessage = "Campo obrigatório.")]
        public Guid TipoSolicitacaoId { get; set; }


        public virtual TipoSolicitacao TipoSolicitacao { get; set; }
    }
}
