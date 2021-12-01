using LibEntityPos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;


namespace ProvPos
{

    public partial class Provider: IPos.IProvider
    {

        public DtoLib.ResultadoId VentaAdm_Temporal_Encabezado_Registrar(DtoLibPos.VentaAdm.Temporal.Encabezado.Registrar.Ficha ficha)
        {
            var result = new DtoLib.ResultadoId();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var fechaSistema = cnn.Database.SqlQuery<DateTime>("select now()").FirstOrDefault();
                        var ent = new p_ventaadm()
                        {
                            auto_cliente = ficha.autoCliente,
                            auto_deposito = ficha.autoDeposito,
                            auto_sist_documento = ficha.autoSistDocumento,
                            auto_sucursal = ficha.autoSucursal,
                            auto_usuario = ficha.autoUsuario,
                            cirif_cliente = ficha.ciRifCliente,
                            estatus_pendiente = ficha.estatusPendiente,
                            factor_divisa = ficha.factorDivisa,
                            fecha = fechaSistema.Date,
                            hora = fechaSistema.ToShortTimeString(),
                            idEquipo = ficha.idEquipo,
                            monto = ficha.monto,
                            monto_divisa = ficha.montoDivisa,
                            nombre_cliente = ficha.razonSocialCliente,
                            nombre_deposito = ficha.nombreDeposito,
                            nombre_sist_documento = ficha.nombreSistDocumento,
                            nombre_sucursal = ficha.nombreSucursal,
                            nombre_usuario = ficha.nombreUsuario,
                            renglones = ficha.renglones
                        };
                        cnn.p_ventaadm.Add(ent);
                        cnn.SaveChanges();
                        result.Id = ent.id;

                        ts.Complete();
                    }
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }

        public DtoLib.Resultado VentaAdm_Temporal_Encabezado_Eliminar(int idEncabezado)
        {
            var result = new DtoLib.Resultado();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var ent = cnn.p_ventaadm.Find(idEncabezado);
                        if (ent == null)
                        {
                            result.Mensaje = "ENTIDAD NO ENCONTRADO";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        cnn.p_ventaadm.Remove(ent);
                        cnn.SaveChanges();

                        ts.Complete();
                    }
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }

        public DtoLib.Resultado VentaAdm_Temporal_Encabezado_Editar(DtoLibPos.VentaAdm.Temporal.Encabezado.Editar.Ficha ficha)
        {
            var result = new DtoLib.Resultado();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var ent = cnn.p_ventaadm.Find(ficha.id);
                        if (ent == null)
                        {
                            result.Mensaje = "ENTIDAD NO ENCONTRADO";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        ent.auto_cliente = ficha.autoCliente;
                        ent.auto_deposito = ficha.autoDeposito;
                        ent.auto_sucursal = ficha.autoSucursal;
                        ent.cirif_cliente = ficha.ciRifCliente;
                        ent.nombre_cliente = ficha.razonSocialCliente;
                        ent.nombre_deposito = ficha.nombreDeposito;
                        ent.nombre_sucursal = ficha.nombreSucursal;
                        cnn.SaveChanges();
                        ts.Complete();
                    }
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }

