/*
Descrição: Configuração e funções para uso do banco de dados
Data: 01/01/2021 - v.1.0
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
            string server = "DIOGO\\SQLEXPRESS";
            string database = "sistema";
            string user = "sa";
            string password = "d121079c";
            string catalog = "sistema";

            // String de conexão
            string connectionString = "Data Source=" + server + "; Initial Catalog=" + catalog + "; User ID=" + user + "; Password=" + password  + ";  Max Pool Size=10000; Database=" + database;

            // Conexão
            conn = new SqlConnection(connectionString);

            conn.Open();
        }

        // Fecha a conexão
        public void Close()
        {
            conn.Close();
        }

        // Executa a query
        public Query CreateQuery(string sql)
        {
            return new Query(sql, conn);
        }

    }

    // Query
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

        // Executa query sem retorno
        public void ExecuteUpdate()
        {
            comando.ExecuteNonQuery();
        }

        // Executa query com retorno de dataReader
        public IDataReader ExecuteQuery()
        {
            return comando.ExecuteReader();
        }

        // Executa a query com retorno de inteiro
        public int ExecuteScalar()
        {
            return Convert.ToInt32(comando.ExecuteScalar());
        }
    }

}