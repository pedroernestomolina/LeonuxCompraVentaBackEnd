using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProvLibVenta
{

    public partial class Provider: ILibVentas.IProvider 
    {

        public DtoLib.ResultadoEntidad<DtoLibVenta.Inventario.Precio.Resumen> Precio(string autoProducto, string idPrecio)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibVenta.Inventario.Precio.Resumen>();

            try
            {
                using (var cnn = new LibEntityVentas.libVentasEntities(_cnVenta.ConnectionString))
                {
                    var q = cnn.productos.Find(autoProducto);
                    if (q == null)
                    {
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Mensaje = "[ ID ] PRODUCTO NO ENCONTRADO";
                        return result;
                    }

                    DtoLibVenta.Inventario.Precio.Resumen p=null;
                    switch (idPrecio.Trim().ToUpper()) 
                    {
                        case "1":
                            p = new DtoLibVenta.Inventario.Precio.Resumen()
                            {
                                Id = "1",
                                PrecioNeto = q.precio_1,
                                ContEmpVenta = q.contenido_1,
                                DescEmpVenta = q.productos_medida3.nombre,
                                UtilidadMargen = q.utilidad_1,
                                Decimales = q.productos_medida3.decimales,
                            };
                            break;
                        case "2":
                            p = new DtoLibVenta.Inventario.Precio.Resumen()
                            {
                                Id = "2",
                                PrecioNeto = q.precio_2,
                                ContEmpVenta = q.contenido_2,
                                DescEmpVenta = q.productos_medida4.nombre,
                                UtilidadMargen = q.utilidad_2,
                                Decimales = q.productos_medida4.decimales,
                            };
                            break;
                        case "3":
                            p = new DtoLibVenta.Inventario.Precio.Resumen()
                            {
                                Id = "3",
                                PrecioNeto = q.precio_3,
                                ContEmpVenta = q.contenido_3,
                                DescEmpVenta = q.productos_medida5.nombre,
                                UtilidadMargen = q.utilidad_3,
                                Decimales = q.productos_medida5.decimales,
                            };
                            break;
                        case "4":
                            p = new DtoLibVenta.Inventario.Precio.Resumen()
                            {
                                Id = "4",
                                PrecioNeto = q.precio_4,
                                ContEmpVenta = q.contenido_4,
                                DescEmpVenta = q.productos_medida.nombre,
                                UtilidadMargen = q.utilidad_4,
                                Decimales = q.productos_medida.decimales,
                            };
                            break;
                        default:
                            result.Mensaje = "[ ID ] PRECIO NO ENCONTRADO/DEFINIDO";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                    }

                    result.Entidad = p;
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