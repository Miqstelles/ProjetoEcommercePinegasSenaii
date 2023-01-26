using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoEcommercePinegas.Models
{
    public class Produto
    {
        //Variaveis
        private string id, nomeProduto, descricao, imgURL, preco, quantidade;

        //Construtor
        public Produto(string id, string nomeProduto, string descricao, string imgURL, string preco, string quantidade)
        {
            this.id = id;
            this.nomeProduto = nomeProduto;
            this.descricao = descricao;
            this.imgURL = imgURL;
            this.preco = preco;
            this.quantidade = quantidade;

        }

        //Cadastra produtos no banco de dados
        public string CadastrarProduto()
        {
            //Conexão com o banco de dados!
            string conexao = "****";
            
            MySqlConnection con = new MySqlConnection(conexao);
            try
            {
                con.Open();
                MySqlCommand qry = new MySqlCommand("INSERT INTO Produto VALUES(Id,@NomeProduto,@Descricao,@ImgURL,@Preco,@Quantidade)", con);

                qry.Parameters.AddWithValue("@NomeProduto", nomeProduto);
                qry.Parameters.AddWithValue("@Descricao", descricao);
                qry.Parameters.AddWithValue("@ImgURL", imgURL);
                qry.Parameters.AddWithValue("@Preco", preco);
                qry.Parameters.AddWithValue("@Quantidade", quantidade);
                qry.ExecuteNonQuery();
                con.Close();
                return "Cadastro Concluido";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        //Edita Produto
        public string Editar()
        {
            string conexao = "****";
           
            MySqlConnection con = new MySqlConnection(conexao);

            try
            {
                con.Open();
                MySqlCommand qry = new MySqlCommand("UPDATE Produto SET NomeProduto = @NomeProduto, Descricao = @Descricao, ImgURL = @ImgURL, Preco = @Preco, Quantidade = @QuantidadE WHERE Id = @Id", con);
                qry.Parameters.AddWithValue("@Id", id);
                qry.Parameters.AddWithValue("@NomeProduto", nomeProduto);
                qry.Parameters.AddWithValue("@Descricao", descricao);
                qry.Parameters.AddWithValue("@ImgURL", imgURL);
                qry.Parameters.AddWithValue("@Preco", preco);
                qry.Parameters.AddWithValue("@Quantidade", quantidade);
                qry.ExecuteNonQuery();
                con.Close();
                return "Editado com sucesso";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        //Deleta(deixa a quantidade em 0) do banco de dados
        public string Deletar()
        {
            string conexao = "****";
            
            MySqlConnection con = new MySqlConnection(conexao);
            try
            {
                con.Open();

                MySqlCommand qry = new MySqlCommand("UPDATE Produto SET Quantidade = '0' WHERE Id = @Id", con);
                qry.Parameters.AddWithValue("@Id", id);
                qry.ExecuteNonQuery();
                con.Close();
                return "Excluido com Exito";
            }
            catch (Exception e)
            {
                return e.Message;
            }

        }
        //Lista produtos cadastrados no banco de dados
        public Produto SelecionarProduto()
        {
            string conexao = "****";
           
            MySqlConnection con = new MySqlConnection(conexao);
            MySqlCommand command = new MySqlCommand();
            try
            {
                con.Open();
                command.Connection = con;
                command.CommandText = "SELECT * FROM Produto WHERE Id = @Id";
                command.Parameters.AddWithValue("@Id", id);
                MySqlDataReader ler = command.ExecuteReader();
                if (ler.Read())
                {
                    Produto p = new Produto(
                        ler["Id"].ToString(),
                        ler["NomeProduto"].ToString(),
                        ler["Descricao"].ToString(),
                        ler["ImgURL"].ToString(),
                        ler["Preco"].ToString(),
                        ler["Quantidade"].ToString());
                    return p;
                }
                con.Close();
                return null;
            }
            catch (Exception e)
            {
                return null;
            }


        }

        //Lista produtos cadastrados no banco de dados
        public static List<Produto> ListarProdutos()
        {
            string conexao = "****";
            
            MySqlConnection con = new MySqlConnection(conexao);
            MySqlCommand command = new MySqlCommand();
            List<Produto> lista = new List<Produto>();
            try
            {
                con.Open();
                command.Connection = con;
                command.CommandText = "SELECT * FROM Produto";
                MySqlDataReader ler = command.ExecuteReader();
                while (ler.Read())
                {
                    Produto p = new Produto(
                        ler["Id"].ToString(),
                        ler["NomeProduto"].ToString(),
                        ler["Descricao"].ToString(),
                        ler["ImgURL"].ToString(),
                        ler["Preco"].ToString(),
                        ler["Quantidade"].ToString());
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
        public string NomeProduto { get => nomeProduto; set => nomeProduto = value; }
        public string Descricao { get => descricao; set => descricao = value; }
        public string ImgURL { get => imgURL; set => imgURL = value; }
        public string Preco { get => preco; set => preco = value; }
        public string Quantidade { get => quantidade; set => quantidade = value; }
        public string Id { get => id; set => id = value; }
    }
}
