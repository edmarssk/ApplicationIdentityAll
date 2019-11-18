using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TesteIdentity.Extensions;

namespace TesteIdentity.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Privacy()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        public IActionResult AcessoPrivado()
        {
            return View();
        }

        [Authorize(Policy = "PodeExcluir")]
        public IActionResult UsoClams()
        {
            return View();
        }
        [CustomClaimsAuthorize("Produtos", "Excluir")]
        public IActionResult ProdutosExcluir()
        {
            return View();
        }

        //Login de acessso

        //edmar.santos1986@gmail.com senha ErrorViewModel

        // Dar uma olhada na Privacy.cshtml para ver as ClaimRazor funcionando
    }
}
