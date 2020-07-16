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

        public DtoLib.ResultadoLista<DtoLibInventario.Costo.Historico.Resumen> Producto_Costo_Historico_Lista(DtoLibInventario.Costo.Historico.Filtro filtro)
        {
            var result = new DtoLib.ResultadoLista<DtoLibInventario.Costo.Historico.Resumen>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var q = cnn.productos_costos.Where(f=>f.auto_producto==filtro.autoProducto && f.serie.Trim()!="").ToList();

                    var list = new List<DtoLibInventario.Costo.Historico.Resumen>();
                    if (q != null)
                    {
                        if (q.Count() > 0)
                        {
                            list = q.Select(s =>
                            {
                                var _modoCambio= DtoLibInventario.Costo.Enumerados.enumModoCambio.SinDefinir;
                                switch (s.serie.Trim().ToUpper()) 
                                {
                                    case "FAC":
                                        _modoCambio = DtoLibInventario.Costo.Enumerados.enumModoCambio.PorCompra;
                                        break;
                                    case "ORD":
                                        _modoCambio = DtoLibInventario.Costo.Enumerados.enumModoCambio.PorOrdenCompra;
                                        break;
                                    case "MAN":
                                        _modoCambio = DtoLibInventario.Costo.Enumerados.enumModoCambio.ManualPorInventario;
                                        break;
                                    case "LIS":
                                        _modoCambio = DtoLibInventario.Costo.Enumerados.enumModoCambio.PoListaPrecioProveedor;
                                        break;
                                }
                                var r = new DtoLibInventario.Costo.Historico.Resumen()
                                {
                                    costo = s.costo,
                                    costoDivisa = s.costo_divisa,
                                    documento = s.documento,
                                    estacion = s.estacion,
                                    fecha = s.fecha,
                                    hora = s.hora,
                                    modoCambio = _modoCambio,
                                    nota = s.nota,
                                    serie = s.serie,
                                    tasaDivisa = s.divisa,
                                    usuario = s.usuario,
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