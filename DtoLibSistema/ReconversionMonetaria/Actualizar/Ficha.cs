using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibSistema.ReconversionMonetaria.Actualizar
{
    
    public class Ficha
    {

        public decimal factorReconverion { get; set; }
        public decimal tasaDivisa { get; set; }
        public decimal tasaDivisaPos { get; set; }
        public string idUsuario { get; set; }
        public string codUsuario { get; set; }
        public string usuario { get; set; }
        public string equipoEstacion { get; set; }
        public List<ItemPrd> Producto { get; set; }
        public List<ItemHistCosto> HistoricoCosto {get;set;}
        public List<ItemHistPrecio> HistoricoPrecio { get; set; }
        public List<ItemProv> Proveedor {get;set;}
        public List<ItemSaldoPorPagar> SaldoPorPagar { get; set; }
        public int ItemsAfectados { get { return Producto.Count(); } }
        public int ProvAfectados { get { return Proveedor.Count(); } }
        public int SaldoPorPagarAfectados { get { return SaldoPorPagar.Count(); } }


        public Ficha()
        {
            idUsuario = "";
            codUsuario = "";
            usuario = "";
            factorReconverion = 0.0m;
            tasaDivisa = 0.0m;
            tasaDivisaPos = 0.0m;
            equipoEstacion = "";
            Producto = new List<ItemPrd>();
            Proveedor = new List<ItemProv>();
            SaldoPorPagar = new List<ItemSaldoPorPagar>();
            HistoricoCosto = new List<ItemHistCosto>();
            HistoricoPrecio = new List<ItemHistPrecio>();
        }

    }

}