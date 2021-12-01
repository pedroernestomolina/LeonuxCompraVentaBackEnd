using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.VentaAdm.Temporal.Encabezado.Registrar
{
    
    public class Ficha
    {

        public string idEquipo { get; set; }
        public string autoUsuario { get; set; }
        public string autoCliente { get; set; }
        public string autoSucursal { get; set; }
        public string autoDeposito { get; set; }
        public string autoSistDocumento { get; set; }
        public string ciRifCliente { get; set; }
        public string razonSocialCliente { get; set; }
        public decimal monto { get; set; }
        public decimal montoDivisa { get; set; }
        public decimal factorDivisa {get;set;}
        public int renglones { get; set; }
        public string estatusPendiente { get; set; }
        public string nombreSistDocumento { get; set; }
        public string nombreUsuario { get; set; }
        public string nombreDeposito { get; set; }
        public string nombreSucursal { get; set; }


        public Ficha() 
        {
            idEquipo = "";
            autoCliente = "";
            autoDeposito = "";
            autoSucursal = "";
            autoSistDocumento = "";
            ciRifCliente = "";
            razonSocialCliente = "";
            monto = 0m;
            montoDivisa = 0m;
            factorDivisa = 0m;
            renglones = 0;
            estatusPendiente = "";
            nombreSistDocumento = "";
            nombreUsuario = "";
            nombreDeposito = "";
            nombreSucursal = "";
        }

    }

}