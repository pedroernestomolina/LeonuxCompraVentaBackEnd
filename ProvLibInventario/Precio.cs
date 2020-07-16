using LibEntityInventario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProvLibInventario
{
    
    public partial class Provider : ILibInventario.IProvider
    {

        public DtoLib.ResultadoLista<DtoLibInventario.Precio.Historico.Resumen> Producto_Precio_Historico_Lista(DtoLibInventario.Precio.Historico.Filtro filtro)
        {
            var result = new DtoLib.ResultadoLista<DtoLibInventario.Precio.Historico.Resumen>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var q = cnn.productos_precios.Where(f => f.auto_producto == filtro.autoProducto).ToList();

                    var list = new List<DtoLibInventario.Precio.Historico.Resumen>();
                    if (q != null)
                    {
                        if (q.Count() > 0)
                        {
                            list = q.Select(s =>
                            {
                                var r = new DtoLibInventario.Precio.Historico.Resumen()
                                {
                                    estacion = s.estacion,
                                    fecha = s.fecha,
                                    hora = s.hora,
                                    nota = s.nota,
                                    usuario = s.usuario,
                                    precio=s.precio,
                                    idPrecio=s.precio_id,
                                };
                                return r;
                            }).ToList();
                        }
                    }
                    result.Lista = list;
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