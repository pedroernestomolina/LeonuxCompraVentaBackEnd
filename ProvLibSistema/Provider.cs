using LibEntitySistema;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.EntityClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProvLibSistema
{
    
    public partial class Provider : ILibSistema.IProvider 
    {

        static EntityConnectionStringBuilder _cnSist ;
        private string _Instancia;
        private string _BaseDatos;
        private string _Usuario;
        private string _Password;


        public Provider(string instancia, string bd, string usuario="root")
        {
            _Usuario = usuario;
            _Password = "123";
            _Instancia = instancia;
            _BaseDatos = bd;
            setConexion();
        }


        private void setConexion()
        {
            _cnSist = new EntityConnectionStringBuilder();
            _cnSist.Metadata = "res://*/ModelLibSistema.csdl|res://*/ModelLibSistema.ssdl|res://*/ModelLibSistema.msl";
            _cnSist.Provider = "MySql.Data.MySqlClient";
            _cnSist.ProviderConnectionString = "data source=" + _Instancia + ";initial catalog=" + _BaseDatos + ";user id=" + _Usuario + ";Password=" + _Password + ";Convert Zero Datetime=True;";
        }

        public DtoLib.ResultadoEntidad<DateTime> FechaServidor()
        {
            var result = new DtoLib.ResultadoEntidad<DateTime>();

            try
            {
                using (var ctx = new sistemaEntities (_cnSist.ConnectionString))
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

        public DtoLib.ResultadoEntidad<DtoLibSistema.Empresa.Data.Ficha> Empresa_Datos()
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibSistema.Empresa.Data.Ficha>();

            try
            {
                using (var ctx = new sistemaEntities(_cnSist.ConnectionString))
                {
                    var ent = ctx.empresa.FirstOrDefault();
                    if (ent == null)
                    {
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Mensaje = "REGISTRO ENTIDAD [ EMPRESA ] NO DEFINIDO";
                        return result;
                    }

                    var nr = new DtoLibSistema .Empresa.Data.Ficha()
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