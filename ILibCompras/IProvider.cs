﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ILibCompras
{

    public interface IProvider: ICompra, IDeposito, ISucursal, IProveedor, IProducto
    {
    }

}