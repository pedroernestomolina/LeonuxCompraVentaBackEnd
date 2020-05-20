using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProvLibVenta
{
    
    public partial class Provider: ILibVentas.IProvider 
    {

        public DtoLib.ResultadoLista<DtoLibVenta.MedioCobro.Resumen> MedioCobroLista()
        {
            var result = new DtoLib.ResultadoLista<DtoLibVenta.MedioCobro.Resumen>();

            try
            {
                using (var cnn = new LibEntityVentas.libVentasEntities(_cnVenta.ConnectionString))
                {
                    var list = new List<DtoLibVenta.MedioCobro.Resumen>();
                    var q = cnn.empresa_medios.Where(w=>w.estatus_cobro=="1").ToList();
                    if (q != null)
                    {
                        if (q.Count() > 0)
                        {
                            result.Lista = q.Select(s =>
                            {
                                var r = new DtoLibVenta.MedioCobro.Resumen()
                                {
                                    Auto = s.auto,
                                    Codigo = s.codigo,
                                    Descripcion= s.nombre,
                                };
                                return r;
                            }).ToList();
                        }
                        else
                        {
                            result.Lista = list;
                        }
                    }
                    else
                    {
                        result.Lista = list;
                    }
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