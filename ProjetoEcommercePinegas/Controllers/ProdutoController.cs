using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjetoEcommercePinegas.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoEcommercePinegas.Controllers
{
    public class ProdutoController : Controller
    {
        public IActionResult Index()
        {
            return View(Produto.ListarProdutos());
        }

        public IActionResult Cadastrar()
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
                    return View();
                }
            }
            return RedirectToAction("Cadastrar");
        }

        [HttpPost]
        public IActionResult Cadastrar(string nomeProduto, string descricao, string imgURL, string preco, string quantidade)
        {
            Produto p = new Produto(null, nomeProduto, descricao, imgURL, preco, quantidade);
            p.CadastrarProduto();
            return RedirectToAction("Cadastrar");
        }

        public IActionResult Editar(string id)
        {

            if (HttpContext.Session.GetString("Cliente") != null)
            {
                Usuario u = JsonConvert.DeserializeObject<Usuario>(HttpContext.Session.GetString("Cliente").ToString());
                Produto p = new Produto(id, null, null, null, null, null);
                if (u.TipoUsuario == "Cliente")
                {
                    return RedirectToAction("Index", "Produto");
                }
                else
                {
                    return View(p.SelecionarProduto());

                }
            }

            return RedirectToAction("Index", "Produto");
        }

        [HttpPost]
        public IActionResult Editar(string id, string nomeProduto, string descricao, string imgURL, string preco, string quantidade)
        {
            Produto p = new Produto(id, nomeProduto, descricao, imgURL, preco, quantidade);
            p.Editar();
            return RedirectToAction("Editar");
        }


        public IActionResult Deletar(string id)
        {
            Produto p = new Produto(id, null, null, null, null, null);
            p.Deletar();
            return RedirectToAction("Index", "Produto");
        }
        public IActionResult Pedido(string id)
        {

            if (HttpContext.Session.GetString("Cliente") != null)
            {
                Usuario u = JsonConvert.DeserializeObject<Usuario>(HttpContext.Session.GetString("Cliente").ToString());            
                Produto p = new Produto(id,null,null,null,null,null);       
                if (u.TipoUsuario == "Cliente")
                {
                    return View(p.SelecionarProduto());
                }
                else
                {

                    return RedirectToAction("Index", "Produto");
                }
            }

            return RedirectToAction("Index", "Produto");
        }

        [HttpPost]
        public IActionResult Pedido(string id, string nomeProduto, int quantidade, float preco, string emailUsuario)
        {
            Usuario u = JsonConvert.DeserializeObject<Usuario>(HttpContext.Session.GetString("Cliente").ToString());
            emailUsuario = u.Email;
            int qnt = quantidade;
            float prc = preco;
            preco = prc * qnt;                    
            Pedido p = new Pedido(id, quantidade.ToString(), nomeProduto, preco.ToString(), emailUsuario);
            p.PedidoCompleto();
            return RedirectToAction("Pedido");
        }
    }
}
