using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibCompra.Documento.Pendiente.Agregar
{
    
    public class Ficha
    {

        public string usuarioId { get; set; }
        public string usuarioNombre { get; set; }
        public string entidadCiRif { get; set; }
        public string entidadNombre { get; set; }
        public string docTipo { get; set; }
        public string docNombre { get; set; }
        public decimal docMonto { get; set; }
        public decimal docMontoDivisa { get; set; }
        public decimal docFactorCambio { get; set; }
        public int docItemsNro { get; set; }
        public string docNumero { get; set; }
        public string docControl { get; set; }
        public List<FichaDetalle> items { get; set; }


        public Ficha() 
        {
            usuarioId = "";
            usuarioNombre = "";
            entidadCiRif = "";
            entidadNombre = "";
            docTipo = "";
            docNombre = "";
            docMonto = 0.0m;
            docMontoDivisa = 0.0m;
            docFactorCambio = 0.0m;
            docItemsNro = 0;
            docNumero = "";
            docControl = "";
            items = new List<FichaDetalle>();
        }

    }

}