        public DtoLib.ResultadoId VentaAdm_Temporal_Item_Registrar(DtoLibPos.VentaAdm.Temporal.Item.Registrar.Ficha ficha)
        {
            var result = new DtoLib.ResultadoId();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var xenc = ficha.itemEncabezado;
                        var entEnc = cnn.p_ventaadm.Find(ficha.itemEncabezado.id);
                        if (entEnc == null)
                        {
                            result.Mensaje = "ENTIDAD [VENTA TEMPORAL ENCABEZADO] NO ENCONTRADO";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        entEnc.monto += xenc.monto;
                        entEnc.monto_divisa += xenc.montoDivisa;
                        entEnc.renglones += xenc.renglones;
                        cnn.SaveChanges();

                        if (ficha.itemActDeposito != null) 
                        {
                            var xit = ficha.itemActDeposito;
                            var entDep = cnn.productos_deposito.FirstOrDefault(f => f.auto_deposito == xit.autoDeposito && f.auto_producto == xit.autoProducto);
                            if (entDep == null)
                            {
                                result.Mensaje = "ENTIDAD DETALLE DEPOSITO [" + xit.prdDescripcion + "] NO ENCONTRADO";
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                return result;
                            }
                            entDep.reservada += xit.cntActualizar;
                            entDep.disponible -= xit.cntActualizar;
                            cnn.SaveChanges();

                            if (ficha.validarExistencia)
                            {
                                if (entDep.disponible < 0)
                                {
                                    result.Mensaje = "EXISTENCIA NO DISPONIBLE PARA [" + xit.prdDescripcion + "] ";
                                    result.Result = DtoLib.Enumerados.EnumResult.isError;
                                    return result;
                                }
                            }
                        }

                        var it = ficha.itemDetalle;
                        var ent = new p_ventaadm_det()
                        {
                            auto_departamento = it.autoDepartamento,
                            auto_grupo = it.autoGrupo,
                            auto_producto = it.autoProducto,
                            auto_subGrupo = it.autoSubGrupo,
                            auto_tasa = it.autoTasaIva,
                            cantidad = it.cantidad,
                            categoria_producto = it.categroiaProducto,
                            codigo_producto = it.codigoProducto,
                            costo = it.costo,
                            costo_promedio = it.costoPromd,
                            costo_promedio_und = it.costoPromdUnd,
                            costo_und = it.costoUnd,
                            decimales = it.decimalesProducto,
                            dscto_porct = it.dsctoPorct,
                            empaque_cont = it.empaqueCont,
                            empaque_desc = it.empaqueDesc,
                            estatus_pesado = it.estatusPesadoProducto,
                            estatusReservaInv = it.estatusReservaMerc,
                            id_ventaAdm = it.idVenta,
                            nombre_producto = it.nombreProducto,
                            notas = it.notas,
                            precio_neto = it.precioNeto,
                            precio_neto_divisa = it.precioNetoDivisa,
                            tarifa_precio = it.tarifaPrecio,
                            tasa_iva = it.tasaIva,
                            tipo_iva = it.tipoIva,
                        };
                        cnn.p_ventaadm_det.Add(ent);
                        cnn.SaveChanges();
                        result.Id = ent.id;

                        ts.Complete();
                    }
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }

        public DtoLib.ResultadoEntidad<DtoLibPos.VentaAdm.Temporal.Item.Entidad.Ficha> VentaAdm_Temporal_Item_GetFichaById(int idItem)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibPos.VentaAdm.Temporal.Item.Entidad.Ficha>();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var ent = cnn.p_ventaadm_det.Find(idItem);
                    if (ent == null)
                    {
                        result.Mensaje = "ENTIDAD DETALLE NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }
                    result.Entidad = new DtoLibPos.VentaAdm.Temporal.Item.Entidad.Ficha()
                    {
                        autoDepartamento = ent.auto_departamento,
                        autoGrupo = ent.auto_grupo,
                        autoProducto = ent.auto_producto,
                        autoSubGrupo = ent.auto_subGrupo,
                        autoTasaIva = ent.auto_tasa,
                        cantidad = ent.cantidad,
                        categroiaProducto = ent.categoria_producto,
                        codigoProducto = ent.codigo_producto,
                        costo = ent.costo,
                        costoPromd = ent.costo_promedio,
                        costoPromdUnd = ent.costo_promedio_und,
                        costoUnd = ent.costo_und,
                        decimalesProducto = ent.decimales,
                        dsctoPorct = ent.dscto_porct,
                        empaqueCont = ent.empaque_cont,
                        empaqueDesc = ent.empaque_desc,
                        estatusPesadoProducto = ent.estatus_pesado,
                        estatusReservaMerc = ent.estatusReservaInv,
                        id = ent.id,
                        nombreProducto = ent.nombre_producto,
                        notas = ent.notas,
                        precioNeto = ent.precio_neto,
                        precioNetoDivisa = ent.precio_neto_divisa,
                        tarifaPrecio = ent.tarifa_precio,
                        tasaIva = ent.tasa_iva,
                        tipoIva = ent.tipo_iva,
                    };
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }

