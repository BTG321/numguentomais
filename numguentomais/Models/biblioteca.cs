using beyondthegame.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace beyondthegame.Controllers
{
    public class biblioteca
    {
        [Key]

        public int id { get; set; }

        [ForeignKey("usuario")]

        public int usuario_id { get; set; }




        [ForeignKey("jogo")]

        public int jogo_id { get; set; }


        public usuario usuario { get; set; }

        public jogo jogo { get; set; }
    }

}
