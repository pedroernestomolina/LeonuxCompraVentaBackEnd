using System;
using System.Collections.Generic;
using System.Data.EntityClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProvLibVenta
{

    public partial class Provider : ILibVentas.IProvider
    {

        static EntityConnectionStringBuilder _cnVenta;
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
            _cnVenta = new EntityConnectionStringBuilder();

            _cnVenta.Metadata = "res://*/ModelLibVentas.csdl|res://*/ModelLibVentas.ssdl|res://*/ModelLibVentas.msl";
            _cnVenta.Provider = "MySql.Data.MySqlClient";
            _cnVenta.ProviderConnectionString = "data source=" + _Instancia + ";initial catalog=" + _BaseDatos + ";user id=" + _Usuario + ";Password=" + _Password + ";Convert Zero Datetime=True;";
        }


        public DtoLib.ResultadoEntidad<DateTime> FechaServidor()
        {
            var result = new DtoLib.ResultadoEntidad<DateTime>();

            try
            {
                using (var ctx = new LibEntityVentas.libVentasEntities(_cnVenta.ConnectionString))
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