using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoEcommercePinegas.Models
{
    public class Usuario
    {
        private string nome, email, senha, tipoUsuario;

        public Usuario(string nome, string email, string senha, string tipoUsuario)
        {
            this.nome = nome;
            this.email = email;
            this.senha = senha;
            this.tipoUsuario = tipoUsuario;
        }

        public string Registrar()
        {
            string conexao = "****";
            
            MySqlConnection con = new MySqlConnection(conexao);

            try
            {

                tipoUsuario = "Cliente";
                con.Open();
                MySqlCommand qry = new MySqlCommand("INSERT INTO Usuario VALUES(@Nome,@Email,@Senha,@TipoUsuario)", con);
                qry.Parameters.AddWithValue("@Nome", nome);
                qry.Parameters.AddWithValue("@Email", email);
                qry.Parameters.AddWithValue("@Senha", senha);
                qry.Parameters.AddWithValue("@TipoUsuario", tipoUsuario);
                qry.ExecuteNonQuery();
                con.Close();
                return "Usuario registrado com sucesso!";


            }
            catch (Exception e)
            {
                return e.Message;
            }

        }

        public bool LogarUsuario()
        {
            string conexao = "****";
           
            MySqlConnection con = new MySqlConnection(conexao);

            try
            {
                con.Open();

                MySqlCommand qry = new MySqlCommand("SELECT Email from Usuario where Email = @Email and Senha = @Senha", con);
                qry.Parameters.AddWithValue("@Email", email);
                qry.Parameters.AddWithValue("@Senha", senha);

                MySqlDataReader reader = qry.ExecuteReader();
                reader.Read();
                if (reader.HasRows)
                {

                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception e)
            {
                return false;
            }
            finally
            {
                con.Close();

            }

        }

        public string EditarUsuario()
        {
            string conexao = "****";
            
            MySqlConnection con = new MySqlConnection(conexao);

            try
            {
                con.Open();
                MySqlCommand qry = new MySqlCommand("UPDATE Usuario SET Nome = @Nome, Senha = @Senha WHERE Email = @Email", con);
                qry.Parameters.AddWithValue("@Nome", nome);
                qry.Parameters.AddWithValue("@Email", email);
                qry.Parameters.AddWithValue("@Senha", senha);
                qry.ExecuteNonQuery();
                con.Close();
                return "Editado com sucesso";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public Usuario SelecionarUsuario()
        {
            string conexao = "****";
            
            MySqlConnection con = new MySqlConnection(conexao);
            MySqlCommand command = new MySqlCommand();
            try
            {
                con.Open();
                command.Connection = con;
                command.CommandText = "SELECT * FROM Usuario WHERE Email = @Email";
                command.Parameters.AddWithValue("@Email", email);
                MySqlDataReader ler = command.ExecuteReader();
                if (ler.Read())
                {
                    Usuario u = new Usuario(
                        ler["Nome"].ToString(),
                        ler["Email"].ToString(),
                        ler["Senha"].ToString(),
                        ler["TipoUsuario"].ToString());
                    return u;
                }
                con.Close();
                return null;
            }
            catch (Exception e)
            {
                return null;
            }


        }

        public string Nome { get => nome; set => nome = value; }
        public string Email { get => email; set => email = value; }
        public string Senha { get => senha; set => senha = value; }
        public string TipoUsuario { get => tipoUsuario; set => tipoUsuario = value; }

    }
}
