using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Raizen.Cliente.Application.Contracts
{
    public class ClienteModel
    {
        public int Id { get; set; }

        [DisplayName("Nome")]
        [Required]
        public string Nome { get; set; }

        [DisplayName("Data Nascimento")]
        [Required]
        public string DataNascimento { get; set; }

        
        [DisplayName("Email")]
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [DisplayName("Logradouro")]
        [Required]
        public string Logradouro { get; set; }

        [DisplayName("Complemento")]
        public string Complemento { get; set; }

        [DisplayName("Bairro")]
        [Required]
        public string Bairro { get; set; }

        [DisplayName("Cidade")]
        [Required]
        public string Cidade { get; set; }

        [DisplayName("UF")]
        [Required]
        public string UF { get; set; }

        [DisplayName("CEP")]
        [Required]
        public string CEP { get; set;}


    }
}
