using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibCompra.Documento.Pendiente.Lista
{
    
    public class Ficha
    {

        public int id { get; set; }
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


        public Ficha() 
        {
            id = -1;
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
        }

    }

}