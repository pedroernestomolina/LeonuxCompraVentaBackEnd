﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IPosOffLine
{

    public interface IProvider: IProducto, ICliente, IServidor, IItem, IVentaDocumento, IFiscal, IConfiguracion,
        IPendiente
    {

        void setServidorRemoto(string instancia, string baseDatos);
        DtoLib.ResultadoEntidad<DateTime> FechaServidor();

    }

}