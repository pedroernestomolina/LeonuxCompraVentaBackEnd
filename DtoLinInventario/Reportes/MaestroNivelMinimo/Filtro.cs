﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Reportes.MaestroNivelMinimo
{
    
    public class Filtro
    {

        public string autoDepartamento { get; set; }
        public string autoDeposito { get; set; }


        public Filtro()
        {
            autoDepartamento = "";
            autoDeposito = "";
        }

    }

}