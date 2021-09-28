using LibEntityCompra;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.EntityClient;
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


        public Provider(string instancia, string bd, string usu="root")
        {
            _Usuario = usu;
            _Password = "123";
            _Instancia = instancia;
            _BaseDatos = bd;
            setConexion();
        }


        private void setConexion()
        {
            _cnCompra = new EntityConnectionStringBuilder();
            _cnCompra.Metadata = "res://*/ModelLibCompra.csdl|res://*/ModelLibCompra.ssdl|res://*/ModelLibCompra.msl";
            _cnCompra.Provider = "MySql.Data.MySqlClient";
            _cnCompra.ProviderConnectionString = "data source=" + _Instancia + ";initial catalog=" + _BaseDatos + ";user id=" + _Usuario + ";Password=" + _Password + ";Convert Zero Datetime=True;";
        }


        public DtoLib.ResultadoEntidad<DateTime> FechaServidor()
        {
            var result = new DtoLib.ResultadoEntidad<DateTime>();

            try
            {
                using (var ctx = new compraEntities(_cnCompra.ConnectionString))
                {
                    var fechaSistema = ctx.Database.SqlQuery<DateTime>("select now()").FirstOrDefault();
                    result.Entidad = fechaSistema.Date;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }

    }

}