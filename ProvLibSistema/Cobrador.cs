using LibEntitySistema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProvLibSistema
{

    public partial class Provider : ILibSistema.IProvider
    {

        public DtoLib.ResultadoLista<DtoLibSistema.Cobrador.Lista.Ficha> Cobrador_GetLista(DtoLibSistema.Cobrador.Lista.Filtro filtro)
        {
            var rt = new DtoLib.ResultadoLista<DtoLibSistema.Cobrador.Lista.Ficha>();

            try
            {
                using (var cnn = new sistemaEntities(_cnSist.ConnectionString))
                {
                    var sql = @"select ec.auto as id, ec.codigo, ec.nombre 
                        FROM empresa_cobradores as ec 
                        where 1=1 ";

                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p2 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p3 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p4 = new MySql.Data.MySqlClient.MySqlParameter();

                    var list = cnn.Database.SqlQuery<DtoLibSistema.Cobrador.Lista.Ficha>(sql, p1, p2, p3, p4).ToList();
                    rt.Lista = list;
                }
            }
            catch (Exception e)
            {
                rt.Mensaje = e.Message;
                rt.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return rt;
        }

    }

}