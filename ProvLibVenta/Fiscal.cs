using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProvLibVenta
{
    
    public partial class Provider: ILibVentas.IProvider 
    {

        public DtoLib.ResultadoEntidad<DtoLibVenta.TasasFiscales.Ficha> TasasFiscales()
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibVenta.TasasFiscales.Ficha>();

            try
            {
                result.Entidad = new DtoLibVenta.TasasFiscales.Ficha();
                using (var cnn = new LibEntityVentas.libVentasEntities(_cnVenta.ConnectionString))
                {
                    var q = cnn.empresa_tasas.Where(w=>w.auto=="0000000001").FirstOrDefault();
                    if (q != null)
                    {
                        result.Entidad.Tasa1 = q.tasa;
                    }
                    q = cnn.empresa_tasas.Where(w => w.auto == "0000000002").FirstOrDefault();
                    if (q != null)
                    {
                        result.Entidad.Tasa2 = q.tasa;
                    }
                    q = cnn.empresa_tasas.Where(w => w.auto == "0000000003").FirstOrDefault();
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