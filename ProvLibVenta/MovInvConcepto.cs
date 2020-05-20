using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProvLibVenta
{

    public partial class Provider: ILibVentas.IProvider 
    {

        public DtoLib.ResultadoEntidad<DtoLibVenta.MovInventario.Concepto.Ficha> MovInventario_Concepto(string auto)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibVenta.MovInventario.Concepto.Ficha>();

            try
            {
                using (var cnn = new LibEntityVentas.libVentasEntities(_cnVenta.ConnectionString))
                {
                    var q = cnn.productos_conceptos.Find(auto);
                    if (q == null)
                    {
                        result.Mensaje = "[ ID ] Concepto Movimento Inventario No Encontrado";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }
                    
                    var r = new DtoLibVenta.MovInventario.Concepto.Ficha ()
                    {
                        Auto = q.auto,
                        Codigo = q.codigo,
                        Descripcion = q.nombre,
                    };
                    result.Entidad = r;
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