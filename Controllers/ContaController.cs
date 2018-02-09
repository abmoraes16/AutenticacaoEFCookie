using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using AutenticacaoEFCookie.Dados;
using AutenticacaoEFCookie.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AutenticacaoEFCookie.Controllers
{
    public class ContaController:Controller
    {
        readonly AutenticacaoContext _contexto;
        
        public ContaController(AutenticacaoContext context) {
            this._contexto = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Usuario usuario)
        {
            Console.WriteLine("Teste");
            try{
                Usuario user = _contexto.Usuarios.Include("UsuariosPermissoes")
                                            .Include("UsuariosPermissoes.Permissao")
                                            .FirstOrDefault(c=>c.Email==usuario.Email && c.Senha==usuario.Senha);// && c.Senha==usuario.Senha).FirstOrDefault();
                            //Console.WriteLine("User="+user.Email.ToString());
                if(user!=null){
                    var claims = new List<Claim>();

                    claims.Add(new Claim(ClaimTypes.Email,user.Email));
                    claims.Add(new Claim(ClaimTypes.Name,user.Nome));
                    claims.Add(new Claim(ClaimTypes.Sid,user.IdUsuario.ToString()));

                    foreach(var item in user.UsuariosPermissoes){
                        claims.Add(new Claim(ClaimTypes.Role,item.Permissao.Nome));
                    }

                    //modo de autenticação
                    var claimsIdentity = new ClaimsIdentity(
                        claims, CookieAuthenticationDefaults.AuthenticationScheme
                    );

                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                                            new ClaimsPrincipal(claimsIdentity));

                    return RedirectToAction("Index","Financeiro");
                }

                return View(usuario);

            }
            catch(Exception ex){
                return BadRequest(ex.Message);
            }

        }

        [HttpGet]
        public IActionResult Sair()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login");
        }
    }
}