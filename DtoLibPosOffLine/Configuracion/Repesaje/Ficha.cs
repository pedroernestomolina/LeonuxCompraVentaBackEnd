using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPosOffLine.Configuracion.Repesaje
{

    public class Ficha
    {

        public bool IsActivo { get; set; }
        public decimal LimiteValidoInferior { get; set; }
        public decimal LimiteValidoSuperior { get; set; }

    }

}