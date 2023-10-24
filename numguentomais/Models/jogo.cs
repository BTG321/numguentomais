using beyondthegame.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class jogo
{
    [Key]
    public int id { get; set; }
    public string link { get; set; }
    public string descricao { get; set; }
    public string nome { get; set; }
    public string classificacao { get; set; }


    // Chave estrangeira para estabelecer o relacionamento
    [ForeignKey("empresa")]
    public int empresa_id { get; set; }

    // Propriedade de navegação para representar a empresa relacionada
    public empresa? Empresa { get; set; }
}
