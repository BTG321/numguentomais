using beyondthegame.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;


namespace beyondthegame.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JogoController : ControllerBase
    {
        private readonly DatabaseContext _dbContext;

        public JogoController(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Rota para adicionar um jogo
        [HttpPost]
        public async Task<IActionResult> AdicionarJogo([FromBody] jogo jogo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Retorna um erro 400 com mensagens de validação
            }
            try
            {
                _dbContext.jogo.Add(jogo);
                await _dbContext.SaveChangesAsync();

                return Ok(jogo); // Retorna o jogo adicionado com sucesso
            }
            catch (Exception ex)
            {
                // Log do erro
                return StatusCode(500, "Erro interno do servidor"); // Retorna um erro 500
            }
        }
        
        [HttpGet]
        public ActionResult<List<jogo>> BuscarTodosOsJogos()
        {
            var jogo = _dbContext.jogo.ToList();
            return jogo;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoverJogo(int id)
        {
            var jogo = await _dbContext.jogo.FindAsync(id);

            if (jogo == null)
            {
                return NotFound(); // A empresa não foi encontrada
            }

            _dbContext.jogo.Remove(jogo);
            await _dbContext.SaveChangesAsync();

            return NoContent(); // A empresa foi removida com sucesso
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarJogo(int id, [FromBody] jogo jogo)
        {
            var jogoExistente = await _dbContext.jogo.FindAsync(id);

            if (jogoExistente == null)
            {
                return NotFound(); // A empresa não foi encontrada
            }

            // Atualize os campos da empresa existente com os novos valores
            jogoExistente.nome = jogo.nome;
            jogoExistente.classificacao = jogo.classificacao;
            jogoExistente.descricao = jogo.descricao;
            jogoExistente.link = jogo.link;
            

            _dbContext.Entry(jogoExistente).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
                return NoContent(); // Atualização bem-sucedida
            }
            catch (DbUpdateConcurrencyException)
            {
                // Trate exceções de concorrência aqui, se necessário
                return StatusCode(500); // Erro interno do servidor
            }
        }
    }
}
