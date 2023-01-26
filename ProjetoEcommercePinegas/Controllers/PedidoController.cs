using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjetoEcommercePinegas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoEcommercePinegas.Controllers
{
    public class PedidoController : Controller
    {
        public IActionResult PedidoAdm()
        {
            if (HttpContext.Session.GetString("Cliente") != null)
            {
                Usuario u = JsonConvert.DeserializeObject<Usuario>(HttpContext.Session.GetString("Cliente").ToString());
                if (u.TipoUsuario == "Cliente")
                {
                    return RedirectToAction("Index", "Produto");
                }
                else
                {
                    return View(Pedido.ListarPedido());
                }
            }
            return RedirectToAction("Index", "Produto");
        }

        public IActionResult PedidoCliente(string emailUsuario)
        {
            if (HttpContext.Session.GetString("Cliente") != null)
            {
                Usuario u = JsonConvert.DeserializeObject<Usuario>(HttpContext.Session.GetString("Cliente").ToString());
                emailUsuario = u.Email;
                Pedido p = new Pedido(null, null, null, null, emailUsuario);
                if (u.TipoUsuario == "Cliente")
                {
                    return View(p.SelecionarPedido());
                }
                else
                {
                    return RedirectToAction("Index", "Produto");
                }
            }
            return RedirectToAction("Index", "Produto");
        }

    }
}
