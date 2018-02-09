using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AutenticacaoEFCookie.Controllers
{
    public class FinanceiroController:Controller
    {
        [Authorize(Roles="Financeiro")] //Reforça a autenticacao
        public IActionResult Index(){
            return View();
        }
    }
}