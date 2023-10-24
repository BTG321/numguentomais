using beyondthegame.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data.Entity;
using System.Linq;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EntityState = Microsoft.EntityFrameworkCore.EntityState;

namespace beyond.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpresaController : ControllerBase
    {
        private readonly DatabaseContext _dbContext;

        public EmpresaController(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }



        [HttpGet]
        public ActionResult<List<empresa>> BuscarTodasAsEmpresas()
        {
            var empresas = _dbContext.empresa.ToList();
            return empresas;
        }





        [HttpPost]

        public async Task<empresa> Adicionar(empresa empresa)
        {
            await _dbContext.empresa.AddAsync(empresa);
            await _dbContext.SaveChangesAsync();

            return empresa;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoverEmpresa(int id)
        {
            var empresa = await _dbContext.empresa.FindAsync(id);

            if (empresa == null)
            {
                return NotFound(); // A empresa não foi encontrada
            }

            _dbContext.empresa.Remove(empresa);
            await _dbContext.SaveChangesAsync();

            return NoContent(); // A empresa foi removida com sucesso
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarEmpresa(int id, [FromBody] empresa empresa)
        {
            var empresaExistente = await _dbContext.empresa.FindAsync(id);

            if (empresaExistente == null)
            {
                return NotFound(); // A empresa não foi encontrada
            }

            // Atualize os campos da empresa existente com os novos valores
            empresaExistente.nome = empresa.nome;
            empresaExistente.numero_de_contato = empresa.numero_de_contato;
            empresaExistente.email = empresa.email;
            empresaExistente.senha = empresa.senha;
            empresaExistente.cnpj = empresa.cnpj;
            empresaExistente.cpf = empresa.cpf;

            _dbContext.Entry(empresaExistente).State = EntityState.Modified;

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



        public class AuthController : ControllerBase
        {
            private readonly IConfiguration _configuration;

            public AuthController(IConfiguration configuration)
            {
                _configuration = configuration;
            }

            public IActionResult Login()
            {
                // Simule a autenticação. Você pode verificar as credenciais do usuário aqui.
                // Por exemplo, verificar um banco de dados ou outro local de armazenamento.
                string username = "usuario";
                string password = "senha";

                if (username == "usuario" && password == "senha")
                {
                    // O usuário está autenticado com sucesso, agora vamos gerar um token JWT.

                    // Chave secreta para assinar o token
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]));

                    var token = new JwtSecurityToken(
                        issuer: _configuration["JwtSettings:Issuer"],
                        audience: _configuration["JwtSettings:Audience"],
                        expires: DateTime.UtcNow.AddHours(1),
                        claims: new[]
                        {
                        new Claim(ClaimTypes.Name, username),
                            // Você pode adicionar outras reivindicações (claims) conforme necessário.
                        },
                        signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                    );

                    var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

                    return Ok(new { Token = tokenString });
                }

                return Ok(new { message = "test" });
            }
        }


    }
    }


