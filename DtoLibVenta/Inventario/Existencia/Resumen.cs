using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibVenta.Inventario.Existencia
{

    public class Resumen
    {

        public string autoDeposito { get; set; }
        public string CodigoDeposito { get; set; }
        public string DescripcionDeposito { get; set; }
        public decimal cntFisica { get; set; }
        public decimal cntReservada { get; set; }
        public decimal cntDisponible { get; set; }
        public string Ubicacion_1 { get; set; }
        public string Ubicacion_2 { get; set; }
        public string Ubicacion_3 { get; set; }
        public string Ubicacion_4 { get; set; }

    }

}