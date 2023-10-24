using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public class AuthService
{
    private readonly string chaveSecreta; // Sua chave secreta aqui

    public AuthService(string chaveSecreta)
    {
        this.chaveSecreta = chaveSecreta;
    }

    public string GerarTokenJWT(string nomeUsuario)
    {
        var chave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(chaveSecreta));
        var credenciais = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.UniqueName, nomeUsuario),
            // Adicione outras reivindicações (claims) conforme necessário
        };

        var token = new JwtSecurityToken(
            issuer: "seu-issuing-party",
            audience: "seu-audience",
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: credenciais
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
