using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibSistema.Configuracion.ActualizarTasaDivisa.ActualizarData
{
    
    public class Ficha
    {

        public string autoUsuario { get; set; }
        public string codigoUsuario { get; set; }
        public string nombreUsuario { get; set; }
        public string EstacionEquipo { get; set; }
        public decimal ValorDivisa { get; set; }
        public List<FichaProductoCostoSinDivisa> productosCostoSinDivisa { get; set; }
        public List<FichaProductoCostoPrecioDivisa> productosCostoPrecioDivisa { get; set; }
        public List<FichaProductoPrecioHistorico> productosPrecioHistorico { get; set; }


        public Ficha()
        {
            productosCostoSinDivisa = new List<FichaProductoCostoSinDivisa>();
            productosCostoPrecioDivisa = new List<FichaProductoCostoPrecioDivisa>();
            productosPrecioHistorico = new List<FichaProductoPrecioHistorico>();
        }

    }

}