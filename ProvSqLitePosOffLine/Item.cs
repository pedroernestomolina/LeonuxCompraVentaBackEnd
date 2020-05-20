using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProvSqLitePosOffLine
{

    public partial class Provider : IPosOffLine.IProvider
    {

        public DtoLib.ResultadoEntidad<DtoLibPosOffLine.Item.Ficha> Item(int id)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibPosOffLine.Item.Ficha>();

            try
            {
                using (var cnn = new LibEntitySqLitePosOffLine.LeonuxPosOffLineEntities(_cnn.ConnectionString))
                {

                    var ent = cnn.Item.Find(id);
                    if (ent == null)
                    {
                        result.Mensaje = "ITEM [ ID ] NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    };

                    var esPesado=ent.esPesado==1?true:false;
                    var nr = new DtoLibPosOffLine.Item.Ficha()
                    {
                        Id = (int)ent.id,
                        AutoPrd=ent.autoPrd,
                        NombrePrd=ent.nombrePrd,
                        Cantidad=ent.cantidad,
                        TasaImpuesto=ent.tasaIva,
                        PrecioNeto=ent.precioNeto,
                        EsPesado=esPesado,
                        TipoIva=ent.tipoIva,
                        CostoCompraUnd = ent.costoUnd,
                        CostoPromedioUnd = ent.costoPromUnd,
                        AutoDepartamento = ent.autoDepartamento,
                        AutoGrupo = ent.autoGrupo,
                        AutoSubGrupo = ent.autoSubGrupo,
                        AutoTasaIva = ent.autoTasa,
                        Categoria = ent.categoria,
                        CodigoPrd = ent.codigoProducto,
                        Decimales = ent.decimales,
                        DiasEmpaqueGarantia = (int)ent.diasEmpaqueGarantia,
                        EmpContenido = (int)ent.empaqueContenido,
                        EmpCodigo = ent.empaqueCodigo,
                        EmpDescripcion = ent.empaqueDescripcion,
                        TarifaPrecio=ent.tarifaPrecio,
                        PrecioSugerido=ent.precioSugerido,
                    };
                    result.Entidad = nr;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }

        public DtoLib.ResultadoId Item_Agregar(DtoLibPosOffLine.Item.Agregar ficha)
        {
            var result = new DtoLib.ResultadoId();

            try
            {
                using (var cnn = new LibEntitySqLitePosOffLine.LeonuxPosOffLineEntities(_cnn.ConnectionString))
                {

                    var entItem = new LibEntitySqLitePosOffLine.Item()
                    {
                        idPendiente=-1,
                        autoPrd=ficha.AutoPrd,
                        nombrePrd=ficha.NombrePrd,
                        cantidad=ficha.Cantidad,
                        tasaIva=ficha.TasaImpuesto,
                        precioNeto=ficha.PrecioNeto,
                        esPesado=ficha.EsPesado,
                        tipoIva=ficha.TipoIva,
                        costoUnd=ficha.CostoCompraUnd,
                        costoPromUnd=ficha.CostoPromedioUnd,
                        autoDepartamento=ficha.AutoDepartamento,
                        autoGrupo=ficha.AutoGrupo,
                        autoSubGrupo=ficha.AutoSubGrupo,
                        autoTasa=ficha.AutoTasaIva,
                        categoria=ficha.Categoria,
                        codigoProducto=ficha.CodigoPrd,
                        decimales=ficha.Decimales,
                        diasEmpaqueGarantia=ficha.DiasEmpaqueGarantia,
                        empaqueCodigo=ficha.EmpCodigo,
                        empaqueDescripcion=ficha.EmpDescripcion,
                        empaqueContenido=ficha.EmpContenido,
                        tarifaPrecio=ficha.TarifaPrecio,
                        precioSugerido=ficha.PrecioSugerido,
                    };
                    cnn.Item .Add(entItem);
                    cnn.SaveChanges();
                    result.Id = (int)entItem.id;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }

        public DtoLib.ResultadoLista<DtoLibPosOffLine.Item.Ficha> Item_Cargar()
        {
            var result = new DtoLib.ResultadoLista<DtoLibPosOffLine.Item.Ficha>();

            try
            {
                using (var cnn = new LibEntitySqLitePosOffLine.LeonuxPosOffLineEntities(_cnn.ConnectionString))
                {
                    var list = new List<DtoLibPosOffLine.Item.Ficha>();
                    
                    var lstEnt = cnn.Item.Where(w=>w.idPendiente==-1).ToList();
                    if (lstEnt != null) 
                    {
                        if (lstEnt.Count > 0) 
                        {
                            list = lstEnt.Select(s =>
                            {
                                var esPesado = s.esPesado == 1 ? true : false;
                                var nr = new DtoLibPosOffLine.Item.Ficha()
                                {
                                    Id = (int)s.id,
                                    AutoPrd = s.autoPrd,
                                    NombrePrd = s.nombrePrd,
                                    Cantidad = s.cantidad,
                                    TasaImpuesto = s.tasaIva,
                                    PrecioNeto = s.precioNeto,
                                    EsPesado = esPesado,
                                    TipoIva = s.tipoIva,
                                    CostoCompraUnd=s.costoUnd,
                                    CostoPromedioUnd=s.costoPromUnd,
                                    AutoDepartamento= s.autoDepartamento,
                                    AutoGrupo= s.autoGrupo,
                                    AutoSubGrupo=s.autoSubGrupo,
                                    AutoTasaIva= s.autoTasa,
                                    Categoria= s.categoria,
                                    CodigoPrd = s.codigoProducto,
                                    Decimales= s.decimales,
                                    DiasEmpaqueGarantia=(int)s.diasEmpaqueGarantia,
                                    EmpContenido=(int)s.empaqueContenido,
                                    EmpCodigo=s.empaqueCodigo,
                                    EmpDescripcion=s.empaqueDescripcion,
                                    TarifaPrecio=s.tarifaPrecio,
                                    PrecioSugerido=s.precioSugerido,
                                };
                                return nr;
                            }).ToList();
                        }
                    }

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

        public DtoLib.Resultado Item_Limpiar()
        {
            var result = new DtoLib.Resultado();

            try
            {
                using (var cnn = new LibEntitySqLitePosOffLine.LeonuxPosOffLineEntities(_cnn.ConnectionString))
                {
                    var lst = cnn.Item.Where(w=>w.idPendiente==-1).ToList();
                    cnn.Item.RemoveRange(lst);
                    cnn.SaveChanges();
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }

        public DtoLib.Resultado Item_Actualizar(DtoLibPosOffLine.Item.Actualizar ficha)
        {
            var result = new DtoLib.Resultado();

            try
            {
                using (var cnn = new LibEntitySqLitePosOffLine.LeonuxPosOffLineEntities(_cnn.ConnectionString))
                {
                    var ent = cnn.Item.Find(ficha.Id);
                    if (ent == null)
                    {
                        result.Mensaje = "ITEM [ ID ] NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    };
                    ent.cantidad = ficha.Cantidad;
                    cnn.SaveChanges();
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }

        public DtoLib.Resultado Item_Eliminar(int id)
        {
            var result = new DtoLib.Resultado();

            try
            {
                using (var cnn = new LibEntitySqLitePosOffLine.LeonuxPosOffLineEntities(_cnn.ConnectionString))
                {
                    var ent = cnn.Item.Find(id);
                    if (ent == null)
                    {
                        result.Mensaje = "ITEM [ ID ] NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    };
                    cnn.Item.Remove(ent);
                    cnn.SaveChanges();
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