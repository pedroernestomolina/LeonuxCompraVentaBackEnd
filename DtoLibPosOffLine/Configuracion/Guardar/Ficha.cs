using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPosOffLine.Configuracion.Guardar
{
    
    public class Ficha
    {

        public string ActivarRepesaje { get; set; }
        public decimal LimiteSuperiorRepesaje { get; set; }
        public decimal LimiteInferiorRepesaje { get; set; }
        public string ActivarBusquedaPorDescripcion { get; set; }
        public int ClavePos { get; set; }

        public string SerieFactura { get; set; }
        public string SerieNotaCredito { get; set; }
        public string SerieNotaDebito { get; set; }
        public string SerieNotaEntrega { get; set; }

        public string AutoCobrador { get; set; }
        public string AutoVendedor { get; set; }
        public string AutoTransporte { get; set; }

        public string AutoMedioEfectivo { get; set; }
        public string AutoMedioDivisa { get; set; }
        public string AutoMedioElectronico { get; set; }
        public string AutoMedioOtro { get; set; }

    }

}