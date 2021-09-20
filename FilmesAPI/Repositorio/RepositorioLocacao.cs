using FilmesAPI.Interface;
using FilmesAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace FilmesAPI.Repositorio
{
    public class RepositorioLocacao : IRepositorioLocacao
    {
        string connectionString = @"Data Source=CAIOSILVA-PC\SQLEXPRESS;Initial Catalog=Everis;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        SqlDataReader dataRead = null;

        public List<Locacao> GetLocacao()
        {
            string queryString = @"SELECT l.id, l.valor, l.dataretirada, l.datadevolucao, l.clienteid, l.filmeid, l.ativo, l.qtdlocado FROM tb_locacao AS l JOIN tb_cliente c ON l.clienteid = c.id JOIN tb_filme f ON l.filmeid = f.id";
            Locacao locacao;
            List<Locacao> ListaLocacao = new List<Locacao>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        locacao = new Locacao
                        {
                            Id = Convert.ToInt32(reader["id"].ToString()),
                            Valor = Convert.ToDecimal(reader["valor"].ToString()),
                            DataRetirada = Convert.ToDateTime(reader["dataretirada"].ToString()),
                            DataDevolucao = Convert.ToDateTime(reader["datadevolucao"].ToString()),
                            ClienteId = Convert.ToInt32(reader["clienteid"].ToString()),
                            FilmeId = Convert.ToInt32(reader["filmeid"].ToString()),
                            Ativo = Convert.ToBoolean(reader["ativo"].ToString()),
                            QtdLocado = Convert.ToInt32(reader["qtdlocado"].ToString())
                        };

                        ListaLocacao.Add(locacao);
                    }
                }
                catch (Exception)
                {
                    throw;
                }

                finally
                {
                    if (dataRead != null)
                    {
                        dataRead.Close();
                    }
                    connection.Close();
                }
            }
            return ListaLocacao;
        }

        public Locacao GetLocacaoById(int id)
        {
            string queryString = @"SELECT l.id, l.valor, l.dataretirada, l.datadevolucao, l.clienteid, l.filmeid, l.ativo, l.qtdlocado FROM tb_locacao AS l JOIN tb_cliente c ON l.clienteid = c.id JOIN tb_filme f ON l.filmeid = f.id WHERE l.id = @id";
            Locacao locacao = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    connection.Open();
                    command.Parameters.AddWithValue("@id", id);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        locacao = new Locacao
                        {
                            Id = int.Parse(reader["id"].ToString()),
                            Valor = Decimal.Parse(reader["valor"].ToString()),
                            DataRetirada = Convert.ToDateTime(reader["dataretirada"].ToString()),
                            DataDevolucao = Convert.ToDateTime(reader["datadevolucao"].ToString()),
                            ClienteId = int.Parse(reader["clienteid"].ToString()),
                            FilmeId = int.Parse(reader["filmeid"].ToString()),
                            Ativo = Convert.ToBoolean(reader["ativo"].ToString()),
                            QtdLocado = Convert.ToInt32(reader["qtdlocado"].ToString())
                        };
                    }
                }
                catch (Exception)
                {
                    throw;
                }

                return locacao;
            }
        }

        public void PostLocacao(Locacao locacao)
        {
            string queryString = @"INSERT INTO tb_locacao (valor, dataretirada, datadevolucao, clienteid, filmeid, ativo, qtdlocado) VALUES (@valor, @dataretirada, @datadevolucao, @clienteid, @filmeid, @ativo, @qtdlocado)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    connection.Open();
                    command.Parameters.AddWithValue("@valor", locacao.Valor);
                    command.Parameters.AddWithValue("@dataretirada", DateTime.Now.ToShortDateString());
                    command.Parameters.AddWithValue("@datadevolucao", locacao.DataDevolucao.ToShortDateString());
                    command.Parameters.AddWithValue("@clienteid", locacao.ClienteId);
                    command.Parameters.AddWithValue("@filmeid", locacao.FilmeId);
                    command.Parameters.AddWithValue("@ativo", locacao.Ativo);
                    command.Parameters.AddWithValue("@qtdlocado", locacao.QtdLocado);
                    command.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public void PutLocacao(Locacao locacao)
        {
            string queryString = @"UPDATE tb_locacao SET valor = @valor WHERE id = @id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    connection.Open();
                    command.Parameters.AddWithValue("@id", locacao.Id);
                    command.Parameters.AddWithValue("@valor", locacao.Valor);
                    command.ExecuteNonQuery();
                }

                catch (Exception)
                {
                    throw;
                }

                finally
                {
                    connection.Close();
                }
            }
        }

        public void DeleteLocacao(int id)
        {
            string queryString = @"UPDATE tb_locacao SET ativo = 0 WHERE id = @id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    connection.Open();
                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public bool GetId(int id)
        {
            string queryString = @"SELECT id FROM tb_locacao WHERE id = @id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                bool resultado = false;
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                command.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    resultado = true;
                }

                connection.Close();
                return resultado;
            }
        }

        public bool GetClienteAtivo(int id)
        {
            string queryString = @"SELECT c.id, c.ativo FROM tb_cliente AS c WHERE ativo = 1 AND c.id = @id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                bool clienteAtivo = false;
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                command.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    clienteAtivo = true;
                }

                connection.Close();
                return clienteAtivo;
            }
        }
    }
}
