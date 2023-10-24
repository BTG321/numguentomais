using beyondthegame.Controllers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace beyondthegame.Models
{
    public class usuario
    {
        [Key]
        public int id { get; set; }
        public string email { get; set; }
        public string senha { get; set; }
        public string nome { get; set; }
        //public string seguindo { get; set; }

    }

}

