using LibEntityInventario;
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


        public Provider(string instancia, string bd)
        {
            _Usuario = "root";
            _Password = "123";
            _Instancia = instancia;
            _BaseDatos = bd;
            setConexion();
        }


        private void setConexion()
        {
            _cnInv = new EntityConnectionStringBuilder();
            _cnInv.Metadata = "res://*/ModelLibInventario.csdl|res://*/ModelLibInventario.ssdl|res://*/ModelLibInventario.msl";
            _cnInv.Provider = "MySql.Data.MySqlClient";
            _cnInv.ProviderConnectionString = "data source=" + _Instancia + ";initial catalog=" + _BaseDatos + ";user id=" + _Usuario + ";Password=" + _Password + ";Convert Zero Datetime=True;";
        }

        public DtoLib.ResultadoEntidad<DateTime> FechaServidor()
        {
            var result = new DtoLib.ResultadoEntidad<DateTime>();

            try
            {
                using (var ctx = new invEntities(_cnInv.ConnectionString))
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

        public DtoLib.ResultadoEntidad<DtoLibInventario.Empresa.Data.Ficha> Empresa_Datos()
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibInventario.Empresa.Data.Ficha>();

            try
            {
                using (var ctx = new invEntities(_cnInv.ConnectionString))
                {
                    var ent = ctx.empresa.FirstOrDefault();
                    if (ent == null) 
                    {
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Mensaje = "REGISTRO ENTIDAD [ EMPRESA ] NO DEFINIDO";
                        return result;
                    }

                    var nr = new DtoLibInventario.Empresa.Data.Ficha()
                    {
                        CiRif = ent.rif,
                        DireccionFiscal = ent.direccion,
                        Nombre = ent.nombre,
                        Telefono = ent.telefono,
                    };
                    result.Entidad = nr;
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