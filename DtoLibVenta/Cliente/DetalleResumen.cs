using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibVenta.Cliente
{
    
    public class DetalleResumen
    {

        public string Auto { get; set; }
        public string AutoGrupo { get; set; }
        public string AutoVendedor { get; set; }
        public string AutoCobrador { get; set; }
        public string AutoEstado { get; set; }
        public string AutoZona { get; set; }

        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string CiRif { get; set; }
        public string DireccionFiscal { get; set; }
        public string Contacto { get; set; }
        public Enumerados.enumTarifaPrecio Tarifa { get; set; }
        public decimal PorctDescuento { get; set; }
        public decimal PorctRecargo { get; set; }
        public bool IsCreditoHabilitado { get; set; }
        public int DiasCredito { get; set; }
        public Enumerados.enumCategoria Categoria { get; set; }
        public decimal LimitePorMonto { get; set; }
        public int LimitePorDocumento { get; set; }
        public string Notas{ get; set; }
        public string Aviso { get; set; }
        public bool IsActivo { get; set; }
        public Enumerados.enumDenominacionFiscal DenominacionFiscal { get; set; }

        public string Email { get; set; }
        public string Telefono_1 { get; set; }
        public string Telefono_2 { get; set; }
        public string Celular { get; set; }

        public string GrupoDescripcion { get; set; }
        public string GrupoCodigo { get; set; }

        public string EstadoDescripcion { get; set; }

        public string ZonaDescripcion { get; set; }
        public string ZonaCodigo { get; set; }

        public string CobradorNombre { get; set; }
        public string CobradorCodigo { get; set; }

        public string VendedorNombre { get; set; }
        public string VendedorCodigo { get; set; }

    }

}