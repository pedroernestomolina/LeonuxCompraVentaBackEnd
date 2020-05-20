using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConsoleMySql
{
    class Program
    {
        static void Main(string[] args)
        {

            var _usuario = "root";
            var _password = "123";
            var _instancia = "localhost";
            var _baseDatos = "bogagalpon";
            var cadena = "Database=" + _baseDatos + "; Data Source=" + _instancia + "; User Id=" + _usuario + "; Password=" + _password + "";

            MySqlConnection _cnn2 = new MySqlConnection(cadena);
            MySqlDataReader reader = null;

            try
            {
                MySqlCommand comando = new MySqlCommand("select count(*) from productos");
                comando.Connection = _cnn2;
                _cnn2.Open();
                var rt = comando.ExecuteScalar();
            }
            catch (MySqlException ex)
            {
            }
            finally 
            {
                _cnn2.Close();
            }

        }
    }
}
