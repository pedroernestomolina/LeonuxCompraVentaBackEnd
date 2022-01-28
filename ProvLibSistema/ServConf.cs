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

        public DtoLib.Resultado Inicializar_BD(DtoLibSistema.Configuracion.InicializarBD.Ficha ficha)
        {
            var result = new DtoLib.Resultado();

            try
            {
                using (var cnn = new sistemaEntities(_cnSist.ConnectionString))
                {
                    //
                    var cmd = "SET foreign_key_checks = 0";
                    cnn.Database.ExecuteSqlCommand(cmd);

                    var fechaSistema = cnn.Database.SqlQuery<DateTime>("select now()").FirstOrDefault();
                    var fechaNula = new DateTime(2000, 1, 1);

                    //AUDITORIA
                    cmd = "truncate table auditoria_accesos";
                    cnn.Database.ExecuteSqlCommand(cmd);

                    cmd = "truncate table auditoria_documentos";
                    cnn.Database.ExecuteSqlCommand(cmd);

                    //USUARIOS
                    cmd = "truncate table usuarios_grupo_permisos";
                    cnn.Database.ExecuteSqlCommand(cmd);

                    cmd = "truncate table usuarios_grupo";
                    cnn.Database.ExecuteSqlCommand(cmd);

                    cmd = "truncate table usuarios";
                    cnn.Database.ExecuteSqlCommand(cmd);

                    cmd = "INSERT INTO `usuarios_grupo` (`auto`, `nombre`) VALUES ('0000000001', 'ADMINISTRADOR')";
                    cnn.Database.ExecuteSqlCommand(cmd);

                    cmd = @"INSERT INTO usuarios_grupo_permisos (codigo_grupo, codigo_funcion, estatus, seguridad)
                            select '0000000001',sf.codigo, '1', 'Ninguna' from sistema_funciones as sf";
                    cnn.Database.ExecuteSqlCommand(cmd);

                    var fp1 = new MySql.Data.MySqlClient.MySqlParameter();
                    var fp2 = new MySql.Data.MySqlClient.MySqlParameter();
                    fp1.ParameterName = "@fAlta";
                    fp1.Value = fechaSistema.Date;
                    fp2.ParameterName = "fNula";
                    fp2.Value = fechaNula;
                    cmd = @"INSERT INTO `usuarios` (`auto`, `nombre`, `clave`, `codigo`, `auto_grupo`, `estatus`,
                        `estatus_replica`, `fecha_alta`, `fecha_baja`, `fecha_sesion`, `apellido`) 
                        VALUES ('0000000001', 'SUPERVISOR', '', 'SUPERVISOR', '0000000001', 'Activo',
                        '0', @falta, @fNula, @fNula, 'SUPERVISOR')";
                    cnn.Database.ExecuteSqlCommand(cmd, fp1,fp2);

                    //EMPRESA

                    cmd = "truncate table empresa";
                    cnn.Database.ExecuteSqlCommand(cmd);

                    cmd = "INSERT INTO `empresa` (`auto` ,`nombre` ,`direccion` ,`rif` ,`telefono` ,`sucursal` ,`codigo_sucursal` ," +
                        "`nit` ,`contacto` ,`fax` ,`email` ,`website` ,`registro` ,`codigo` ,`pais` ,`estado` ,`ciudad` ,`codigo_postal` ," +
                        "`tasa1` ,`tasa2` ,`tasa3` ,`retencion_iva` ,`retencion_islr` ,`factor_cambio` ,`debito_bancario` ,`solicita` ," +
                        "`recibe` ,`precio_1` ,`precio_2` ,`precio_3` ,`precio_4` ,`precio_5`) " +
                        "VALUES ('0000000001', '', '', '', '', 'PRINCIPAL', '01', '', '', '', '', '', " +
                        "'ABC', '01', 'VZLA', '', '', '', '16', '8', '21', '75', '2', '0.00', '0.00', '', " +
                        "'', 'Precio 1', 'Precio 2', 'Precio 3', 'Precio 4', 'Precio 5')";
                    cnn.Database.ExecuteSqlCommand(cmd);

                    cmd = "truncate table empresa_agencias";
                    cnn.Database.ExecuteSqlCommand(cmd);

                    cmd = "INSERT INTO `empresa_agencias` (`auto`, `nombre`) VALUES ('0000000001', '(NA)')";
                    cnn.Database.ExecuteSqlCommand(cmd);

                    cmd = "truncate table empresa_cobradores";
                    cnn.Database.ExecuteSqlCommand(cmd);

                    cmd = "INSERT INTO `empresa_cobradores` (`auto`, `codigo`, `nombre`, `comision`, `contrato`) VALUES ('0000000001', '01', 'DIRECTO', '0.00', '')";
                    cnn.Database.ExecuteSqlCommand(cmd);

                    cmd = "truncate table empresa_departamentos";
                    cnn.Database.ExecuteSqlCommand(cmd);

                    cmd = "INSERT INTO `empresa_departamentos` (`auto`, `codigo`, `nombre`, `comision_g`, `comision_1`, `comision_2`, `comision_3`, `comision_4`) VALUES ('0000000001', '01', 'GENERICO', '0.00', '0.00', '0.00', '0.00', '0.00')";
                    cnn.Database.ExecuteSqlCommand(cmd);

                    cmd = "truncate table empresa_depositos_ext";
                    cnn.Database.ExecuteSqlCommand(cmd);

                    cmd = "truncate table empresa_depositos";
                    cnn.Database.ExecuteSqlCommand(cmd);

                    cmd = "INSERT INTO `empresa_depositos` (`auto`, `nombre`, `codigo`, `codigo_sucursal`) "+ 
                        "VALUES ('0000000001', 'PISO DE VENTA', '01', '01'), "+
                        "('0000000002', 'ALMACEN', '02', '01')";
                    cnn.Database.ExecuteSqlCommand(cmd);

                    cmd = @"INSERT INTO empresa_depositos_ext (auto_deposito, es_predeterminado, es_activo) 
                            select auto,'','1' from empresa_depositos";
                    cnn.Database.ExecuteSqlCommand(cmd);

                    cmd = "truncate table empresa_grupo";
                    cnn.Database.ExecuteSqlCommand(cmd);

                    cmd = "INSERT INTO `empresa_grupo` (`auto`, `nombre`, `idPrecio`) VALUES ('0000000001', 'PRINCIPAL', '1')";
                    cnn.Database.ExecuteSqlCommand(cmd);

                    cmd = "truncate table empresa_sucursal";
                    cnn.Database.ExecuteSqlCommand(cmd);

                    cmd = "INSERT INTO `empresa_sucursal` (`auto`, `autoDepositoPrincipal`, `autoEmpresaGrupo`, `codigo`, `nombre`) VALUES ('0000000001', '0000000001', '0000000001', '01', 'PRINCIPAL')";
                    cnn.Database.ExecuteSqlCommand(cmd);

                    cmd = "truncate table empresa_transporte";
                    cnn.Database.ExecuteSqlCommand(cmd);

                    cmd = "INSERT INTO `empresa_transporte` (`auto`, `codigo`, `nombre`, `contrato`) VALUES ('0000000001', '01', 'PROPIO', '')";
                    cnn.Database.ExecuteSqlCommand(cmd);

                    cmd = "truncate table empresa_series_fiscales";
                    cnn.Database.ExecuteSqlCommand(cmd);

                    cmd = "INSERT INTO `empresa_series_fiscales` (`auto`, `serie`, `correlativo`, `estatus_factura`, `estatus_nd`, `estatus_nc`, `estatus_ne`, `estatus`, `control`) VALUES " +
                        "('0000000001', '1', '0', '1', '1', '0', '0', 'Activo', '')," +
                        "('0000000002', '1D', '0', '0', '0', '1', '0', 'Activo', '')," +
                        "('0000000003', 'NEN', '0', '0', '0', '0', '1', 'Activo', '')";
                    cnn.Database.ExecuteSqlCommand(cmd);

                    cmd = "truncate table empresa_tasas";
                    cnn.Database.ExecuteSqlCommand(cmd);

                    cmd = "INSERT INTO `empresa_tasas` (`auto`, `nombre`, `tasa`) VALUES ('0000000001', 'IVA', '16'), ('0000000002', 'IVA REDUCIDO', '8'), ('0000000003', 'IVA LUJO', '21'), ('0000000004', 'EXENTO', '0.00')";
                    cnn.Database.ExecuteSqlCommand(cmd);

                    cmd = "truncate table empresa_medios";
                    cnn.Database.ExecuteSqlCommand(cmd);

                    cmd= "INSERT INTO `empresa_medios` (`auto`, `codigo`, `nombre`, `estatus_cobro`, `estatus_pago`) "+
                        "VALUES ('0000000001', '01', 'Efectivo', '1', '1'),"+
                        "('0000000002', '02', 'Divisa', '1', '1'),"+
                        "('0000000003', '03', 'Tarjeta Debito', '1', '1'),"+
                        "('0000000004', '04', 'Tarjeta Credito', '1', '1'),"+
                        "('0000000005', '05', 'Transferencia/Depositos', '1', '1'),"+
                        "('0000000006', '06', 'Ticket Cesta', '1', '0'),"+
                        "('0000000007', '07', 'Giros', '0', '0'),"+
                        "('0000000008', '08', 'Pagare', '1', '0'),"+
                        "('0000000009', '09', 'Otro', '1', '0')";
                    cnn.Database.ExecuteSqlCommand(cmd);

                    //VENDEDORES
                    cmd = "truncate table vendedores_comisiones";
                    cnn.Database.ExecuteSqlCommand(cmd);

                    cmd = "truncate table vendedores";
                    cnn.Database.ExecuteSqlCommand(cmd);

                    cmd="INSERT INTO `vendedores` (`auto`, `codigo`, `nombre`, `ci`, `direccion`, `contacto`, "+
                        "`telefono`, `email`, `website`, `fecha_alta`, `fecha_baja`, `memo`, `advertencia`, "+
                        "`estatus`, `ventas_g`, `ventas_1`, `ventas_2`, `ventas_3`, `ventas_4`, `cobranza_g`,"+
                        "`cobranza_1`, `cobranza_2`, `cobranza_3`, `cobranza_4`, `estatus_ventas`, `estatus_cobranza`,"+
                        "`estatus_departamento`, `estatus_f1`, `estatus_f2`, `estatus_f3`, `estatus_f4`, `estatus_f5`, "+
                        "`estatus_f6`, `estatus_f7`, `estatus_f8`, `castigop`, `tolerancia`) "+
                        "VALUES ('0000000001', '01', 'CAJA', 'N/A', 'OFICINA', '', '', '', '', "+
                        "'2000-01-01', '2000-01-01', '', '', 'Activo', '0.00', '0.00', '0.00', "+
                        "'0.00', '0.00', '0.00', '0.00', '0.00', '0.00', '0.00', '0', '0', '0', "+
                        "'0', '0', '0', '0', '0', '0', '0', '0', '0.00', '0')";
                    cnn.Database.ExecuteSqlCommand(cmd);

                    //BANCOS
                    cmd = "truncate table bancos_movimientos_conceptos";
                    cnn.Database.ExecuteSqlCommand(cmd);

                    cmd = "truncate table bancos_movimientos";
                    cnn.Database.ExecuteSqlCommand(cmd);

                    cmd = "truncate table bancos";
                    cnn.Database.ExecuteSqlCommand(cmd);

                    //CLIENTES
                    cmd = "truncate table clientes_extra";
                    cnn.Database.ExecuteSqlCommand(cmd);

                    cmd = "truncate table clientes";
                    cnn.Database.ExecuteSqlCommand(cmd);

                    cmd = "truncate table clientes_grupo";
                    cnn.Database.ExecuteSqlCommand(cmd);

                    cmd = "INSERT INTO `clientes_grupo` (`auto`, `nombre`, `codigo`) VALUES ('0000000001', 'GENERICO', '01')";
                    cnn.Database.ExecuteSqlCommand(cmd);

                    cmd = "truncate table clientes_zonas";
                    cnn.Database.ExecuteSqlCommand(cmd);

                    cmd = "INSERT INTO `clientes_zonas` (`auto`, `nombre`, `codigo`) VALUES ('0000000001', 'LOCAL', '01')";
                    cnn.Database.ExecuteSqlCommand(cmd);

                    cmd = "INSERT INTO `clientes` (`auto`, `codigo`, `nombre`, `ci_rif`, `razon_social`, `auto_grupo`, `dir_fiscal`," +
                        "`dir_despacho`, `contacto`, `telefono`, `email`, `website`, `pais`, `denominacion_fiscal`, `auto_estado`, " +
                        "`auto_zona`, `codigo_postal`, `retencion_iva`, `retencion_islr`, `auto_vendedor`, `tarifa`, `descuento`, " +
                        "`recargo`, `estatus_credito`, `dias_credito`, `limite_credito`, `doc_pendientes`, `estatus_morosidad`, " +
                        "`estatus_lunes`, `estatus_martes`, `estatus_miercoles`, `estatus_jueves`, `estatus_viernes`, `estatus_sabado`," +
                        "`estatus_domingo`, `auto_cobrador`, `fecha_alta`, `fecha_baja`, `fecha_ult_venta`, `fecha_ult_pago`, " +
                        "`anticipos`, `debitos`, `creditos`, `saldo`, `disponible`, `memo`, `aviso`, `estatus`, `cuenta`, `iban`," +
                        "`swit`, `auto_agencia`, `dir_banco`, `auto_codigo_cobrar`, `auto_codigo_ingresos`, `auto_codigo_anticipos`, " +
                        "`categoria`, `descuento_pronto_pago`, `importe_ult_pago`, `importe_ult_venta`, `telefono2`, `fax`, `celular`, " +
                        "`abc`, `fecha_clasificacion`, `monto_clasificacion`) " +
                        "VALUES ('0000000001', '01', 'NO CONTRIBUYENTE', 'NA', 'NO CONTRIBUYENTE', '0000000001', 'VALENCIA', " +
                        "'', '', '', '', '', 'VZLA', 'No Contribuyente', '0000000001', '0000000001', '', '0.00', '0.00'," +
                        "'0000000001', '1', '0.00', '0.00', '0', '0', '0.00', '0', '0', '0', '0', '0', '0', '0', '0', '0'," +
                        "'0000000001', '2000-01-01', '2000-01-01', '2000-01-01', '2000-01-01', '0.00', '0.00', '0.00', '0.00', " +
                        "'0.00', '', '', 'Activo', '', '', '', '0000000001', '', '0000000001', '0000000001', '0000000001'," +
                        "'Eventual', '0.00', '0.00', '0.00', '', '', '', 'C', '2000-01-01', '0.00')";
                    cnn.Database.ExecuteSqlCommand(cmd);

                    //PROVEEDORES
                    cmd = "truncate table proveedores_agencias";
                    cnn.Database.ExecuteSqlCommand(cmd);

                    cmd = "truncate table proveedores";
                    cnn.Database.ExecuteSqlCommand(cmd);

                    cmd = "truncate table proveedores_grupo";
                    cnn.Database.ExecuteSqlCommand(cmd);

                    cmd = "INSERT INTO `proveedores_grupo` (`auto`, `nombre`, `codigo`) VALUES ('0000000001', 'DIRECTO', '01')";
                    cnn.Database.ExecuteSqlCommand(cmd);

                    cmd = "INSERT INTO `proveedores` (`auto`, `codigo`, `nombre`, `ci_rif`, `razon_social`, `auto_grupo`, " +
                        "`dir_fiscal`, `contacto`, `telefono`, `email`, `website`, `pais`, `denominacion_fiscal`, `auto_estado`, " +
                        "`codigo_postal`, `retencion_iva`, `retencion_islr`, `fecha_alta`, `fecha_baja`, `fecha_ult_pago`, " +
                        "`fecha_ult_compra`, `anticipos`, `debitos`, `creditos`, `saldo`, `disponible`, `memo`, `advertencia`, " +
                        "`estatus`, `auto_codigo_cobrar`, `auto_codigo_ingresos`, `auto_codigo_anticipos`, `beneficiario`, `rif`, " +
                        "`ctabanco`, `nj`) " +
                        "VALUES ('0000000001', '', '', 'NA', 'DIRECTO', '0000000001', 'VALENCIA', '', '', '', '', 'VZLA', " +
                        "'Contribuyente', '0000000001', '', '0.00', '0.00', '2000-01-01', '2000-01-01', '2000-01-01', " +
                        "'2000-01-01', '0.00', '0.00', '0.00', '0.00', '0.00', '', '', 'Activo', '0000000001', '0000000001', " +
                        "'0000000001', '', '', '', '')";
                    cnn.Database.ExecuteSqlCommand(cmd);

                    //CXC
                    cmd = "truncate table cxc_medio_pago";
                    cnn.Database.ExecuteSqlCommand(cmd);

                    cmd = "truncate table cxc_documentos";
                    cnn.Database.ExecuteSqlCommand(cmd);

                    cmd = "truncate table cxc_recibos";
                    cnn.Database.ExecuteSqlCommand(cmd);

                    cmd = "truncate table cxc";
                    cnn.Database.ExecuteSqlCommand(cmd);

                    //CXP
                    cmd = "truncate table cxp_medio_pago";
                    cnn.Database.ExecuteSqlCommand(cmd);

                    cmd = "truncate table cxp_documentos";
                    cnn.Database.ExecuteSqlCommand(cmd);

                    cmd = "truncate table cxp_recibos";
                    cnn.Database.ExecuteSqlCommand(cmd);

                    cmd = "truncate table cxp";
                    cnn.Database.ExecuteSqlCommand(cmd);

                    //COMPRAS
                    cmd = "truncate table compras_detalle";
                    cnn.Database.ExecuteSqlCommand(cmd);

                    cmd = "truncate table compras_retenciones_detalle";
                    cnn.Database.ExecuteSqlCommand(cmd);

                    cmd = "truncate table compras_seriales";
                    cnn.Database.ExecuteSqlCommand(cmd);

                    cmd = "truncate table compras_retenciones";
                    cnn.Database.ExecuteSqlCommand(cmd);

                    cmd = "truncate table compras";
                    cnn.Database.ExecuteSqlCommand(cmd);

                    //POS
                    cmd = "truncate table pos_arqueo";
                    cnn.Database.ExecuteSqlCommand(cmd);

                    cmd = "truncate table pos_eventos";
                    cnn.Database.ExecuteSqlCommand(cmd);

                    cmd = "truncate table pos_cuentas";
                    cnn.Database.ExecuteSqlCommand(cmd);

                    cmd = "truncate table pos_cierre_detalle";
                    cnn.Database.ExecuteSqlCommand(cmd);

                    cmd = "truncate table pos_cierrez";
                    cnn.Database.ExecuteSqlCommand(cmd);

                    cmd = "truncate table pos_jornadas";
                    cnn.Database.ExecuteSqlCommand(cmd);

                    //VENTAS
                    cmd = "truncate table ventas_detalle";
                    cnn.Database.ExecuteSqlCommand(cmd);

                    cmd = "truncate table ventas_guias_detalle";
                    cnn.Database.ExecuteSqlCommand(cmd);

                    cmd = "truncate table ventas_retenciones_detalle";
                    cnn.Database.ExecuteSqlCommand(cmd);

                    cmd = "truncate table ventas_retenciones";
                    cnn.Database.ExecuteSqlCommand(cmd);

                    cmd = "truncate table ventas_guias";
                    cnn.Database.ExecuteSqlCommand(cmd);

                    cmd = "truncate table ventas";
                    cnn.Database.ExecuteSqlCommand(cmd);

                    //PRODUCTOS
                    cmd = "truncate table productos_alterno";
                    cnn.Database.ExecuteSqlCommand(cmd);

                    cmd = "truncate table productos_conteo";
                    cnn.Database.ExecuteSqlCommand(cmd);

                    cmd = "truncate table productos_costos";
                    cnn.Database.ExecuteSqlCommand(cmd);

                    cmd = "truncate table productos_precios_ext";
                    cnn.Database.ExecuteSqlCommand(cmd);

                    cmd = "truncate table productos_precios";
                    cnn.Database.ExecuteSqlCommand(cmd);

                    cmd = "truncate table productos_deposito";
                    cnn.Database.ExecuteSqlCommand(cmd);

                    cmd = "truncate table productos_extra";
                    cnn.Database.ExecuteSqlCommand(cmd);

                    cmd = "truncate table productos_proveedor";
                    cnn.Database.ExecuteSqlCommand(cmd);

                    cmd = "truncate table productos_movimientos_detalle";
                    cnn.Database.ExecuteSqlCommand(cmd);

                    cmd = "truncate table productos_movimientos_seriales";
                    cnn.Database.ExecuteSqlCommand(cmd);

                    cmd = "truncate table productos_movimientos";
                    cnn.Database.ExecuteSqlCommand(cmd);

                    cmd = "truncate table productos_kardex";
                    cnn.Database.ExecuteSqlCommand(cmd);

                    cmd = "truncate table productos_falla";
                    cnn.Database.ExecuteSqlCommand(cmd);

                    cmd = "truncate table productos_marca";
                    cnn.Database.ExecuteSqlCommand(cmd);

                    cmd = "INSERT INTO `productos_marca` (`auto`, `nombre`, `tipo`) VALUES ('0000000001', 'GENERICO', '')";
                    cnn.Database.ExecuteSqlCommand(cmd);

                    cmd = "truncate table productos_grupo";
                    cnn.Database.ExecuteSqlCommand(cmd);

                    cmd = "INSERT INTO `productos_grupo` (`auto`, `codigo`, `nombre`, `estatus_catalogo`) VALUES ('0000000001', '01', 'GENERICO', '0')";
                    cnn.Database.ExecuteSqlCommand(cmd);

                    cmd = "truncate table productos_subgrupo";
                    cnn.Database.ExecuteSqlCommand(cmd);

                    cmd = "INSERT INTO `productos_subgrupo` (`auto`, `codigo`, `nombre`, `auto_grupo`) VALUES ('0000000001', '', '', '0000000001')";
                    cnn.Database.ExecuteSqlCommand(cmd);

                    cmd = "truncate table productos_medida";
                    cnn.Database.ExecuteSqlCommand(cmd);

                    cmd = "INSERT INTO `productos_medida` (`auto`, `nombre`, `decimales`) " +
                        "VALUES ('0000000001', 'UNIDAD', '0'), ('0000000002', 'KILO', '3')," +
                        "('0000000003', 'METROS', '2'), ('0000000004', 'LITROS', '3')," +
                        "('0000000005', 'CAJA', '0'), ('0000000006', 'BULTO', '0')," +
                        "('0000000007', 'PAQUETE', '0'), ('0000000008', 'HORA', '0')";
                    cnn.Database.ExecuteSqlCommand(cmd);

                    cmd = "truncate table productos_lista";
                    cnn.Database.ExecuteSqlCommand(cmd);

                    cmd = "INSERT INTO `productos_lista` (`id`, `codigo`, `nombre`) VALUES (NULL, '01', 'GENERICO')";
                    cnn.Database.ExecuteSqlCommand(cmd);

                    cmd = "truncate table productos_conceptos";
                    cnn.Database.ExecuteSqlCommand(cmd);

                    cmd = "INSERT INTO `productos_conceptos` (`auto`, `codigo`, `nombre`) " +
                        "VALUES ('0000000001', 'VENTAS', 'VENTAS MERCANCIA'), " +
                        "('0000000002', 'COMPRAS', 'COMPRAS MERCANCIA'), " +
                        "('0000000003', 'DEV_VENTAS', 'DEVOLUCIONES EN VENTAS'), " +
                        "('0000000004', 'DEV_COMPRAS', 'DEVOLUCIONES EN COMPRAS'), " +
                        "('0000000005', 'ENTRADAS', 'ENTRADAS DE MERCANCIA'), " +
                        "('0000000006', 'SALIDAS', 'SALIDAS DE MERCANCIA'), " +
                        "('0000000007', 'AJUSTE', 'AJUSTE DE INVENTARIO'), " +
                        "('0000000008', 'TRASL', 'TRASLADO ENTRE DEPOSITOS')";
                    cnn.Database.ExecuteSqlCommand(cmd);

                    cmd = "truncate table productos_ext";
                    cnn.Database.ExecuteSqlCommand(cmd);

                    cmd = "truncate table productos";
                    cnn.Database.ExecuteSqlCommand(cmd);

                    cmd = "INSERT INTO `productos` (`auto`, `codigo`, `nombre`, `nombre_corto`, `auto_departamento`, " +
                        "`auto_grupo`, `auto_subgrupo`, `auto_tasa`, `auto_empaque_compra`, `costo_proveedor`, " +
                        "`costo_proveedor_und`, `costo_importacion`, `costo_importacion_und`, `costo_varios`, " +
                        "`costo_varios_und`, `costo`, `costo_und`, `costo_promedio`, `costo_promedio_und`, " +
                        "`utilidad_1`, `utilidad_2`, `utilidad_3`, `utilidad_4`, `utilidad_pto`, " +
                        "`precio_1`, `precio_2`, `precio_3`, `precio_4`, `precio_pto`, `estatus_garantia`, `dias_garantia`, " +
                        "`modelo`, `precio_sugerido`, `comentarios`, `referencia`, `contenido_compras`, `estatus`, " +
                        "`advertencia`, `fecha_alta`, `fecha_baja`, `auto_codigo_plan`, `categoria`, `origen`, " +
                        "`alto`, `largo`, `ancho`, `peso`, `codigo_arancel`, `tasa_arancel`, `auto_marca`, " +
                        "`estatus_serial`, `estatus_oferta`, `inicio`, `fin`, `precio_oferta`, `estatus_web`," +
                        "`estatus_corte`, `tasa`, `auto_precio_1`, `auto_precio_2`, `auto_precio_3`, `auto_precio_4`, " +
                        "`auto_precio_pto`, `memo`, `contenido_1`, `contenido_2`, `contenido_3`, `contenido_4`, " +
                        "`contenido_pto`, `corte`, `estatus_pesado`, `plu`, `estatus_compuesto`, `estatus_catalogo`," +
                        "`estatus_cambio`, `fecha_movimiento`, `fecha_cambio`, `fecha_ult_venta`, `presentacion`, `lugar`," +
                        "`fecha_ult_costo`, `fecha_lote`, `abc`, `divisa`, `estatus_lote`, `estatus_divisa`, " +
                        "`pdf_1`, `pdf_2`, `pdf_3`, `pdf_4`, `pdf_pto`) " +
                        "VALUES ('0000000001', 'EXENTO', 'PRODUCTO EXENTO', '', '0000000001', '0000000001', '0000000001', " +
                        "'0000000004', '0000000001', '0.00', '0.00', '0.00', '0.00', '0.00', '0.00', '0.00', '0.00', '0.00'," +
                        "'0.00', '0.00', '0.00', '0.00', '0.00', '0.00', '0.00', '0.00', '0.00', '0.00', '0.00', '0', '0', ''," +
                        "'0.00', '', '', '1', 'Activo', '', '2000-01-01', '2000-01-01', '0000000001', 'Producto Terminado'," +
                        "'Nacional', '0.00', '0.00', '0.00', '0.000', '', '0.00', '0000000001', '0', '0', '2000-01-01', " +
                        "'2000-01-01', '0.00', '0', '0', '0.00', '0000000001', '0000000001', '0000000001', '0000000001', " +
                        "'0000000001', '', '1', '1', '1', '1', '1', '', '0', '', '0', '0', '0', '2000-01-01', '2000-01-01', " +
                        "'2000-01-01', '', '01', '2000-01-01', '2000-01-01', 'D', '0.00', '0', '0', '0.00', '0.00', '0.00', " +
                        "'0.00', '0.00')";
                    cnn.Database.ExecuteSqlCommand(cmd);

                    cmd = "INSERT INTO `productos` (`auto`, `codigo`, `nombre`, `nombre_corto`, `auto_departamento`, " +
                        "`auto_grupo`, `auto_subgrupo`, `auto_tasa`, `auto_empaque_compra`, `costo_proveedor`, " +
                        "`costo_proveedor_und`, `costo_importacion`, `costo_importacion_und`, `costo_varios`, " +
                        "`costo_varios_und`, `costo`, `costo_und`, `costo_promedio`, `costo_promedio_und`, " +
                        "`utilidad_1`, `utilidad_2`, `utilidad_3`, `utilidad_4`, `utilidad_pto`, " +
                        "`precio_1`, `precio_2`, `precio_3`, `precio_4`, `precio_pto`, `estatus_garantia`, `dias_garantia`, " +
                        "`modelo`, `precio_sugerido`, `comentarios`, `referencia`, `contenido_compras`, `estatus`, " +
                        "`advertencia`, `fecha_alta`, `fecha_baja`, `auto_codigo_plan`, `categoria`, `origen`, " +
                        "`alto`, `largo`, `ancho`, `peso`, `codigo_arancel`, `tasa_arancel`, `auto_marca`, " +
                        "`estatus_serial`, `estatus_oferta`, `inicio`, `fin`, `precio_oferta`, `estatus_web`," +
                        "`estatus_corte`, `tasa`, `auto_precio_1`, `auto_precio_2`, `auto_precio_3`, `auto_precio_4`, " +
                        "`auto_precio_pto`, `memo`, `contenido_1`, `contenido_2`, `contenido_3`, `contenido_4`, " +
                        "`contenido_pto`, `corte`, `estatus_pesado`, `plu`, `estatus_compuesto`, `estatus_catalogo`," +
                        "`estatus_cambio`, `fecha_movimiento`, `fecha_cambio`, `fecha_ult_venta`, `presentacion`, `lugar`," +
                        "`fecha_ult_costo`, `fecha_lote`, `abc`, `divisa`, `estatus_lote`, `estatus_divisa`, " +
                        "`pdf_1`, `pdf_2`, `pdf_3`, `pdf_4`, `pdf_pto`) " +
                        "VALUES ('0000000002', 'TASA1', 'PRODUCTO IVA', '', '0000000001', '0000000001', '0000000001', " +
                        "'0000000001', '0000000001', '0.00', '0.00', '0.00', '0.00', '0.00', '0.00', '0.00', '0.00', '0.00'," +
                        "'0.00', '0.00', '0.00', '0.00', '0.00', '0.00', '0.00', '0.00', '0.00', '0.00', '0.00', '0', '0', ''," +
                        "'0.00', '', '', '1', 'Activo', '', '2000-01-01', '2000-01-01', '0000000001', 'Producto Terminado'," +
                        "'Nacional', '0.00', '0.00', '0.00', '0.000', '', '0.00', '0000000001', '0', '0', '2000-01-01', " +
                        "'2000-01-01', '0.00', '0', '0', '16.00', '0000000001', '0000000001', '0000000001', '0000000001', " +
                        "'0000000001', '', '1', '1', '1', '1', '1', '', '0', '', '0', '0', '0', '2000-01-01', '2000-01-01', " +
                        "'2000-01-01', '', '01', '2000-01-01', '2000-01-01', 'D', '0.00', '0', '0', '0.00', '0.00', '0.00', " +
                        "'0.00', '0.00')";
                    cnn.Database.ExecuteSqlCommand(cmd);

                    cmd = "INSERT INTO `productos` (`auto`, `codigo`, `nombre`, `nombre_corto`, `auto_departamento`, " +
                        "`auto_grupo`, `auto_subgrupo`, `auto_tasa`, `auto_empaque_compra`, `costo_proveedor`, " +
                        "`costo_proveedor_und`, `costo_importacion`, `costo_importacion_und`, `costo_varios`, " +
                        "`costo_varios_und`, `costo`, `costo_und`, `costo_promedio`, `costo_promedio_und`, " +
                        "`utilidad_1`, `utilidad_2`, `utilidad_3`, `utilidad_4`, `utilidad_pto`, " +
                        "`precio_1`, `precio_2`, `precio_3`, `precio_4`, `precio_pto`, `estatus_garantia`, `dias_garantia`, " +
                        "`modelo`, `precio_sugerido`, `comentarios`, `referencia`, `contenido_compras`, `estatus`, " +
                        "`advertencia`, `fecha_alta`, `fecha_baja`, `auto_codigo_plan`, `categoria`, `origen`, " +
                        "`alto`, `largo`, `ancho`, `peso`, `codigo_arancel`, `tasa_arancel`, `auto_marca`, " +
                        "`estatus_serial`, `estatus_oferta`, `inicio`, `fin`, `precio_oferta`, `estatus_web`," +
                        "`estatus_corte`, `tasa`, `auto_precio_1`, `auto_precio_2`, `auto_precio_3`, `auto_precio_4`, " +
                        "`auto_precio_pto`, `memo`, `contenido_1`, `contenido_2`, `contenido_3`, `contenido_4`, " +
                        "`contenido_pto`, `corte`, `estatus_pesado`, `plu`, `estatus_compuesto`, `estatus_catalogo`," +
                        "`estatus_cambio`, `fecha_movimiento`, `fecha_cambio`, `fecha_ult_venta`, `presentacion`, `lugar`," +
                        "`fecha_ult_costo`, `fecha_lote`, `abc`, `divisa`, `estatus_lote`, `estatus_divisa`, " +
                        "`pdf_1`, `pdf_2`, `pdf_3`, `pdf_4`, `pdf_pto`) " +
                        "VALUES ('0000000003', 'TASA2', 'PRODUCTO IVA REDUCIDO', '', '0000000001', '0000000001', '0000000001', " +
                        "'0000000002', '0000000001', '0.00', '0.00', '0.00', '0.00', '0.00', '0.00', '0.00', '0.00', '0.00'," +
                        "'0.00', '0.00', '0.00', '0.00', '0.00', '0.00', '0.00', '0.00', '0.00', '0.00', '0.00', '0', '0', ''," +
                        "'0.00', '', '', '1', 'Activo', '', '2000-01-01', '2000-01-01', '0000000001', 'Producto Terminado'," +
                        "'Nacional', '0.00', '0.00', '0.00', '0.000', '', '0.00', '0000000001', '0', '0', '2000-01-01', " +
                        "'2000-01-01', '0.00', '0', '0', '8.00', '0000000001', '0000000001', '0000000001', '0000000001', " +
                        "'0000000001', '', '1', '1', '1', '1', '1', '', '0', '', '0', '0', '0', '2000-01-01', '2000-01-01', " +
                        "'2000-01-01', '', '01', '2000-01-01', '2000-01-01', 'D', '0.00', '0', '0', '0.00', '0.00', '0.00', " +
                        "'0.00', '0.00')";
                    cnn.Database.ExecuteSqlCommand(cmd);

                    cmd = "INSERT INTO `productos` (`auto`, `codigo`, `nombre`, `nombre_corto`, `auto_departamento`, " +
                        "`auto_grupo`, `auto_subgrupo`, `auto_tasa`, `auto_empaque_compra`, `costo_proveedor`, " +
                        "`costo_proveedor_und`, `costo_importacion`, `costo_importacion_und`, `costo_varios`, " +
                        "`costo_varios_und`, `costo`, `costo_und`, `costo_promedio`, `costo_promedio_und`, " +
                        "`utilidad_1`, `utilidad_2`, `utilidad_3`, `utilidad_4`, `utilidad_pto`, " +
                        "`precio_1`, `precio_2`, `precio_3`, `precio_4`, `precio_pto`, `estatus_garantia`, `dias_garantia`, " +
                        "`modelo`, `precio_sugerido`, `comentarios`, `referencia`, `contenido_compras`, `estatus`, " +
                        "`advertencia`, `fecha_alta`, `fecha_baja`, `auto_codigo_plan`, `categoria`, `origen`, " +
                        "`alto`, `largo`, `ancho`, `peso`, `codigo_arancel`, `tasa_arancel`, `auto_marca`, " +
                        "`estatus_serial`, `estatus_oferta`, `inicio`, `fin`, `precio_oferta`, `estatus_web`," +
                        "`estatus_corte`, `tasa`, `auto_precio_1`, `auto_precio_2`, `auto_precio_3`, `auto_precio_4`, " +
                        "`auto_precio_pto`, `memo`, `contenido_1`, `contenido_2`, `contenido_3`, `contenido_4`, " +
                        "`contenido_pto`, `corte`, `estatus_pesado`, `plu`, `estatus_compuesto`, `estatus_catalogo`," +
                        "`estatus_cambio`, `fecha_movimiento`, `fecha_cambio`, `fecha_ult_venta`, `presentacion`, `lugar`," +
                        "`fecha_ult_costo`, `fecha_lote`, `abc`, `divisa`, `estatus_lote`, `estatus_divisa`, " +
                        "`pdf_1`, `pdf_2`, `pdf_3`, `pdf_4`, `pdf_pto`) " +
                        "VALUES ('0000000004', 'TASA3', 'PRODUCTO IVA LUJO', '', '0000000001', '0000000001', '0000000001', " +
                        "'0000000003', '0000000001', '0.00', '0.00', '0.00', '0.00', '0.00', '0.00', '0.00', '0.00', '0.00'," +
                        "'0.00', '0.00', '0.00', '0.00', '0.00', '0.00', '0.00', '0.00', '0.00', '0.00', '0.00', '0', '0', ''," +
                        "'0.00', '', '', '1', 'Activo', '', '2000-01-01', '2000-01-01', '0000000001', 'Producto Terminado'," +
                        "'Nacional', '0.00', '0.00', '0.00', '0.000', '', '0.00', '0000000001', '0', '0', '2000-01-01', " +
                        "'2000-01-01', '0.00', '0', '0', '21.00', '0000000001', '0000000001', '0000000001', '0000000001', " +
                        "'0000000001', '', '1', '1', '1', '1', '1', '', '0', '', '0', '0', '0', '2000-01-01', '2000-01-01', " +
                        "'2000-01-01', '', '01', '2000-01-01', '2000-01-01', 'D', '0.00', '0', '0', '0.00', '0.00', '0.00', " +
                        "'0.00', '0.00')";
                    cnn.Database.ExecuteSqlCommand(cmd);

                    cmd = @"insert into productos_ext(auto_producto, auto_precio_may_1, auto_precio_may_2, auto_precio_may_3, 
                            contenido_may_1, contenido_may_2, contenido_may_3, utilidad_may_1, utilidad_may_2, utilidad_may_3, 
                            precio_may_1, precio_may_2, precio_may_3, pdmf_1, pdmf_2, pdmf_3) 
                            select auto, '0000000001',  '0000000001',  '0000000001', 1,1,1, 0,0,0, 0,0,0, 0,0,0 from productos" ;
                    cnn.Database.ExecuteSqlCommand(cmd);
                    
                    //SISTEMA 
                    cmd = "update sistema set deposito_principal='0000000001', codigo_empresa='01', prefijo='0101'";
                    cnn.Database.ExecuteSqlCommand(cmd);

                    cmd = "truncate table sistema_estados";
                    cnn.Database.ExecuteSqlCommand(cmd);

                    cmd="INSERT INTO `sistema_estados` (`auto` ,`nombre`) VALUES ('0000000001', 'LOCAL')";
                    cnn.Database.ExecuteSqlCommand(cmd);

                    cmd = "truncate table sistema_transito_detalle";
                    cnn.Database.ExecuteSqlCommand(cmd);

                    cmd = "truncate table sistema_transito_asiento_detalle";
                    cnn.Database.ExecuteSqlCommand(cmd);

                    cmd = "truncate table sistema_transito_seriales";
                    cnn.Database.ExecuteSqlCommand(cmd);

                    cmd = "truncate table sistema_transito_asiento";
                    cnn.Database.ExecuteSqlCommand(cmd);

                    cmd = "truncate table sistema_transito";
                    cnn.Database.ExecuteSqlCommand(cmd);

                    cmd="update sistema_contadores set a_clientes_grupo='1',"+
                        "a_usuarios='1',a_empresa_series_fiscales='3',a_empresa_depositos='2',"+
                        "a_empresa_medios='15',a_empresa_cobradores='1',a_empresa_transporte='1',"+
                        "a_clientes='1',a_productos_conceptos='15',"+
                        "a_sistema_estados='1',a_clientes_zonas='1',a_proveedores_grupo='1',a_proveedores='1',"+
                        "a_productos_grupo='1',a_productos_subgrupo='1',a_productos_marca='1',a_productos_medida='10',"+
                        "a_empresa_departamentos='1',a_productos='4',a_empresa_tasas='4',a_productos_movimientos='0',"+
                        "a_productos_movimientos_cargos='0',a_empresa_agencias='1',a_cxc='0',a_cxc_nd='0',a_cxc_nc='0',"+
                        "a_cxc_chq='0',"+
                        "a_cxc_recibo='0',a_cxc_recibo_numero='0',a_ventas='0',a_cxp='0',a_cxp_nd='0',a_cxp_nc='0',"+
                        "a_cxp_chq='0',a_cxp_recibo='0',a_cxp_recibo_numero='0',a_compras='0',a_ventas_presupuesto='0',"+
                        "a_ventas_pedido='0',"+
                        "a_sistema_transito='0',a_ventas_retenciones='0',a_ventas_despacho='0',a_cxc_anticipo='0',"+
                        "a_cxp_anticipo='0',a_productos_movimientos_descargos='0',a_productos_movimientos_traslados='0',"+
                        "a_productos_movimientos_ajustes='0',a_compras_orden='0',a_compras_retenciones='0',a_clientes_afiliados='0',"+
                        "a_compras_retencion_iva='0',a_compras_retencion_islr='0',a_pos_cuentas='0',a_pos_comandas='0',"+
                        "a_bancos='0',a_bancos_movimientos='0',a_bancos_movimientos_conceptos='0',a_bancos_egreso='0',"+
                        "a_sistema_transito_asiento='0',a_sistema_transito_asiento_numero='0',a_vendedores='1',"+
                        "a_bancos_beneficiarios='0',a_vendedores_comisiones='0',a_vendedores_comisiones_numero='0',"+
                        "a_cierre='0', a_productos_conteo='0', a_compras_lista='0', a_compras_recepcion='0', "+
                        "a_productos_movimientos_recepcion='0',a_empresa_sucursal='1', a_cierre_ftp='0',a_empresa_grupo='1',"+
                        "a_usuarios_grupo='1', a_productos_movimientos_traslados_dev='0'";
                    cnn.Database.ExecuteSqlCommand(cmd);

                    var p1= new MySql.Data.MySqlClient.MySqlParameter("@p1",ficha.CodSucursal);
                    var p2 = new MySql.Data.MySqlClient.MySqlParameter("@p2", ficha.CodSucursal+ficha.IdEquipo);
                    cmd = "update sistema set codigo_empresa=@p1, prefijo=@p2";
                    cnn.Database.ExecuteSqlCommand(cmd, p1,p2);


                    // TABLAS NUEVAS

                    cmd = "truncate table productos_movimientos_extra";
                    cnn.Database.ExecuteSqlCommand(cmd);

                    cmd = "truncate table compras_pend_detalle";
                    cnn.Database.ExecuteSqlCommand(cmd);

                    cmd = "truncate table compras_pend";
                    cnn.Database.ExecuteSqlCommand(cmd);

                    cmd = "truncate table p_venta";
                    cnn.Database.ExecuteSqlCommand(cmd);

                    cmd = "truncate table p_pendiente";
                    cnn.Database.ExecuteSqlCommand(cmd);

                    cmd = "truncate table p_resumen";
                    cnn.Database.ExecuteSqlCommand(cmd);

                    cmd = "truncate table p_operador";
                    cnn.Database.ExecuteSqlCommand(cmd);

                    cmd = "truncate table p_configuracion";
                    cnn.Database.ExecuteSqlCommand(cmd);

                    //
                    cmd = "SET foreign_key_checks = 1";
                    cnn.Database.ExecuteSqlCommand(cmd);
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