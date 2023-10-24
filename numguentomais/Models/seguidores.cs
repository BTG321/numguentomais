using beyondthegame.Controllers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace beyondthegame.Models
{
    public class seguidores
    {
        [Key]
        public int id { get; set; }
        [ForeignKey("usuario")]
        public int usuario_id { get; set; }

        public usuario Usuario { get; set; }

        [ForeignKey("empresa")]
        public int empresa_id { get; set; }

        public empresa empresa { get; set; }

        public int estado { get; set; }

    }
}
