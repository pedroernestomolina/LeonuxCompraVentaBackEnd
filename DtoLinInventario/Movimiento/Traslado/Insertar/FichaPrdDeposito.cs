using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Movimiento.Traslado.Insertar
{
    
    public class FichaPrdDeposito
    {

        public string autoProducto { get; set; }
        public string autoDepositoOrigen { get; set; }
        public string autoDepositoDestino { get; set; }
        public decimal cantidadUnd { get; set; }

    }

}