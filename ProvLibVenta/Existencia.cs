using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProvLibVenta
{
    
    public partial class Provider: ILibVentas.IProvider 
    {

        public DtoLib.ResultadoEntidad<DtoLibVenta.Inventario.Existencia.Resumen> Existencia(string autoProducto, string autoDeposito)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibVenta.Inventario.Existencia.Resumen>();

            try
            {
                using (var cnn = new LibEntityVentas.libVentasEntities(_cnVenta.ConnectionString))
                {
                    var q = cnn.productos_deposito.FirstOrDefault(w => w.auto_producto == autoProducto && w.auto_deposito==autoDeposito);
                    if (q == null)
                    {
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Mensaje = "ENTIDAD [ DEPOSITO ] NO ENCONTRADA";
                        return result;
                    }

                    var codigoDep = q.empresa_depositos.codigo;
                    var descripcionDep = q.empresa_depositos.nombre;
                    var r = new DtoLibVenta.Inventario.Existencia.Resumen()
                    {
                        autoDeposito = q.auto_deposito,
                        cntDisponible = q.disponible,
                        cntFisica = q.fisica,
                        cntReservada = q.reservada,
                        CodigoDeposito = codigoDep,
                        DescripcionDeposito = descripcionDep,
                        Ubicacion_1 = q.ubicacion_1,
                        Ubicacion_2 = q.ubicacion_2,
                        Ubicacion_3 = q.ubicacion_3,
                        Ubicacion_4 = q.ubicacion_4,
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