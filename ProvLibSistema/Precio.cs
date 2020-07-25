using LibEntitySistema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProvLibSistema
{

    public partial class Provider : ILibSistema.IProvider 
    {

        public DtoLib.ResultadoLista<DtoLibSistema.Precio.Resumen> Precio_GetLista()
        {
            var result = new DtoLib.ResultadoLista<DtoLibSistema.Precio.Resumen>();

            try
            {
                using (var cnn = new sistemaEntities(_cnSist.ConnectionString))
                {
                    var ent= cnn.empresa.FirstOrDefault();
                    if (ent == null) 
                    {
                        result.Mensaje = "ENTIDAD [ EMPRESA ] NO ENCONTRADA";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }

                    var _des = "";
                    _des = ent.precio_1.Trim();
                    var list = new List<DtoLibSistema.Precio.Resumen>();
                    var p1 = new DtoLibSistema.Precio.Resumen()
                    {
                        id = "1",
                        descripcion =  _des==""?"Precio 1":_des,
                    };
                    list.Add(p1);

                    _des = ent.precio_2.Trim();
                    var p2 = new DtoLibSistema.Precio.Resumen()
                    {
                        id = "2",
                        descripcion = _des == "" ? "Precio 2" : _des,
                    };
                    list.Add(p2);

                    _des = ent.precio_3.Trim();
                    var p3 = new DtoLibSistema.Precio.Resumen()
                    {
                        id = "3",
                        descripcion = _des == "" ? "Precio 3" : _des,
                    };
                    list.Add(p3);

                    _des = ent.precio_4.Trim();
                    var p4 = new DtoLibSistema.Precio.Resumen()
                    {
                        id = "4",
                        descripcion = _des == "" ? "Precio 4" : _des,
                    };
                    list.Add(p4);

                    _des = ent.precio_5.Trim();
                    var p5 = new DtoLibSistema.Precio.Resumen()
                    {
                        id = "5",
                        descripcion = _des == "" ? "Precio 5" : _des,
                    };
                    list.Add(p5);

                    result.Lista = list;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }

    }

}