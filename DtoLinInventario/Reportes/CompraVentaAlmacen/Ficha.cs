using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Reportes.CompraVentaAlmacen
{
    
    public class Ficha
    {
        
        //DATOS GENERAL
        public string producto { get; set; }
        public string tipoDocumento { get; set; }
        public decimal factor { get; set; }

        //COMPRAS
        public string c_documento { get; set; }
        public DateTime c_fecha { get; set; }
        public decimal c_cntCompra { get; set; }
        public string c_empaque { get; set; }
        public int c_contenido { get; set; }
        public decimal c_cntCompraUnd { get; set; }
        public decimal c_costoUnd { get; set; }
        public decimal c_costoUndDivisa { get; set; }

        //VENTAS
        public decimal v_cnt { get; set; }
        public decimal v_montoVenta { get; set; }
        public decimal v_montoVentaDivisa { get; set; }
        public decimal v_montoCosto { get; set; }
        public decimal v_montoCostoDivisa { get; set; }

        //PRODUCTOS
        public decimal p_existencia { get; set; }

    }

}