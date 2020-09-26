using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Movimiento.Cargo.Insertar
{
    
    public class Ficha
    {

        public string autoConcepto { get; set; }
        public string autoDepositoOrigen { get; set; }
        public string autoDepositoDestino { get; set; }
        public string autoRemision { get; set; }
        public string autoUsuario { get; set; }

        public string nota { get; set; }
        public string estatusAnulado { get; set; }
        public string usuario { get; set; }
        public string codUsuario { get; set; }
        public string estacion { get; set; }
        public string codConcepto { get; set; }
        public string desConcepto { get; set; }
        public string codDepositoOrigen { get; set; }
        public string desDepositoOrigen { get; set; }
        public string codDepositoDestino { get; set; }
        public string desDepositoDestino { get; set; }
        public string tipo { get; set; }
        public int renglones { get; set; }
        public string documentoNombre { get; set; }
        public string autorizado { get; set; }
        public decimal total { get; set; }
        public string situacion { get; set; }
        public string codigoSucursal { get; set; }
        public string cierreFtp { get; set; }
        public string estatusCierreContable { get; set; }

        public List<FichaDetalle> detalles { get; set; }
        public List<FichaPrdDeposito> prdDeposito { get; set; }
        public List<FichaKardex> movKardex { get; set; }
        public List<FichaPrdCosto> prdCosto { get; set; }
        public List<FichaPrdCostoHistorico> prdCostoHistorico { get; set; }
        public List<FichaPrdPrecio> prdPrecio { get; set; }
        public List<FichaPrdPrecioMargen> prdPrecioMargen { get; set; }
        public List<FichaPrdPrecioHistorico> prdPrecioHistorico { get; set; }

    }

}