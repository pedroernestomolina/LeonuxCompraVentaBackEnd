using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPosOffLine.Cliente.ExportarData
{
    
    public class Ficha
    {

        public string CiRif { get; set; }
        public string NombreRazonSocial { get; set; }
        public string DirFiscal { get; set; }
        public string Telefono { get; set; }

        public Ficha() 
        {
            CiRif = "";
            NombreRazonSocial = "";
            DirFiscal = "";
            Telefono = "";
        }

    }

}