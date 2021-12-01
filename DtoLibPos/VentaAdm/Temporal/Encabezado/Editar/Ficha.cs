using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.VentaAdm.Temporal.Encabezado.Editar
{
    
    public class Ficha
    {

        public int id { get; set; }
        public string autoCliente { get; set; }
        public string autoSucursal { get; set; }
        public string autoDeposito { get; set; }
        public string ciRifCliente { get; set; }
        public string razonSocialCliente { get; set; }
        public string nombreDeposito { get; set; }
        public string nombreSucursal { get; set; }


        public Ficha() 
        {
            id = -1;
            autoCliente = "";
            autoDeposito = "";
            autoSucursal = "";
            ciRifCliente = "";
            razonSocialCliente = "";
            nombreDeposito = "";
            nombreSucursal = "";
        }

    }

}