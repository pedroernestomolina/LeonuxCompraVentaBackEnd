using System;
using System.Collections.Generic;
using System.Data.Entity.Core.EntityClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProvLibInventario
{

    public partial class Provider : ILibInventario.IProvider
    {

        static EntityConnectionStringBuilder _cnInv ;
        private string _Instancia;
        private string _BaseDatos;
        private string _Usuario;
        private string _Password;


        public Provider()
        {
            _Usuario = "root";
            _Password = "123";
            _Instancia = "localhost";
            _BaseDatos = "enrique";
            _cnInv = new EntityConnectionStringBuilder();

            _cnInv.Metadata = "res://*/ModelLibInventario.csdl|res://*/ModelLibInventario.ssdl|res://*/ModelLibInventario.msl";
            _cnInv.Provider = "MySql.Data.MySqlClient";
            _cnInv.ProviderConnectionString = "data source=" + _Instancia + ";initial catalog=" + _BaseDatos + ";user id=" + _Usuario + ";Password=" + _Password + ";Convert Zero Datetime=True;";
        }

    }

}