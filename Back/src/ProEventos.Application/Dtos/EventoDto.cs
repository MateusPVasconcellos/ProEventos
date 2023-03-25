using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProEventos.Application.Dtos
{
    public class EventoDto
    {
        public int Id { get; set; }
        public string Local { get; set; }
        public string DataEvento { get; set; }
        [
            Required(ErrorMessage = "O campo {0} é obrigatório"),
            StringLength(50, MinimumLength = 3, ErrorMessage = "o campo {0} deve ter entre 3 e 50 caracteres")
        ]
        public string Tema { get; set; }
        [
            Display(Name = "Qtd Pessoas"),
            Range(1, 1200, ErrorMessage = "{0} deve estar entre 1 e 1200")
        ]
        public int QtdPessoas { get; set; }
        [RegularExpression(@".*\.(gif|jpe?g|bmp|png)$", ErrorMessage = "Imagem inválida.")]
        public string ImagemUrl { get; set; }
        [
            Required(ErrorMessage = "O campo {0} é obrigatório"),
            Phone(ErrorMessage = "Número inválido")
        ]
        public string Telefone { get; set; }
        [
            Display(Name = "E-mail"),
            EmailAddress(ErrorMessage = "o campo {0} deve ser um e-mail válido")
        ]
        public string Email { get; set; }
        public IEnumerable<LoteDto> lote { get; set; }
        public IEnumerable<RedeSocialDto> redessociais { get; set; }
        public IEnumerable<PalestranteDto> palestranteseventos { get; set; }
    }
}