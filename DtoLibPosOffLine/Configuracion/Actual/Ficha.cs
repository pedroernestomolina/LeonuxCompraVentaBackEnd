using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPosOffLine.Configuracion.Actual
{

    public class Ficha
    {

        public string CodigoSucursal { get; set; }
        public string EquipoNumero { get; set; }

        public bool ActivarRepesaje { get; set; }
        public decimal LimiteSuperiorRepesaje { get; set; }
        public decimal LimiteInferiorRepesaje { get; set; }
        public bool ActivarBusquedaPorDescripcion { get; set; }
        public int ClavePos { get; set; }
        public string TarifaPrecio { get; set; }
        public bool EtiquetarPrecioPorTipoNegocio { get; set; }

        public string SerieFactura { get; set; }
        public string SerieNotaCredito { get; set; }
        public string SerieNotaDebito { get; set; }
        public string SerieNotaEntrega { get; set; }

        public string AutoDeposito { get; set; }
        public string AutoCobrador { get; set; }
        public string AutoVendedor { get; set; }
        public string AutoTransporte { get; set; }

        public string AutoMedioEfectivo { get; set; }
        public string AutoMedioDivisa { get; set; }
        public string AutoMedioElectronico { get; set; }
        public string AutoMedioOtro { get; set; }

        public string AutoMovConceptoVenta { get; set; }
        public string AutoMovConceptoDevVenta { get; set; }
        public string AutoMovConceptoSalida { get; set; }

    }

}