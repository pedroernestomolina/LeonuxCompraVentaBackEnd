using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibCompra.Documento.Pendiente.Filtro
{
    
    public class Ficha
    {

        public string idUsuario { get; set; }
        public string docTipo { get; set; }


        public Ficha() 
        {
            idUsuario = "";
            docTipo = "";
        }

    }

}