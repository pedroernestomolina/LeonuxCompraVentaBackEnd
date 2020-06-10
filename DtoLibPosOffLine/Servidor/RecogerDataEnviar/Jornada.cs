using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPosOffLine.Servidor.RecogerDataEnviar
{
    
    public class Jornada
    {

        public int Id { get; set; }
        public List<Documento> Documentos { get; set; }

    }

}