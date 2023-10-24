using beyondthegame.Controllers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace beyondthegame.Models
{
    public class empresa
    {
        [Key]
        public int id { get; set; }
        public string nome { get; set; }
        public string? numero_de_contato { get; set; }
        public string? email { get; set; }
        // public string seguidores { get; set; }


        public string senha { get; set; }
        public string? cnpj { get; set; }
        public string? cpf { get; set; }


    }
}
