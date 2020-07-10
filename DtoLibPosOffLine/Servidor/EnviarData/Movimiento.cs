using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPosOffLine.Servidor.EnviarData
{
    
    public class Movimiento
    {

        public string Prefijo { get; set; }
        public MovCierre Cierre { get; set; }
        public List<Documento> Documentos { get; set; }

    }

}