        public DtoLib.Resultado VentaAdm_Temporal_Item_Eliminar(DtoLibPos.VentaAdm.Temporal.Item.Eliminar.Ficha ficha)
        {
            var result = new DtoLib.Resultado();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {

                        if (ficha.itemActDeposito!=null)
                        {
                            var actDep = ficha.itemActDeposito;
                            var entDep = cnn.productos_deposito.FirstOrDefault(f => f.auto_deposito == actDep.autoDeposito && f.auto_producto == actDep.autoProducto);
                            if (entDep == null)
                            {
                                result.Mensaje = "ENTIDAD DEPOSITO PARA [" + actDep.prdDescripcion + "] NO ENCONTRADO";
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                return result;
                            }
                            entDep.reservada -= actDep.cntActualizar;
                            entDep.disponible += actDep.cntActualizar;
                            cnn.SaveChanges();
                        }

                        var rgDet = ficha.itemDetalle;
                        var entDet = cnn.p_ventaadm_det.Find(rgDet.id);
                        if (entDet == null)
                        {
                            result.Mensaje = "ENTIDAD VENTA TEMPORAL DETALLE NO ENCONTRADO";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        cnn.p_ventaadm_det.Remove(entDet);
                        cnn.SaveChanges();

                        var rgEnc = ficha.itemEncabezado;
                        var ent = cnn.p_ventaadm.Find(rgEnc.id);
                        if (ent == null)
                        {
                            result.Mensaje = "ENTIDAD VENTA TEMPORAL ENCABEZADO NO ENCONTRADO";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        ent.renglones -= rgEnc.renglones;
                        ent.monto -= rgEnc.monto;
                        ent.monto_divisa -= rgEnc.montoDivisa;
                        cnn.SaveChanges();

                        ts.Complete();
                    }
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }

        public DtoLib.Resultado VentaAdm_Temporal_Item_Limpiar(DtoLibPos.VentaAdm.Temporal.Item.Limpiar.Ficha ficha)
        {
            var result = new DtoLib.Resultado();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {

                        if (ficha.itemActDeposito != null)
                        {
                            foreach(var actDep in ficha.itemActDeposito)
                            {
                                var entDep = cnn.productos_deposito.FirstOrDefault(f => f.auto_deposito == actDep.autoDeposito && f.auto_producto == actDep.autoProducto);
                                if (entDep == null)
                                {
                                    result.Mensaje = "ENTIDAD DEPOSITO PARA [" + actDep.prdDescripcion + "] NO ENCONTRADO";
                                    result.Result = DtoLib.Enumerados.EnumResult.isError;
                                    return result;
                                }
                                entDep.reservada -= actDep.cntActualizar;
                                entDep.disponible += actDep.cntActualizar;
                                cnn.SaveChanges();
                            }
                        }

                        foreach (var rgDet in ficha.itemDetalle)
                        {
                            var entDet = cnn.p_ventaadm_det.Find(rgDet.id);
                            if (entDet == null)
                            {
                                result.Mensaje = "ENTIDAD VENTA TEMPORAL DETALLE NO ENCONTRADO";
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                return result;
                            }
                            cnn.p_ventaadm_det.Remove(entDet);
                            cnn.SaveChanges();
                        }

                        var rgEnc = ficha.itemEncabezado;
                        var ent = cnn.p_ventaadm.Find(rgEnc.id);
                        if (ent == null)
                        {
                            result.Mensaje = "ENTIDAD VENTA TEMPORAL ENCABEZADO NO ENCONTRADO";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        ent.renglones =0;
                        ent.monto = 0;
                        ent.monto_divisa = 0;
                        cnn.SaveChanges();
                        ts.Complete();
                    }
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }

        public DtoLib.Resultado VentaAdm_Temporal_Anular(DtoLibPos.VentaAdm.Temporal.Anular.Ficha ficha)
        {
            var result = new DtoLib.Resultado();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        if (ficha.ItemsActDeposito !=null)
                        {
                            foreach (var rg in ficha.ItemsActDeposito)
                            {
                                var entDep = cnn.productos_deposito.FirstOrDefault(f => f.auto_deposito == rg.autoDeposito && f.auto_producto == rg.autoProducto);
                                if (entDep == null)
                                {
                                    result.Mensaje = "ENTIDAD DEPOSITO PARA [" + rg.prdDescripcion + "] NO ENCONTRADO";
                                    result.Result = DtoLib.Enumerados.EnumResult.isError;
                                    return result;
                                }
                                entDep.reservada -= rg.cntActualizar;
                                entDep.disponible += rg.cntActualizar;
                                cnn.SaveChanges();
                            }
                        }
                        if (ficha.Items != null) 
                        {
                            foreach (var rg in ficha.Items)
                            {
                                var entDet = cnn.p_ventaadm_det.Find(rg.idItem);
                                if (entDet == null)
                                {
                                    result.Mensaje = "ENTIDAD TEMPORAL VENTA DETALLE [" + rg.idItem.ToString() + "] NO ENCONTRADO";
                                    result.Result = DtoLib.Enumerados.EnumResult.isError;
                                    return result;
                                }
                                cnn.p_ventaadm_det.Remove(entDet);
                                cnn.SaveChanges();
                            }
                        }

                        var ent = cnn.p_ventaadm.Find(ficha.IdEncabezado);
                        if (ent == null)
                        {
                            result.Mensaje = "ENTIDAD TEMPORAL VENTA ENCABEZADO NO ENCONTRADO";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        cnn.p_ventaadm.Remove(ent);
                        cnn.SaveChanges();

                        ts.Complete();
                    }
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