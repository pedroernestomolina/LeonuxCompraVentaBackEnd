using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPosOffLine.Cliente.Editar
{
    
    public class Ficha
    {

        public int Id { get; set; }
        public string NombreRazaonSocial { get; set; }
        public string DirFiscal { get; set; }
        public string Telefono { get; set; }


        public Ficha() 
        {
            Id = -1;
            NombreRazaonSocial = "";
            DirFiscal = "";
            Telefono = "";
        }

    }

}