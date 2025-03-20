using System.ComponentModel.DataAnnotations;

namespace EquipeFacil.Models
{
    public class Colaborador
    {
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Sobrenome { get; set; }

        [Required]
        public DateTime Nascimento { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "Documento deve conter 11 dígitos.")]
        public string Documento { get; set; }

        public List<string> Telefone { get; set; } = new List<string>();

        public string Gestor { get; set; }

        public string Outros { get; set; }

        // Validação de maioridade
        public bool MaiorDeIdade()
        {
            return (DateTime.Now.Year - Nascimento.Year) >= 18;
        }
    }
}

