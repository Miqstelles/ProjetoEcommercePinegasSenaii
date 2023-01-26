using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using ProjetoEcommercePinegas.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace ProjetoEcommercePinegas.Controllers
{
    public class UsuarioController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registrar(string nome, string email, string senha, string tipoUsuario)
        {

            Usuario u = new Usuario(nome, email, senha, tipoUsuario);
            u.Registrar();
            return RedirectToAction("Registrar");
        }

        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("Cliente") == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Produto");
            }

        }
        [HttpPost]
        public IActionResult Login(string email, string senha)
        {
            MySqlConnection con = new MySqlConnection("****");
            string tipoUsuario = "";
            string nome = "";

            con.Open();

            MySqlCommand qry = new MySqlCommand("SELECT TipoUsuario, Nome, Email from Usuario where Email = @Email and Senha = @Senha", con);
            qry.Parameters.AddWithValue("@Email", email);
            qry.Parameters.AddWithValue("@Senha", senha);
            MySqlDataReader ler = qry.ExecuteReader();
            while (ler.Read())
            {
                tipoUsuario = ler["TipoUsuario"].ToString();
                nome = ler["Nome"].ToString();
            }
            con.Close();
            Usuario u = new Usuario(nome, email, senha, tipoUsuario);


            if (u.LogarUsuario())
            {

                HttpContext.Session.SetString("Cliente", JsonConvert.SerializeObject(u));
                return RedirectToAction("Index", "Produto");
            }
            else
            {
                TempData["mgs"] = "Email ou Senha Incorretos!";
                return RedirectToAction("Login");

            }


        }
        public IActionResult Editar(string email)
        {
            if (HttpContext.Session.GetString("Cliente") != null)
            {
                Usuario u = JsonConvert.DeserializeObject<Usuario>(HttpContext.Session.GetString("Cliente").ToString());
                if (u.TipoUsuario == "Cliente")
                {
                    return View(u.SelecionarUsuario());
                }
                else
                {
                    return RedirectToAction("Index", "Produto");
                }
            }
            return RedirectToAction("Index", "Produto");
        }

        [HttpPost]
        public IActionResult Editar(string nome, string email, string senha)
        {
            Usuario u = JsonConvert.DeserializeObject<Usuario>(HttpContext.Session.GetString("Cliente").ToString());
            email = u.Email;
            Usuario uu = new Usuario(nome, email, senha, null);
            uu.EditarUsuario();
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Produto");
        }


        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();         
            return RedirectToAction("Index", "Produto");
        }

    }
}
