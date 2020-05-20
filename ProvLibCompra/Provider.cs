using System;
using System.Collections.Generic;
using System.Data.EntityClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProvLibCompra
{

    public partial class Provider: ILibCompras.IProvider
    {
        
        static EntityConnectionStringBuilder _cnCompra;
        private string _Instancia;
        private string _BaseDatos;
        private string _Usuario;
        private string _Password;


        public Provider() 
        {
            _Usuario = "root";
            _Password = "123";
            _Instancia = "localhost";
            _BaseDatos = "bogagalpon";
            _cnCompra = new EntityConnectionStringBuilder();

            _cnCompra.Metadata = "res://*/ModelLibCompras.csdl|res://*/ModelLibCompras.ssdl|res://*/ModelLibCompras.msl";
            _cnCompra.Provider = "MySql.Data.MySqlClient";
            _cnCompra.ProviderConnectionString = "data source=" + _Instancia + ";initial catalog=" + _BaseDatos + ";user id=" + _Usuario + ";Password=" + _Password + ";Convert Zero Datetime=True;";
        }

    }

}