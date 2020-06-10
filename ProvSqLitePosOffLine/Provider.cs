using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProvSqLitePosOffLine
{
    
    public partial class Provider : IPosOffLine.IProvider
    {

        static EntityConnectionStringBuilder _cnn;
        static MySqlConnectionStringBuilder _cnn2;
        static string _bdRemotoInstancia;
        static string _bdRemotaBaseDatos;
        static string _bdLocal;


        public Provider(string pathDB) 
        {
            _bdLocal = pathDB;
            _cnn = new EntityConnectionStringBuilder()
            {
                Metadata = @"res://*/ModelPos.csdl|res://*/ModelPos.ssdl|res://*/ModelPos.msl",
                Provider = @"System.Data.SQLite.EF6",
                ProviderConnectionString = new SqlConnectionStringBuilder()
                {
                    DataSource =pathDB,
                }.ConnectionString
            };

            //var _usuario = "root";
            //var _password = "123";
            //var _instancia = "localhost";
            //var _baseDatos = "bogagalpon";
            //var cadena="Database="+_baseDatos+"; Data Source="+_instancia+"; User Id=" +_usuario+"; Password="+_password+"";
            //_cnn2 = new MySqlConnectionStringBuilder();
            //_cnn2.Database = _baseDatos;
            //_cnn2.UserID = _usuario;
            //_cnn2.Password = _password;
            //_cnn2.Server = _instancia;
        }

        public void setServidorRemoto(string instancia, string baseDatos)
        {
            _bdRemotoInstancia = instancia;
            _bdRemotaBaseDatos = baseDatos;

            var _usuario = "root";
            var _password = "123";
            var _instancia = instancia ;
            var _baseDatos = baseDatos;
            var cadena = "Database=" + _baseDatos + "; Data Source=" + _instancia + "; User Id=" + _usuario + "; Password=" + _password + "";
            _cnn2 = new MySqlConnectionStringBuilder();
            _cnn2.Database = _baseDatos;
            _cnn2.UserID = _usuario;
            _cnn2.Password = _password;
            _cnn2.Server = _instancia;
        }


        public DtoLib.ResultadoEntidad<DateTime> FechaServidor()
        {
            var result = new DtoLib.ResultadoEntidad<DateTime>();

            try
            {
                using (var ctx = new LibEntitySqLitePosOffLine.LeonuxPosOffLineEntities(_cnn.ConnectionString))
                {
                    var fechaSistema = ctx.Database.SqlQuery<DateTime>("select date('now')").FirstOrDefault();
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


        public DtoLib.ResultadoEntidad<DtoLibPosOffLine.Sistema.InformacionBD.Ficha> InformacionBD()
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibPosOffLine.Sistema.InformacionBD.Ficha>();

            var inf = new DtoLibPosOffLine.Sistema.InformacionBD.Ficha()
            {
                 BD_Remota= _bdRemotoInstancia+"\\"+_bdRemotaBaseDatos,
                 BD_Local=_bdLocal,
            };
            result.Entidad = inf;

            return result;
        }

    }

}