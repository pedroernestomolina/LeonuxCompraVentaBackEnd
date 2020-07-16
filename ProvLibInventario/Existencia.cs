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

        public DtoLib.ResultadoEntidad<DtoLibInventario.Existencia.Deposito.Ficha> Producto_Existencia_Deposito(DtoLibInventario.Existencia.Deposito.Filtro filtro)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibInventario.Existencia.Deposito.Ficha>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var ent = cnn.productos_deposito.FirstOrDefault(f => f.auto_producto == filtro.autoProducto && f.auto_deposito==filtro.autoDeposito);
                    if (ent == null) 
                    {
                        result.Mensaje = "PROBLEMA AL ENCONTRAR PRODUCTO / DEPOSITO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }

                    var fnula=new DateTime(2000,01,01);
                    var fconteo=ent.fecha_conteo;
                    var nr = new DtoLibInventario.Existencia.Deposito.Ficha()
                    {
                        disponible = ent.disponible,
                        fechaUltConteo = ent.fecha_conteo == fnula ? (DateTime?)null : fconteo,
                        fisica = ent.fisica,
                        nivelMinimo = ent.nivel_minimo,
                        nivelOptimo = ent.nivel_optimo,
                        ptoPedido = ent.pto_pedido,
                        reservada = ent.reservada,
                        resultadoUltConteo = ent.resultado_conteo,
                        ubicacion_1 = ent.ubicacion_1,
                        ubicacion_2 = ent.ubicacion_2,
                        ubicacion_3 = ent.ubicacion_3,
                        ubicacion_4 = ent.ubicacion_4,
                    };
                    result.Entidad = nr;
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