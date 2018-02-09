using System.Linq;
using AutenticacaoEFCookie.Models;

namespace AutenticacaoEFCookie.Dados
{
    public class CodeFirstBanco
    {
        public static void Inicializar(AutenticacaoContext contexto) {
            contexto.Database.EnsureCreated(); //Cria o banco se ele não existir

            if(contexto.Usuarios.Any()) return;

            var usuario = new Usuario(){
                Nome="Amanda",
                Email="amanda@gmail.com",
                Senha="123456"
            };

            contexto.Usuarios.Add(usuario);
            
            var permissao = new Permissao(){
                Nome="Financeiro"
            };

            contexto.Permissoes.Add(permissao);

            var usuariosPermissoes = new UsuarioPermissao(){
                IdUsuario = usuario.IdUsuario,
                IdPermissao = permissao.IdPermissao
            };
            
            contexto.UsuariosPermissoes.Add(usuariosPermissoes);
            
            contexto.SaveChanges();
        }
    }
}