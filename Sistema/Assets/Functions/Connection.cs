/*
Descrição: Configuração e funções para uso do banco de dados
Data: 01/01/2020 - v.1.0
*/

using System;
using System.Data;
using System.Data.SqlClient;

namespace Functions
{
    // Conexão
    public class Connection
    {
        private IDbConnection conn;

        public Connection()
        {
            // Variaveis
            string server = "NOTEBOOK\\SQLEXPRESS";
            string database = "sistema";
            string user = "teste";
            string password = "teste";
            string catalog = "sistema";

            // String de conexão
            string connectionString = "Data Source=" + server + "; Initial Catalog=" + catalog + "; User ID=" + user + "; Password=" + password  + ";  Max Pool Size=10000; Database=" + database;

            // Conexão
            conn = new SqlConnection(connectionString);

            conn.Open();
        }

        public void Close()
        {
            conn.Close();
        }

        public Query CreateQuery(string sql)
        {
            return new Query(sql, conn);
        }

    }

    public class Query
    {
        private IDbCommand comando;
        public Query(string sql, IDbConnection connection)
        {
            comando = connection.CreateCommand();
            comando.CommandType = CommandType.Text;
            comando.CommandTimeout = 600;
            comando.CommandText = sql;
        }

        public void ExecuteUpdate()
        {
            comando.ExecuteNonQuery();
        }

        public IDataReader ExecuteQuery()
        {
            return comando.ExecuteReader();
        }

        public int ExecuteScalar()
        {
            return Convert.ToInt32(comando.ExecuteScalar());
        }
    }

}