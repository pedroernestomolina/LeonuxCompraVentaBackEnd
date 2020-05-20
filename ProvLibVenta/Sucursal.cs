using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProvLibVenta
{

    public partial class Provider: ILibVentas.IProvider 
    {

        public DtoLib.ResultadoLista<DtoLibVenta.Sucursal.Resumen> SucursalLista()
        {
            var result = new DtoLib.ResultadoLista<DtoLibVenta.Sucursal.Resumen>();

            var list = new List<DtoLibVenta.Sucursal.Resumen>();
            var r1 = new DtoLibVenta.Sucursal.Resumen()
            {
                Codigo = "01",
                Nombre = "GALPON",
            };
            var r2 = new DtoLibVenta.Sucursal.Resumen()
            {
                Codigo = "02",
                Nombre = "PARAPARAL",
            };
            var r3 = new DtoLibVenta.Sucursal.Resumen()
            {
                Codigo = "03",
                Nombre = "AGUA DORADA",
            };
            var r4 = new DtoLibVenta.Sucursal.Resumen()
            {
                Codigo = "04",
                Nombre = "NAGUANAGUA",
            };
            var r5 = new DtoLibVenta.Sucursal.Resumen()
            {
                Codigo = "05",
                Nombre = "ALMACEN MAYOR",
            };
            list.Add(r1);
            list.Add(r2);
            list.Add(r3);
            list.Add(r4);
            list.Add(r5);
            result.Lista = list;

            return result;
        }

    }

}