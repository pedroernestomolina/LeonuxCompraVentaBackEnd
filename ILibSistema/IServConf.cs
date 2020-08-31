using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ILibSistema
{
    
    public interface IServConf
    {

        DtoLib.Resultado Inicializar_BD(DtoLibSistema.Configuracion.InicializarBD.Ficha ficha);

    }

}