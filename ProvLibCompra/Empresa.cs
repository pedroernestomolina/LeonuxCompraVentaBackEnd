using LibEntityCompra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProvLibCompra
{
    
    public partial class Provider: ILibCompras.IProvider
    {

        public DtoLib.ResultadoEntidad<DtoLibCompra.Empresa.Data.Ficha> Empresa_Datos()
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibCompra.Empresa.Data.Ficha>();

            try
            {
                using (var ctx = new compraEntities(_cnCompra.ConnectionString))
                {
                    var sql = "SELECT nombre, rif as ciRif, direccion as direccionFiscal, telefono " +
                        "FROM empresa where auto='0000000001'";
                    var ent = ctx.Database.SqlQuery<DtoLibCompra.Empresa.Data.Ficha>(sql).FirstOrDefault();
                    if (ent == null)
                    {
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Mensaje = "REGISTRO ENTIDAD [ EMPRESA ] NO DEFINIDO";
                        return result;
                    }
                    result.Entidad = ent;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }

        public DtoLib.ResultadoEntidad<DtoLibCompra.Empresa.Fiscal.Ficha> Empresa_GetTasas()
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibCompra.Empresa.Fiscal.Ficha>();

            try
            {
                var nr = new DtoLibCompra.Empresa.Fiscal.Ficha();
                using (var ctx = new compraEntities(_cnCompra.ConnectionString))
                {
                    var q = ctx.empresa_tasas.Find("0000000001");
                    if (q != null)
                    {
                        nr.Tasa1 = q.tasa;
                    }
                    q = ctx.empresa_tasas.Find("0000000002");
                    if (q != null)
                    {
                        nr.Tasa2 = q.tasa;
                    }
                    q = ctx.empresa_tasas.Find("0000000003");
                    if (q != null)
                    {
                        nr.Tasa3 = q.tasa;
                    }
                }
                result.Entidad = nr;
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