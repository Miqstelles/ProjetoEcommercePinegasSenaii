using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoEcommercePinegas.Models
{
    public class Pedido
    {

        private string id, quantidade, nomeProduto, preco, emailUsuario;


        public Pedido(string id, string quantidade, string nomeProduto, string preco, string emailUsuario)
        {
            this.id = id;
            this.quantidade = quantidade;
            this.nomeProduto = nomeProduto;
            this.preco = preco;
            this.emailUsuario = emailUsuario;
        }

        //Cadastra produtos no banco de dados
        public string PedidoCompleto()
        {
            //Conexão com o banco de dados!
            string conexao = "****";
            
            MySqlConnection con = new MySqlConnection(conexao);
            try
            {
                con.Open();
                MySqlCommand qry = new MySqlCommand("INSERT INTO Pedido VALUES(IdPedido,@Quantidade, @Preco,@NomeProduto,@IdProduto, @EmailUsuario) ", con);

                qry.Parameters.AddWithValue("@Quantidade", quantidade);
                qry.Parameters.AddWithValue("@Preco", preco);
                qry.Parameters.AddWithValue("@NomeProduto", nomeProduto);
                qry.Parameters.AddWithValue("@EmailUsuario", emailUsuario);
                qry.Parameters.AddWithValue("@IdProduto", id);
                qry.ExecuteNonQuery();
                con.Close();
                return "Pedido Concluido";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public List<Pedido> SelecionarPedido() {
            string conexao = "****";
            
            MySqlConnection con = new MySqlConnection(conexao);
            MySqlCommand command = new MySqlCommand();
            List<Pedido> lista = new List<Pedido>();
            try {
                con.Open();
                command.Connection = con;
                command.CommandText = "SELECT * FROM Pedido WHERE EmailUsuario = @EmailUsuario";
                command.Parameters.AddWithValue("@EmailUsuario", emailUsuario);
                MySqlDataReader ler = command.ExecuteReader();
                while (ler.Read()) {
                    Pedido p = new Pedido(
                    ler["IdPedido"].ToString(),
                    ler["Quantidade"].ToString(),
                    ler["NomeProduto"].ToString(),
                    ler["Preco"].ToString(),
                    ler["EmailUsuario"].ToString());
                    lista.Add(p);
                }
                con.Close();
                return lista;
            } catch (Exception e) {
                return null;
            }
        }

        public static List<Pedido> ListarPedido()
        {
             string conexao = "****";
            
            MySqlConnection con = new MySqlConnection(conexao);
            MySqlCommand command = new MySqlCommand();
            List<Pedido> lista = new List<Pedido>();
            try
            {
                con.Open();
                command.Connection = con;
                command.CommandText = "SELECT * FROM Pedido";
                MySqlDataReader ler = command.ExecuteReader();
                while (ler.Read())
                {
                    Pedido p = new Pedido(
                    ler["IdPedido"].ToString(),
                    ler["Quantidade"].ToString(),
                    ler["NomeProduto"].ToString(),
                    ler["Preco"].ToString(),
                    ler["EmailUsuario"].ToString());
                    lista.Add(p);
                }
                con.Close();
                return lista;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public string Id { get => id; set => id = value; }
        public string Quantidade { get => quantidade; set => quantidade = value; }
        public string ProdutoId { get => id; set => id = value; }
        public string Preco { get => preco; set => preco = value; }
        public string NomeProduto { get => nomeProduto; set => nomeProduto = value; }
        public string EmailUsuario { get => emailUsuario; set => emailUsuario = value; }
    }
}
