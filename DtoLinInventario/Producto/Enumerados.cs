using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Producto
{

    public class Enumerados
    {

        public enum EnumMetodoBusqueda { SnDefinir = -1, Codigo=1, Nombre, Referencia };
        public enum EnumOrigen { SnDefinir = -1, Nacional = 1, Importado };
        public enum EnumEstatus { SnDefinir = -1, Activo= 1, Suspendido, Inactivo };
        public enum EnumCategoria { SnDefinir = -1 , ProductoTerminado=1, BienServicio, MateriaPrima, UsoInterno, SubProducto};
        public enum EnumAdministradorPorDivisa { SnDefinir = -1, Si=1, No };
        public enum EnumOferta { SnDefinir = -1, Si = 1, No };
        public enum EnumPesado { SnDefinir = -1, Si = 1, No };

    }

}