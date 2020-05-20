using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProvSqLitePosOffLine
{
    
    public partial class Provider : IPosOffLine.IProvider
    {

        public DtoLib.ResultadoEntidad<DtoLibPosOffLine.Fiscal.Ficha> Fiscal_Tasas()
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibPosOffLine.Fiscal.Ficha>();

            try
            {
                result.Entidad = new DtoLibPosOffLine.Fiscal.Ficha();
                using (var cnn = new LibEntitySqLitePosOffLine.LeonuxPosOffLineEntities(_cnn.ConnectionString))
                {
                    var q = cnn.Fiscal.Where(w => w.auto == "0000000001").FirstOrDefault();
                    if (q != null)
                    {
                        result.Entidad.Tasa1 = q.tasa;
                    }
                    q = cnn.Fiscal.Where(w => w.auto == "0000000002").FirstOrDefault();
                    if (q != null)
                    {
                        result.Entidad.Tasa2 = q.tasa;
                    }
                    q = cnn.Fiscal.Where(w => w.auto == "0000000003").FirstOrDefault();
                    if (q != null)
                    {
                        result.Entidad.Tasa3 = q.tasa;
                    }
                }
                return result;
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