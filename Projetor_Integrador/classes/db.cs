using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projetor_Integrador.classes
{
    internal class db
    {
        private static string stringConexao { get; set; }
        static SqlConnection con;
        public static void AbreBanco()
        {
            stringConexao = @"Server=DESKTOP-8MFDP0Q; Database=BlueBee; Trusted_Connection=True";
            con = new SqlConnection();
            con.ConnectionString = stringConexao;
            con.Open();
        }

        public static void FechaBanco()
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
        public static DataTable RetornaDataTable(string sql)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;

            SqlDataReader dr = cmd.ExecuteReader();

            dt.Load(dr);

            dr.Close();
            return dt;
        }

        public static Int32 ExecutaComando(string sql, bool inclusao)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();

            if (inclusao)
            {
                cmd.CommandText = "SELECT @@IDENTITY";
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
            return 0;
        }
    }
}
