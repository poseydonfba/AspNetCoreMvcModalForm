using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AspNetCoreMvcModalForm.Business.Models
{
    public class TipoSolicitacao : Entity
    {
        [DisplayName("Descrição")]
        [Required(ErrorMessage = "Campo obrigatório.")]
        public string Descricao { get; set; } = string.Empty;
    }
}
