using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProvSqLitePosOffLine
{


    public class DataPrd
    {
        public string Auto { get; set; }
        public string AutoDepartamento { get; set; }
        public string AutoGrupo { get; set; }
        public string AutoSubGrupo { get; set; }
        public string AutoTasaIva { get; set; }
        public string AutoMarca { get; set; }

        public string CodigoPrd { get; set; }
        public string CodigoPLU { get; set; }
        public string NombrePrd { get; set; }
        public string Referencia { get; set; }
        public string Categoria { get; set; }

        public string CodDepartamento { get; set; }
        public string NombreDepartamento { get; set; }

        public string CodGrupo { get; set; }
        public string NombreGrupo { get; set; }

        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Pasillo { get; set; }

        public bool IsActivo { get; set; }
        public bool IsDivisa { get; set; }
        public bool IsPesado { get; set; }
        public bool IsOferta { get; set; }

        public decimal TasaImpuesto { get; set; }
        public DateTime? OfertaDesde { get; set; }
        public DateTime? OfertaHasta { get; set; }
        public decimal OfertaPrecio { get; set; }
        public int DiasEmpaqueGarantia { get; set; }
        public DateTime FechaServidor { get; set; }

        public decimal Costo { get; set; }
        public decimal CostoUnidad { get; set; }
        public decimal CostoPromedio { get; set; }
        public decimal CostoPromedioUnidad { get; set; }
        public string Decimales { get; set; }

        public DataPrecio Precio_1 { get; set; }
        public DataPrecio Precio_2 { get; set; }
        public DataPrecio Precio_3 { get; set; }
        public DataPrecio Precio_4 { get; set; }
        public DataPrecio Precio_5 { get; set; }
    }

    public class DataPrecio
    {
        public decimal Neto { get; set; }
        public int Contenido { get; set; }
        public string Decimales { get; set; }
        public string Empaque { get; set; }
    }

    public class DataUsuario
    {
        public string UsuarioAuto { get; set; }
        public string UsuarioClave { get; set; }
        public string UsuarioCodigo { get; set; }
        public string UsuarioDescripcion { get; set; }
        public string UsuarioEstatus { get; set; }
        public string GrupoAuto { get; set; }
        public string GrupoDescripcion { get; set; }
    }

    public class DataUsuarioPermiso
    {
        public string CodigoFuncion { get; set; }
        public string Estatus { get; set; }
        public string Seguridad { get; set; }
        public string CodigoGrupo { get; set; }
    }

    public class DataPrdBarra
    {
        public string AutoPrd { get; set; }
        public string codigo { get; set; }
    }

    public class DataFiscal
    {
        public string Auto { get; set; }
        public string Nombre { get; set; }
        public decimal Tasa { get; set; }
    }

    public class DataDeposito
    {
        public string Auto { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
    }

    public class DataVendedor
    {
        public string Auto { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
    }

    public class DataMedioCobro
    {
        public string Auto { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
    }

    public class DataCobrador
    {
        public string Auto { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
    }

    public class DataTransporte
    {
        public string Auto { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
    }

    public class DataSerie
    {
        public string Auto { get; set; }
        public string Serie { get; set; }
        public string Control { get; set; }
        public int Correlativo { get; set; }
    }

    public class DataProductoConcepto
    {
        public string Auto { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
    }

    public class DataEmpresa
    {
        public string Auto { get; set; }
        public string Nombre { get; set; }
        public string CiRif { get; set; }
        public string DirFiscal { get; set; }
        public string Telefono { get; set; }
    }

    public class DataKardexResumen
    {
        public string autoProducto { get; set; }
        public string autoDeposito { get; set; }
        public decimal cnt { get; set; }
    }


    public partial class Provider : IPosOffLine.IProvider
    {

        public DtoLib.ResultadoEntidad<int> Productos_TotalItems()
        {
            var result = new DtoLib.ResultadoEntidad<int>();

            try
            {
                using (var cn = new MySqlConnection(_cnn2.ConnectionString))
                {
                    MySqlCommand comando = new MySqlCommand("select count(*) from productos");
                    comando.Connection = cn;
                    cn.Open();
                    string rt = comando.ExecuteScalar().ToString();
                    result.Entidad = int.Parse(rt);
                };
            }
            catch (MySqlException ex)
            {
                result.Mensaje = ex.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }

        public DtoLib.Resultado Servidor_Test()
        {
            var result = new DtoLib.ResultadoEntidad<int>();

            try
            {
                using (var cn = new MySqlConnection(_cnn2.ConnectionString))
                {
                    cn.Open();
                };
            }
            catch (MySqlException ex)
            {
                result.Mensaje = ex.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }

        public DtoLib.Resultado Servidor_ActualizarData()
        {
            var result = new DtoLib.Resultado();
            var list = new List<DataPrd>();
            var list2 = new List<DataPrdBarra>();
            var list3 = new List<DataUsuario>();
            var list3_1 = new List<DataUsuarioPermiso>();
            var list4 = new List<DataFiscal>();
            var list5 = new List<DataDeposito>();
            var list6 = new List<DataVendedor>();
            var list7 = new List<DataMedioCobro>();
            var list8 = new List<DataCobrador>();
            var list9 = new List<DataTransporte>();
            var listA = new List<DataSerie>();
            var listB = new List<DataProductoConcepto>();
            var listC = new List<DataEmpresa>();


            var exito = false;
            var factorCambio = 0.0m;
            var clave1 = "";
            var clave2 = "";
            var clave3 = "";

            var _etiquetarPrecioPorTipoNegocio = false;
            var _codigoSucursal = "";
            var _depositoAsignado = "";
            var _tarifaAsignada = "1";
            var _fechaServidor = DateTime.Now.Date;

            try
            {
                MySqlDataReader reader;
                using (var cn = new MySqlConnection(_cnn2.ConnectionString))
                {
                    var sql = "select p.*, d.codigo as codigoDepart, d.nombre as nombreDepart, " +
                        "g.codigo as codigoGrupo, g.nombre as nombreGrupo, m.nombre as nombreMarca, " +
                        "pm.nombre as pv1Nombre, pm.decimales as pv1Decimales, " +
                        "pm2.nombre as pv2Nombre, pm2.decimales as pv2Decimales, " +
                        "pm3.nombre as pv3Nombre, pm3.decimales as pv3Decimales, " +
                        "pm4.nombre as pv4Nombre, pm4.decimales as pv4Decimales, " +
                        "pm5.nombre as pv5Nombre, pm5.decimales as pv5Decimales " +
                        "from productos as p join empresa_departamentos as d on p.auto_departamento=d.auto " +
                        " join productos_grupo as g on p.auto_grupo=g.auto " +
                        " join productos_marca as m on p.auto_marca=m.auto " +
                        " join productos_medida as pm on p.auto_precio_1=pm.auto " +
                        " join productos_medida as pm2 on p.auto_precio_2=pm2.auto " +
                        " join productos_medida as pm3 on p.auto_precio_3=pm3.auto " +
                        " join productos_medida as pm4 on p.auto_precio_4=pm4.auto " +
                        " join productos_medida as pm5 on p.auto_precio_pto=pm5.auto " +
                        " join productos_deposito as dep on p.auto=dep.auto_producto and  dep.auto_deposito=@deposito ";
                    var sql2 = "select * from productos_alterno";

                    //var sql3 = "select u.*,g.nombre as nombreGrupo from usuarios as u " +
                    //    " join usuarios_grupo as g on u.auto_grupo=g.auto where g.auto='0000000004'";
                    //var sql3_1 = "select codigo_funcion, estatus, seguridad from usuarios_grupo_permisos as ugp " +
                    //    " where ugp.codigo_grupo='0000000004' and ugp.codigo_funcion like '0816%' ";

                    var sql3 = "select u.*,g.nombre as nombreGrupo from usuarios as u " +
                        " join usuarios_grupo as g on u.auto_grupo=g.auto";
                    var sql3_1 = "select codigo_funcion, estatus, seguridad, codigo_grupo from usuarios_grupo_permisos as ugp " +
                        " where ugp.codigo_funcion like '0816%' ";

                    var sql4 = "select * from empresa_tasas";
                    var sql5 = "select usuario from sistema_configuracion where codigo='GLOBAL48'";
                    var sql6 = "select auto,nombre,codigo from empresa_depositos";
                    var sql7 = "select auto,codigo,nombre from vendedores where estatus='Activo'";
                    var sql8 = "select auto,codigo,nombre from empresa_medios where estatus_cobro='1'";
                    var sql9 = "select auto,codigo,nombre from empresa_cobradores";
                    var sqlA = "select auto,codigo,nombre from empresa_transporte";
                    var sqlB = "select usuario from sistema_configuracion where codigo='GLOBAL17'";
                    var sqlC = "select usuario from sistema_configuracion where codigo='GLOBAL18'";
                    var sqlD = "select usuario from sistema_configuracion where codigo='GLOBAL19'";
                    var sqlE = "select auto, serie, control, correlativo from empresa_series_fiscales where estatus='Activo'";
                    var sqlF = "select usuario from sistema_configuracion where codigo='GLOBAL49'"; //PRECIO POR TIPO DE NEGOCIO
                    var sqlG = "select codigo_empresa from sistema"; //CODIGO SUCURSAL 
                    var sqlH = "select es.autoDepositoPrincipal as autoDeposito, eg.idPrecio as Tarifa " +
                        "from empresa_sucursal as es join empresa_grupo as eg on es.autoEmpresaGrupo=eg.Auto " +
                        "where codigo=@codigo"; //DEPOSITO, TARIFA PARA EL TIPO DE NEGOCIO INDICADO
                    var sqlI = "select auto,nombre,codigo from productos_conceptos";
                    var sqlJ = "select auto,nombre,rif,direccion,telefono from empresa where auto='0000000001'";


                    var sql1 = "select now()";
                    MySqlCommand comando1 = new MySqlCommand(sql1);
                    comando1.Connection = cn;
                    cn.Open();
                    var fechaServ = comando1.ExecuteScalar().ToString();
                    _fechaServidor = DateTime.Parse(fechaServ).Date;


                    var xsql = "select deposito_principal from sistema";
                    MySqlCommand xcomando = new MySqlCommand(xsql);
                    xcomando.Connection = cn;
                    var xdeposito= xcomando.ExecuteScalar().ToString();


                    MySqlCommand comando = new MySqlCommand(sql);
                    comando.Parameters.AddWithValue("@deposito", xdeposito);
                    comando.Connection = cn;
                    reader = comando.ExecuteReader();
                    while (reader.Read())
                    {
                        var nr = new DataPrd();

                        nr.Auto = reader.GetString("auto");
                        nr.AutoDepartamento = reader.GetString("auto_departamento");
                        nr.AutoGrupo = reader.GetString("auto_grupo");
                        nr.AutoSubGrupo = reader.GetString("auto_subgrupo");
                        nr.AutoMarca = reader.GetString("auto_marca");
                        nr.AutoTasaIva = reader.GetString("auto_tasa");

                        nr.CodigoPrd = reader.GetString("codigo");
                        nr.CodigoPLU = reader.GetString("plu");
                        nr.NombrePrd = reader.GetString("nombre");
                        nr.Referencia = reader.GetString("referencia");
                        nr.Categoria = reader.GetString("categoria");

                        nr.CodDepartamento = reader.GetString("codigoDepart");
                        nr.NombreDepartamento = reader.GetString("nombreDepart");
                        nr.CodGrupo = reader.GetString("codigoGrupo");
                        nr.NombreGrupo = reader.GetString("nombreGrupo");
                        nr.Marca = reader.GetString("nombreMarca");
                        nr.Modelo = reader.GetString("modelo");
                        nr.Pasillo = reader.GetString("lugar");

                        var isActivo = reader.GetString("estatus").Trim().ToUpper();
                        var isSuspendido = reader.GetString("estatus_cambio").Trim().ToUpper();
                        var isDivisa = reader.GetString("estatus_divisa").Trim().ToUpper();
                        var isOferta = reader.GetString("estatus_oferta").Trim().ToUpper();
                        var isPesado = reader.GetString("estatus_pesado").Trim().ToUpper();

                        if (isSuspendido == "1")
                        {
                            nr.IsActivo = false;
                        }
                        else 
                        {
                            nr.IsActivo = isActivo == "ACTIVO" ? true : false;
                        }
                        nr.IsDivisa = isDivisa == "1" ? true : false;
                        nr.IsOferta = isOferta == "1" ? true : false;
                        nr.IsPesado = isPesado == "1" ? true : false;

                        nr.TasaImpuesto = reader.GetDecimal("tasa");
                        nr.OfertaDesde = reader.GetMySqlDateTime("inicio").GetDateTime();
                        nr.OfertaHasta = reader.GetMySqlDateTime("fin").GetDateTime();
                        nr.OfertaPrecio = reader.GetDecimal("precio_oferta");
                        nr.DiasEmpaqueGarantia = reader.GetInt32("dias_garantia");
                        nr.FechaServidor = DateTime.Parse(fechaServ).Date;

                        nr.Costo = reader.GetDecimal("costo");
                        nr.CostoPromedio = reader.GetDecimal("costo_promedio");
                        nr.CostoPromedioUnidad = reader.GetDecimal("costo_promedio_und");
                        nr.CostoUnidad = reader.GetDecimal("costo_und");

                        var pv1 = new DataPrecio()
                        {
                            Contenido = reader.GetInt32("contenido_1"),
                            Decimales = reader.GetString("pv1Decimales"),
                            Empaque = reader.GetString("pv1Nombre"),
                            Neto = reader.GetDecimal("precio_1"),
                        };
                        var pv2 = new DataPrecio()
                        {
                            Contenido = reader.GetInt32("contenido_2"),
                            Decimales = reader.GetString("pv2Decimales"),
                            Empaque = reader.GetString("pv2Nombre"),
                            Neto = reader.GetDecimal("precio_2"),
                        };
                        var pv3 = new DataPrecio()
                        {
                            Contenido = reader.GetInt32("contenido_3"),
                            Decimales = reader.GetString("pv3Decimales"),
                            Empaque = reader.GetString("pv3Nombre"),
                            Neto = reader.GetDecimal("precio_3"),
                        };
                        var pv4 = new DataPrecio()
                        {
                            Contenido = reader.GetInt32("contenido_4"),
                            Decimales = reader.GetString("pv4Decimales"),
                            Empaque = reader.GetString("pv4Nombre"),
                            Neto = reader.GetDecimal("precio_4"),
                        };
                        var pv5 = new DataPrecio()
                        {
                            Contenido = reader.GetInt32("contenido_pto"),
                            Decimales = reader.GetString("pv5Decimales"),
                            Empaque = reader.GetString("pv5Nombre"),
                            Neto = reader.GetDecimal("precio_pto"),
                        };

                        nr.Precio_1 = pv1;
                        nr.Precio_2 = pv2;
                        nr.Precio_3 = pv3;
                        nr.Precio_4 = pv4;
                        nr.Precio_5 = pv5;

                        list.Add(nr);
                    }
                    reader.Close();

                    MySqlCommand comando2 = new MySqlCommand(sql2);
                    comando2.Connection = cn;
                    reader = comando2.ExecuteReader();
                    while (reader.Read())
                    {
                        var nr = new DataPrdBarra();

                        nr.AutoPrd = reader.GetString("auto_producto");
                        nr.codigo = reader.GetString("codigo_alterno");
                        list2.Add(nr);
                    }
                    reader.Close();

                    MySqlCommand comando3 = new MySqlCommand(sql3);
                    comando3.Connection = cn;
                    reader = comando3.ExecuteReader();
                    while (reader.Read())
                    {
                        var nr = new DataUsuario();
                        nr.UsuarioAuto = reader.GetString("auto");
                        nr.UsuarioClave = reader.GetString("clave");
                        nr.UsuarioCodigo = reader.GetString("codigo");
                        nr.UsuarioDescripcion = reader.GetString("nombre");
                        nr.UsuarioEstatus = reader.GetString("estatus");
                        nr.GrupoAuto = reader.GetString("auto_grupo");
                        nr.GrupoDescripcion = reader.GetString("nombreGrupo");
                        list3.Add(nr);
                    }
                    reader.Close();

                    MySqlCommand comando3_1 = new MySqlCommand(sql3_1);
                    comando3_1.Connection = cn;
                    reader = comando3_1.ExecuteReader();
                    while (reader.Read())
                    {
                        var nr = new DataUsuarioPermiso();
                        nr.CodigoFuncion = reader.GetString("codigo_funcion");
                        nr.Estatus = reader.GetString("estatus");
                        nr.Seguridad = reader.GetString("seguridad");
                        nr.CodigoGrupo= reader.GetString("codigo_grupo");
                        list3_1.Add(nr);
                    }
                    reader.Close();

                    MySqlCommand comando4 = new MySqlCommand(sql4);
                    comando4.Connection = cn;
                    reader = comando4.ExecuteReader();
                    while (reader.Read())
                    {
                        var nr = new DataFiscal();
                        nr.Auto = reader.GetString("auto");
                        nr.Nombre = reader.GetString("nombre");
                        nr.Tasa = reader.GetDecimal("tasa");
                        list4.Add(nr);
                    }
                    reader.Close();

                    MySqlCommand comando5 = new MySqlCommand(sql5);
                    comando5.Connection = cn;
                    var _cm5 = comando5.ExecuteScalar();
                    if (_cm5 != null)
                    {
                        var style = NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands;
                        //var culture = CultureInfo.CreateSpecificCulture("es-ES");
                        var culture = CultureInfo.CreateSpecificCulture("en-EN");
                        Decimal.TryParse(_cm5.ToString(), style, culture, out factorCambio);
                    }

                    MySqlCommand comando6 = new MySqlCommand(sql6);
                    comando6.Connection = cn;
                    reader = comando6.ExecuteReader();
                    while (reader.Read())
                    {
                        var nr = new DataDeposito();
                        nr.Auto = reader.GetString("auto");
                        nr.Codigo = reader.GetString("codigo");
                        nr.Descripcion = reader.GetString("nombre");
                        list5.Add(nr);
                    }
                    reader.Close();

                    MySqlCommand comando7 = new MySqlCommand(sql7);
                    comando7.Connection = cn;
                    reader = comando7.ExecuteReader();
                    while (reader.Read())
                    {
                        var nr = new DataVendedor();
                        nr.Auto = reader.GetString("auto");
                        nr.Codigo = reader.GetString("codigo");
                        nr.Nombre = reader.GetString("nombre");
                        list6.Add(nr);
                    }
                    reader.Close();

                    MySqlCommand comando8 = new MySqlCommand(sql8);
                    comando8.Connection = cn;
                    reader = comando8.ExecuteReader();
                    while (reader.Read())
                    {
                        var nr = new DataMedioCobro();
                        nr.Auto = reader.GetString("auto");
                        nr.Codigo = reader.GetString("codigo");
                        nr.Descripcion = reader.GetString("nombre");
                        list7.Add(nr);
                    }
                    reader.Close();

                    MySqlCommand comando9 = new MySqlCommand(sql9);
                    comando9.Connection = cn;
                    reader = comando9.ExecuteReader();
                    while (reader.Read())
                    {
                        var nr = new DataCobrador();
                        nr.Auto = reader.GetString("auto");
                        nr.Codigo = reader.GetString("codigo");
                        nr.Nombre = reader.GetString("nombre");
                        list8.Add(nr);
                    }
                    reader.Close();

                    MySqlCommand comandoA = new MySqlCommand(sqlA);
                    comandoA.Connection = cn;
                    reader = comandoA.ExecuteReader();
                    while (reader.Read())
                    {
                        var nr = new DataTransporte();
                        nr.Auto = reader.GetString("auto");
                        nr.Codigo = reader.GetString("codigo");
                        nr.Nombre = reader.GetString("nombre");
                        list9.Add(nr);
                    }
                    reader.Close();

                    MySqlCommand comandoB = new MySqlCommand(sqlB);
                    comandoB.Connection = cn;
                    var _cmB = comandoB.ExecuteScalar();
                    if (_cmB != null)
                    {
                        clave1 = _cmB.ToString();
                    }

                    MySqlCommand comandoC = new MySqlCommand(sqlC);
                    comandoC.Connection = cn;
                    var _cmC = comandoC.ExecuteScalar();
                    if (_cmC != null)
                    {
                        clave2 = _cmC.ToString();
                    }

                    MySqlCommand comandoD = new MySqlCommand(sqlD);
                    comandoD.Connection = cn;
                    var _cmD = comandoD.ExecuteScalar();
                    if (_cmD != null)
                    {
                        clave3 = _cmD.ToString();
                    }

                    MySqlCommand comandoE = new MySqlCommand(sqlE);
                    comandoE.Connection = cn;
                    reader = comandoE.ExecuteReader();
                    while (reader.Read())
                    {
                        var nr = new DataSerie();
                        nr.Auto = reader.GetString("auto");
                        nr.Serie = reader.GetString("serie");
                        nr.Control = reader.GetString("control");
                        nr.Correlativo = reader.GetInt32("correlativo");
                        listA.Add(nr);
                    }
                    reader.Close();

                    MySqlCommand comandoF = new MySqlCommand(sqlF);
                    comandoF.Connection = cn;
                    var xetiquetarPrecioPorTipoNegocio = comandoF.ExecuteScalar().ToString(); // SI,NO
                    if (xetiquetarPrecioPorTipoNegocio.Trim().ToUpper() == "SI")
                    {
                        _etiquetarPrecioPorTipoNegocio = true;
                    }

                    MySqlCommand comandoG = new MySqlCommand(sqlG);
                    comandoG.Connection = cn;
                    _codigoSucursal = comandoG.ExecuteScalar().ToString();

                    if (_etiquetarPrecioPorTipoNegocio)
                    {
                        MySqlCommand comandoH = new MySqlCommand(sqlH);
                        comandoH.Parameters.AddWithValue("@codigo", _codigoSucursal);
                        comandoH.Connection = cn;
                        MySqlDataReader dataReader = comandoH.ExecuteReader();
                        while (dataReader.Read())
                        {
                            _depositoAsignado = dataReader.GetString(0);
                            _tarifaAsignada = dataReader.GetString(1);
                        }
                        dataReader.Close();
                    }

                    MySqlCommand comandoI = new MySqlCommand(sqlI);
                    comandoI.Connection = cn;
                    reader = comandoI.ExecuteReader();
                    while (reader.Read())
                    {
                        var nr = new DataProductoConcepto();
                        nr.Auto = reader.GetString("auto");
                        nr.Codigo = reader.GetString("codigo");
                        nr.Nombre = reader.GetString("nombre");
                        listB.Add(nr);
                    }
                    reader.Close();


                    MySqlCommand comandoJ = new MySqlCommand(sqlJ);
                    comandoJ.Connection = cn;
                    reader = comandoJ.ExecuteReader();
                    while (reader.Read())
                    {
                        var nr = new DataEmpresa();
                        nr.Auto = reader.GetString("auto");
                        nr.Nombre = reader.GetString("nombre");
                        nr.CiRif = reader.GetString("rif");
                        nr.DirFiscal = reader.GetString("direccion");
                        nr.Telefono = reader.GetString("telefono");
                        listC.Add(nr);
                    }
                    reader.Close();

                    exito = true;
                };
            }
            catch (MySqlException ex)
            {
                result.Mensaje = ex.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            if (exito)
            {

                try
                {
                    using (var cnn = new LibEntitySqLitePosOffLine.LeonuxPosOffLineEntities(_cnn.ConnectionString))
                    {
                        using (var ts = cnn.Database.BeginTransaction())
                        {
                            var prdList = cnn.Producto.ToList();
                            var prdBarra = cnn.ProductoBarra.ToList();
                            var usuList = cnn.UsuarioGrupo.ToList();
                            var usuPermisoList = cnn.UsuarioPermiso.ToList();
                            var fiscalList = cnn.Fiscal.ToList();
                            var vendedorList = cnn.Vendedor.ToList();
                            var medioList = cnn.MedioCobro.ToList();
                            var depositoList = cnn.Deposito.ToList();
                            var cobradorList = cnn.Cobrador.ToList();
                            var transporteList = cnn.Transporte.ToList();
                            var serieList = cnn.Serie.ToList();
                            var movConceptoInvList = cnn.MovConceptoInv.ToList();
                            var empresaList = cnn.Empresa.ToList();


                            var sistema = cnn.Sistema.Find("0000000001");
                            if (factorCambio > 0)
                            {
                                sistema.factorCambio = factorCambio;
                                sistema.clave1 = clave1;
                                sistema.clave2 = clave2;
                                sistema.clave3 = clave3;
                                sistema.sucursalCodigo = _codigoSucursal;
                                sistema.autoDeposito = _depositoAsignado;
                                sistema.tarifaAsignada = _tarifaAsignada;
                                sistema.EtiquetarPrecioPorTipoNegocio = _etiquetarPrecioPorTipoNegocio ? "S" : "N";
                                sistema.fechaUltActualizacion = _fechaServidor.ToShortDateString();
                                cnn.SaveChanges();
                            }

                            /*
                            foreach (var r in list3_1)
                            {
                                switch (r.CodigoFuncion)
                                {
                                    case "0816010000": //DEVOLUCION
                                        var p1 = cnn.Permiso.Find(1);
                                        p1.requiereClave = "";
                                        if (r.Estatus.Trim().ToUpper() == "1")
                                        {
                                            p1.requiereClave = "S";
                                            if (r.Seguridad.Trim().ToUpper() == "NINGUNA")
                                            {
                                                p1.requiereClave = "N";
                                            }
                                        }
                                        cnn.SaveChanges();
                                        break;

                                    case "0816020000": // ANULAR VENTA 
                                        var p2 = cnn.Permiso.Find(2);
                                        p2.requiereClave = "";
                                        if (r.Estatus.Trim().ToUpper() == "1")
                                        {
                                            p2.requiereClave = "S";
                                            if (r.Seguridad.Trim().ToUpper() == "NINGUNA")
                                            {
                                                p2.requiereClave = "N";
                                            }
                                        }
                                        cnn.SaveChanges();
                                        break;

                                    case "0816030000": // SUMAR 
                                        var p3 = cnn.Permiso.Find(3);
                                        p3.requiereClave = "";
                                        if (r.Estatus.Trim().ToUpper() == "1")
                                        {
                                            p3.requiereClave = "S";
                                            if (r.Seguridad.Trim().ToUpper() == "NINGUNA")
                                            {
                                                p3.requiereClave = "N";
                                            }
                                        }
                                        cnn.SaveChanges();
                                        break;

                                    case "0816040000": // MULTIPLICAR
                                        var p4 = cnn.Permiso.Find(4);
                                        p4.requiereClave = "";
                                        if (r.Estatus.Trim().ToUpper() == "1")
                                        {
                                            p4.requiereClave = "S";
                                            if (r.Seguridad.Trim().ToUpper() == "NINGUNA")
                                            {
                                                p4.requiereClave = "N";
                                            }
                                        }
                                        cnn.SaveChanges();
                                        break;

                                    case "0816050000": // RESTAR
                                        var p5 = cnn.Permiso.Find(5);
                                        p5.requiereClave = "";
                                        if (r.Estatus.Trim().ToUpper() == "1")
                                        {
                                            p5.requiereClave = "S";
                                            if (r.Seguridad.Trim().ToUpper() == "NINGUNA")
                                            {
                                                p5.requiereClave = "N";
                                            }
                                        }
                                        cnn.SaveChanges();
                                        break;

                                    case "0816060000": // CTA PENDIENTE
                                        var p6 = cnn.Permiso.Find(6);
                                        p6.requiereClave = "";
                                        if (r.Estatus.Trim().ToUpper() == "1")
                                        {
                                            p6.requiereClave = "S";
                                            if (r.Seguridad.Trim().ToUpper() == "NINGUNA")
                                            {
                                                p6.requiereClave = "N";
                                            }
                                        }
                                        cnn.SaveChanges();
                                        break;

                                    case "0816070000": // ANULAR PENDIENTE
                                        var p7 = cnn.Permiso.Find(7);
                                        p7.requiereClave = "";
                                        if (r.Estatus.Trim().ToUpper() == "1")
                                        {
                                            p7.requiereClave = "S";
                                            if (r.Seguridad.Trim().ToUpper() == "NINGUNA")
                                            {
                                                p7.requiereClave = "N";
                                            }
                                        }
                                        cnn.SaveChanges();
                                        break;

                                    case "0816080000": // DSCTO GLOBAL
                                        var p8 = cnn.Permiso.Find(8);
                                        p8.requiereClave = "";
                                        if (r.Estatus.Trim().ToUpper() == "1")
                                        {
                                            p8.requiereClave = "S";
                                            if (r.Seguridad.Trim().ToUpper() == "NINGUNA")
                                            {
                                                p8.requiereClave = "N";
                                            }
                                        }
                                        cnn.SaveChanges();
                                        break;

                                    case "0816090000": // CTA CREDITO
                                        var p9 = cnn.Permiso.Find(9);
                                        p9.requiereClave = "";
                                        if (r.Estatus.Trim().ToUpper() == "1")
                                        {
                                            p9.requiereClave = "S";
                                            if (r.Seguridad.Trim().ToUpper() == "NINGUNA")
                                            {
                                                p9.requiereClave = "N";
                                            }
                                        }
                                        cnn.SaveChanges();
                                        break;

                                    case "0816100000": // ADM ANULAR DOCUMENTO 
                                        var p10 = cnn.Permiso.Find(10);
                                        p10.requiereClave = "";
                                        if (r.Estatus.Trim().ToUpper() == "1")
                                        {
                                            p10.requiereClave = "S";
                                            if (r.Seguridad.Trim().ToUpper() == "NINGUNA")
                                            {
                                                p10.requiereClave = "N";
                                            }
                                        }
                                        cnn.SaveChanges();
                                        break;

                                    case "0816110000": // ADM NOTA DE CREDITO 
                                        var p11 = cnn.Permiso.Find(11);
                                        p11.requiereClave = "";
                                        if (r.Estatus.Trim().ToUpper() == "1")
                                        {
                                            p11.requiereClave = "S";
                                            if (r.Seguridad.Trim().ToUpper() == "NINGUNA")
                                            {
                                                p11.requiereClave = "N";
                                            }
                                        }
                                        cnn.SaveChanges();
                                        break;

                                    case "0816120000": // ADM REIMPRIMIR DOCUMENTO 
                                        var p12 = cnn.Permiso.Find(12);
                                        p12.requiereClave = "";
                                        if (r.Estatus.Trim().ToUpper() == "1")
                                        {
                                            p12.requiereClave = "S";
                                            if (r.Seguridad.Trim().ToUpper() == "NINGUNA")
                                            {
                                                p12.requiereClave = "N";
                                            }
                                        }
                                        cnn.SaveChanges();
                                        break;
                                }
                            }
                            */

                            cnn.Configuration.AutoDetectChangesEnabled = false;
                            cnn.Producto.RemoveRange(prdList);
                            cnn.SaveChanges();
                            cnn.ProductoBarra.RemoveRange(prdBarra);
                            cnn.SaveChanges();
                            cnn.UsuarioGrupo.RemoveRange(usuList);
                            cnn.SaveChanges();
                            cnn.UsuarioPermiso.RemoveRange(usuPermisoList);
                            cnn.SaveChanges();
                            cnn.Fiscal.RemoveRange(fiscalList);
                            cnn.SaveChanges();
                            cnn.Vendedor.RemoveRange(vendedorList);
                            cnn.SaveChanges();
                            cnn.Deposito.RemoveRange(depositoList);
                            cnn.SaveChanges();
                            cnn.MedioCobro.RemoveRange(medioList);
                            cnn.SaveChanges();
                            cnn.Cobrador.RemoveRange(cobradorList);
                            cnn.SaveChanges();
                            cnn.Transporte.RemoveRange(transporteList);
                            cnn.SaveChanges();

                            //NO BORRAR LAS SERIES FISCALES, 
                            //MOTIVO: REINICIA LOS CONTADORES 
                            //cnn.Serie.RemoveRange(serieList);
                            //cnn.SaveChanges();

                            cnn.MovConceptoInv.RemoveRange(movConceptoInvList);
                            cnn.SaveChanges();
                            cnn.Empresa.RemoveRange(empresaList);
                            cnn.SaveChanges();


                            var listnr = new List<LibEntitySqLitePosOffLine.Producto>();
                            foreach (var r in list)
                            {
                                var desdeOf = "";
                                var hastaOf = "";
                                if (r.OfertaDesde.HasValue)
                                {
                                    desdeOf = r.OfertaDesde.Value.ToShortDateString();
                                }
                                if (r.OfertaHasta.HasValue)
                                {
                                    hastaOf = r.OfertaHasta.Value.ToShortDateString();
                                }
                                var nr = new LibEntitySqLitePosOffLine.Producto()
                                {
                                    auto = r.Auto,
                                    categoria = r.Categoria,
                                    codDepartamento = r.CodDepartamento,
                                    codGrupo = r.CodGrupo,
                                    codigoPrd = r.CodigoPrd,
                                    cont_1 = r.Precio_1.Contenido,
                                    cont_2 = r.Precio_2.Contenido,
                                    cont_3 = r.Precio_3.Contenido,
                                    cont_4 = r.Precio_4.Contenido,
                                    cont_5 = r.Precio_5.Contenido,
                                    costo = r.Costo,
                                    costoPromedio = r.CostoPromedio,
                                    costoPromedioUnidad = r.CostoPromedioUnidad,
                                    costoUnidad = r.CostoUnidad,
                                    dec_1 = r.Precio_1.Decimales,
                                    dec_2 = r.Precio_2.Decimales,
                                    dec_3 = r.Precio_3.Decimales,
                                    dec_4 = r.Precio_4.Decimales,
                                    dec_5 = r.Precio_5.Decimales,
                                    departamento = r.NombreDepartamento,
                                    descripcionPrd = "",
                                    dias_Empaque_Garantia = r.DiasEmpaqueGarantia,
                                    emp_1 = r.Precio_1.Empaque,
                                    emp_2 = r.Precio_2.Empaque,
                                    emp_3 = r.Precio_3.Empaque,
                                    emp_4 = r.Precio_4.Empaque,
                                    emp_5 = r.Precio_5.Empaque,
                                    grupo = r.NombreGrupo,
                                    isActivo = r.IsActivo ? 1 : 0,
                                    isDivisa = r.IsDivisa ? 1 : 0,
                                    isOferta = r.IsOferta ? 1 : 0,
                                    isPesado = r.IsPesado ? 1 : 0,
                                    marca = r.Marca,
                                    modelo = r.Modelo,
                                    nombrePrd = r.NombrePrd,
                                    ofertaDesde = desdeOf,
                                    ofertaHasta = hastaOf,
                                    ofertaprecio = r.OfertaPrecio,
                                    pasilo = r.Pasillo,
                                    plu = r.CodigoPLU,
                                    precio_1 = r.Precio_1.Neto,
                                    precio_2 = r.Precio_2.Neto,
                                    precio_3 = r.Precio_3.Neto,
                                    precio_4 = r.Precio_4.Neto,
                                    precio_5 = r.Precio_5.Neto,
                                    referencia = r.Referencia,
                                    tasaImpuesto = r.TasaImpuesto,
                                    autoDepartamento = r.AutoDepartamento,
                                    autoGrupo = r.AutoGrupo,
                                    autoSubGrupo = r.AutoSubGrupo,
                                    autoMarca = r.AutoMarca,
                                    autoTasaIva = r.AutoTasaIva,
                                };
                                listnr.Add(nr);
                            }
                            cnn.Producto.AddRange(listnr);
                            cnn.SaveChanges();


                            var listBarra = new List<LibEntitySqLitePosOffLine.ProductoBarra>();
                            foreach (var r in list2)
                            {
                                var nr = new LibEntitySqLitePosOffLine.ProductoBarra()
                                {
                                    autoProducto = r.AutoPrd,
                                    codigo = r.codigo,
                                };
                                listBarra.Add(nr);
                            }
                            cnn.ProductoBarra.AddRange(listBarra);
                            cnn.SaveChanges();


                            var listUsuario = new List<LibEntitySqLitePosOffLine.UsuarioGrupo>();
                            foreach (var r in list3)
                            {
                                var _estatus = "A";
                                if (r.UsuarioEstatus.Trim().ToUpper() == "INACTIVO") 
                                {
                                    _estatus = "I";
                                }
                                var nr = new LibEntitySqLitePosOffLine.UsuarioGrupo()
                                {
                                    autoGrupo = r.GrupoAuto,
                                    autoUsuario = r.UsuarioAuto,
                                    grupoDescripcion = r.GrupoDescripcion,
                                    usuarioClave = r.UsuarioClave,
                                    usuarioCodigo = r.UsuarioCodigo,
                                    usuarioDescripcion = r.UsuarioDescripcion,
                                    usuarioEstatus= _estatus 
                                };
                                listUsuario.Add(nr);
                            }
                            cnn.UsuarioGrupo.AddRange(listUsuario);
                            cnn.SaveChanges();


                            var listUsuarioPermiso = new List<LibEntitySqLitePosOffLine.UsuarioPermiso>();
                            foreach (var r in list3_1)
                            {
                                var _esActivo = 1;
                                var _requiereClave = 0;
                                if (r.Estatus.Trim().ToUpper() == "0") { _esActivo = 0; }
                                if (r.Seguridad.Trim().ToUpper() != "NINGUNA") { _requiereClave = 1; }

                                var nr = new LibEntitySqLitePosOffLine.UsuarioPermiso()
                                {
                                    codigoFuncion = r.CodigoFuncion,
                                    codigoGrupo = r.CodigoGrupo,
                                    esActivo = _esActivo,
                                    requiereClave = _requiereClave,
                                };
                                listUsuarioPermiso.Add(nr);
                            }
                            cnn.UsuarioPermiso.AddRange(listUsuarioPermiso);
                            cnn.SaveChanges();


                            var listFiscal = new List<LibEntitySqLitePosOffLine.Fiscal>();
                            foreach (var r in list4)
                            {
                                var nr = new LibEntitySqLitePosOffLine.Fiscal()
                                {
                                    auto = r.Auto,
                                    nombre = r.Nombre,
                                    tasa = r.Tasa,
                                };
                                listFiscal.Add(nr);
                            }
                            cnn.Fiscal.AddRange(listFiscal);
                            cnn.SaveChanges();


                            var listDeposito = new List<LibEntitySqLitePosOffLine.Deposito>();
                            foreach (var r in list5)
                            {
                                var nr = new LibEntitySqLitePosOffLine.Deposito()
                                {
                                    auto = r.Auto,
                                    nombre = r.Descripcion,
                                    codigo = r.Codigo,
                                };
                                listDeposito.Add(nr);
                            }
                            cnn.Deposito.AddRange(listDeposito);
                            cnn.SaveChanges();


                            var listVendedor = new List<LibEntitySqLitePosOffLine.Vendedor>();
                            foreach (var r in list6)
                            {
                                var nr = new LibEntitySqLitePosOffLine.Vendedor()
                                {
                                    auto = r.Auto,
                                    nombre = r.Nombre,
                                    codigo = r.Codigo,
                                };
                                listVendedor.Add(nr);
                            }
                            cnn.Vendedor.AddRange(listVendedor);
                            cnn.SaveChanges();


                            var listMedioCobro = new List<LibEntitySqLitePosOffLine.MedioCobro>();
                            foreach (var r in list7)
                            {
                                var nr = new LibEntitySqLitePosOffLine.MedioCobro()
                                {
                                    auto = r.Auto,
                                    descripcion = r.Descripcion,
                                    codigo = r.Codigo,
                                };
                                listMedioCobro.Add(nr);
                            }
                            cnn.MedioCobro.AddRange(listMedioCobro);
                            cnn.SaveChanges();


                            var listCobrador = new List<LibEntitySqLitePosOffLine.Cobrador>();
                            foreach (var r in list8)
                            {
                                var nr = new LibEntitySqLitePosOffLine.Cobrador()
                                {
                                    auto = r.Auto,
                                    nombre = r.Nombre,
                                    codigo = r.Codigo,
                                };
                                listCobrador.Add(nr);
                            }
                            cnn.Cobrador.AddRange(listCobrador);
                            cnn.SaveChanges();


                            var listTransporte = new List<LibEntitySqLitePosOffLine.Transporte>();
                            foreach (var r in list9)
                            {
                                var nr = new LibEntitySqLitePosOffLine.Transporte()
                                {
                                    auto = r.Auto,
                                    nombre = r.Nombre,
                                    codigo = r.Codigo,
                                };
                                listTransporte.Add(nr);
                            }
                            cnn.Transporte.AddRange(listTransporte);
                            cnn.SaveChanges();


                            //NOTA:VERIFICA SI EXISTE UNA NUEVA SERIE, DE LAS YA EXISTENTE
                            var listSerie = new List<LibEntitySqLitePosOffLine.Serie>();
                            foreach (var r in listA)
                            {
                                var nr = new LibEntitySqLitePosOffLine.Serie()
                                {
                                    auto = r.Auto,
                                    serie1 = r.Serie,
                                    control = r.Control,
                                    correlativo = r.Correlativo,
                                };
                                var xverifica = serieList.FirstOrDefault(f => f.auto == r.Auto);
                                if (xverifica == null) 
                                {
                                    listSerie.Add(nr);
                                }
                            }
                            cnn.Serie.AddRange(listSerie);
                            cnn.SaveChanges();


                            var listMovConcepto = new List<LibEntitySqLitePosOffLine.MovConceptoInv>();
                            foreach (var r in listB)
                            {
                                var nr = new LibEntitySqLitePosOffLine.MovConceptoInv()
                                {
                                    auto = r.Auto,
                                    codigo = r.Codigo,
                                    nombre = r.Nombre,
                                };
                                listMovConcepto.Add(nr);
                            }
                            cnn.MovConceptoInv.AddRange(listMovConcepto);
                            cnn.SaveChanges();


                            var listEmpresa = new List<LibEntitySqLitePosOffLine.Empresa>();
                            foreach (var r in listC)
                            {
                                var nr = new LibEntitySqLitePosOffLine.Empresa()
                                {
                                    auto = r.Auto,
                                    nombre = r.Nombre,
                                    cirif = r.CiRif,
                                    direccion = r.DirFiscal,
                                    telefono = r.Telefono,
                                };
                                listEmpresa.Add(nr);
                            }
                            cnn.Empresa.AddRange(listEmpresa);
                            cnn.SaveChanges();

                            cnn.Configuration.AutoDetectChangesEnabled = true;
                            ts.Commit();
                        }
                    }
                }
                catch (Exception e)
                {
                    result.Mensaje = e.Message;
                    result.Result = DtoLib.Enumerados.EnumResult.isError;
                }
            }

            return result;
        }

        public DtoLib.ResultadoEntidad<DtoLibPosOffLine.Servidor.RecogerDataEnviar.Ficha> Servidor_RecogerDataEnviar()
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibPosOffLine.Servidor.RecogerDataEnviar.Ficha>();

            try
            {
                using (var cnn = new LibEntitySqLitePosOffLine.LeonuxPosOffLineEntities(_cnn.ConnectionString))
                {
                    List<DtoLibPosOffLine.Servidor.RecogerDataEnviar.Serie> ListSeries = new List<DtoLibPosOffLine.Servidor.RecogerDataEnviar.Serie>();
                    var qserie = cnn.Serie.ToList();
                    foreach (var s in qserie)
                    {
                        var ns = new DtoLibPosOffLine.Servidor.RecogerDataEnviar.Serie()
                        {
                            Auto = s.auto,
                            Control = s.control,
                            Correlativo = (int)s.correlativo,
                            SerieNombre = s.serie1,
                        };
                        ListSeries.Add(ns);
                    }

                    List<DtoLibPosOffLine.Servidor.RecogerDataEnviar.Jornada> ListJornadas = new List<DtoLibPosOffLine.Servidor.RecogerDataEnviar.Jornada>();
                    var qJornada = cnn.Jornada.Where(w => w.estatus == "C" && w.estatusTransmitida == "").ToList();
                    if (qJornada != null)
                    {
                        if (qJornada.Count > 0)
                        {
                            foreach (var rg in qJornada)
                            {

                                var jornada = new DtoLibPosOffLine.Servidor.RecogerDataEnviar.Jornada();
                                jornada.Id = (int)rg.id;

                                var operador = cnn.Operador.FirstOrDefault(f => f.idJornada == rg.id);
                                if (operador == null)
                                {
                                    result.Mensaje = "OPERADOR NO ENCONTRADO PARA ESTA JORNADA";
                                    result.Result = DtoLib.Enumerados.EnumResult.isError;
                                    return result;
                                }

                                var operadorCierre = cnn.OperadorCierre.FirstOrDefault(f => f.idOperador == operador.id);
                                if (operadorCierre == null)
                                {
                                    result.Mensaje = "MOVIMIENTO DE CIERRE DEL OPERADOR NO ENCONTRADO";
                                    result.Result = DtoLib.Enumerados.EnumResult.isError;
                                    return result;
                                }

                                var MovCierre = new DtoLibPosOffLine.Servidor.RecogerDataEnviar.MovimientoCierre();
                                MovCierre.autoUsuario = operador.autoUsuario;
                                MovCierre.codigoUsuario = operador.codigoUsuario;
                                MovCierre.usuario = operador.usuario;
                                MovCierre.fecha = DateTime.Parse(operador.fechaCierre);
                                MovCierre.hora = operador.horaCierre;
                                MovCierre.prefijo = operador.prefijo;
                                MovCierre.diferencia = operadorCierre.diferencia;
                                MovCierre.efectivo = operadorCierre.efectivo;
                                MovCierre.divisa = operadorCierre.divisa;
                                MovCierre.tarjeta = operadorCierre.tarjeta;
                                MovCierre.otros = operadorCierre.otros;
                                MovCierre.devolucion = operadorCierre.devolucion;
                                MovCierre.subTotal = operadorCierre.subTotal;
                                MovCierre.total = operadorCierre.total;
                                MovCierre.mEfectivo = operadorCierre.mEfectivo;
                                MovCierre.mDivisa = operadorCierre.mDivisa;
                                MovCierre.mTarjeta = operadorCierre.mTarjeta;
                                MovCierre.mOtro = operadorCierre.mOtros;
                                MovCierre.mSubTotal = operadorCierre.mSubTotal;
                                MovCierre.mTotal = operadorCierre.mTotal;
                                MovCierre.firma = operadorCierre.firma;
                                MovCierre.mFirma = operadorCierre.mFirma;
                                //
                                MovCierre.cntDivisa = operadorCierre.cntDivisa;
                                MovCierre.cntDivisaUsu = operadorCierre.cntDivisaUsu;
                                MovCierre.cntDoc = (int)operadorCierre.cntDoc;
                                MovCierre.cntDocFac = (int)operadorCierre.cntDocFac;
                                MovCierre.cntDocNcr = (int)operadorCierre.cntDocNcr;
                                MovCierre.montoFac = operadorCierre.montoFac;
                                MovCierre.montoNcr = operadorCierre.montoNcr;

                                List<DtoLibPosOffLine.Servidor.RecogerDataEnviar.Documento> ListDocumentos = new List<DtoLibPosOffLine.Servidor.RecogerDataEnviar.Documento>();
                                var qVenta = cnn.Venta.Where(w => w.idJornada == rg.id).ToList();
                                if (qVenta != null)
                                {
                                    if (qVenta.Count > 0)
                                    {
                                        foreach (var rg2 in qVenta)
                                        {
                                            var idDocumento = rg2.id;
                                            var q = cnn.Venta.Find(idDocumento);
                                            var qdetalle = cnn.VentaDetalle.Where(f => f.idVenta == idDocumento).ToList();
                                            var qpago = cnn.VentaPago.Where(f => f.idVenta == idDocumento).ToList();

                                            if (qdetalle == null)
                                            {
                                                result.Mensaje = "DETALLES/ITEM NO ENCONTRADOS";
                                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                                return result;
                                            }
                                            if (qdetalle.Count == 0)
                                            {
                                                result.Mensaje = "DETALLES/ITEM NO ENCONTRADOS";
                                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                                return result;
                                            }

                                            if (q != null)
                                            {
                                                var s = q;
                                                var isActivo = s.estatusActivo == 1 ? true : false;
                                                var isCredito = s.esCredito.Trim().ToUpper() == "S" ? true : false;
                                                var tipoDocumento = DtoLibPosOffLine.Servidor.RecogerDataEnviar.Enumerados.EnumTipoDocumento.SinDefinir;
                                                switch (s.tipoDocumento)
                                                {
                                                    case 1:
                                                        tipoDocumento = DtoLibPosOffLine.Servidor.RecogerDataEnviar.Enumerados.EnumTipoDocumento.Factura;
                                                        break;
                                                    case 2:
                                                        tipoDocumento = DtoLibPosOffLine.Servidor.RecogerDataEnviar.Enumerados.EnumTipoDocumento.NotaDebito;
                                                        break;
                                                    case 3:
                                                        tipoDocumento = DtoLibPosOffLine.Servidor.RecogerDataEnviar.Enumerados.EnumTipoDocumento.NotaCredito;
                                                        break;
                                                }

                                                var nr = new DtoLibPosOffLine.Servidor.RecogerDataEnviar.Documento()
                                                {
                                                    Id = (int)s.id,
                                                    AnoRelacion = s.anoRelacion,
                                                    Aplica = s.aplica,
                                                    Base1 = s.base1,
                                                    Base2 = s.base2,
                                                    Base3 = s.base3,
                                                    CambioDar = s.cambioDar,
                                                    CargoMonto_1 = s.cargoMonto1,
                                                    CargoPorc_1 = s.cargoPorc_1,
                                                    CiRif = s.ciRif,
                                                    ClienteDirFiscal = s.dirFiscal,
                                                    ClienteId = (int)s.idCliente,
                                                    ClienteNombre = s.nombreRazonSocial,
                                                    ClienteTelefono = s.telefono,
                                                    CobradorAuto = s.autoCobrador,
                                                    CobradorCodigo = s.codigoCobrador,
                                                    CobradorNombre = s.cobrador,
                                                    CodigoSucursal = s.codigoSucursal,
                                                    Prefijo = s.prefijo,
                                                    Control = s.control,
                                                    DepositoAuto = s.autoDeposito,
                                                    DepositoCodigo = s.codigoDeposito,
                                                    DepositoNombre = s.deposito,
                                                    DesctoMonto_1 = s.descuentoMonto1,
                                                    DesctoMonto_2 = s.descuentoMonto2,
                                                    DesctoPorc_1 = s.descuentoPorc1,
                                                    DesctoPorc_2 = s.descuentoPorc2,
                                                    DocumentoNro = s.documento,
                                                    Estacion = s.estacion,
                                                    FactorCambio = s.factorCambio,
                                                    Fecha = DateTime.Parse(s.fecha),
                                                    Hora = s.hora,
                                                    Impuesto1 = s.impuesto1,
                                                    Impuesto2 = s.impuesto2,
                                                    Impuesto3 = s.impuesto3,
                                                    IsActiva = isActivo,
                                                    IsCredito = isCredito,
                                                    MesRelacion = s.mesRelacion,
                                                    MontoBase = s.montoBase,
                                                    MontoCostoVenta = s.montoCostoVenta,
                                                    MontoDivisa = s.montoDivisa,
                                                    MontoExento = s.montoExento,
                                                    MontoImpuesto = s.montoImpuesto,
                                                    MontoRecibido = s.montoRecibido,
                                                    MontoSubt = s.montoSubTotal,
                                                    MontoSubtImpuesto = s.montoSubTotalImpuesto,
                                                    MontoSubtNeto = s.montoSubTotalNeto,
                                                    MontoTotal = s.montoTotal,
                                                    MontoUtilidad = s.montoUtilidad,
                                                    MontoUtilidadPorc = s.montoUtilidadPorc,
                                                    MontoVentaNeta = s.montoVentaNeta,
                                                    Renglones = (int)s.renglones,
                                                    Serie = s.serie,
                                                    Signo = (int)s.signo,
                                                    TasaIva1 = s.tasaIva1,
                                                    TasaIva2 = s.tasaIva2,
                                                    TasaIva3 = s.tasaIva3,
                                                    TipoDocumento = tipoDocumento,
                                                    TranporteAuto = s.autoTransporte,
                                                    TranporteCodigo = s.codigoTransporte,
                                                    TranporteNombre = s.transporte,
                                                    UsuarioAuto = s.autoUsuario,
                                                    UsuarioCodigo = s.usuarioCodigo,
                                                    UsuarioNombre = s.usuario,
                                                    VendedorAuto = s.autoVendedor,
                                                    VendedorCodigo = s.codigoVendedor,
                                                    VendedorNombre = s.vendedor,
                                                    Tarifa = s.tarifa,
                                                    SaldoPendiente = s.saldoPendiente,
                                                    AutoConceptoMov = s.autoConceptoMov,
                                                    CodigoConceptoMov = s.codigoConceptoMov,
                                                    NombreConceptoMov = s.nombreConceptoMov,
                                                };

                                                var det = qdetalle.Select(t =>
                                                {
                                                    var esPesado = t.esPesado == 1 ? true : false;
                                                    var rg2Det = new DtoLibPosOffLine.Servidor.RecogerDataEnviar.DocumentoDetalle()
                                                    {
                                                        AutoDepartamento = t.autoDepartamento,
                                                        AutoGrupo = t.autoGrupo,
                                                        AutoProducto = t.autoProducto,
                                                        AutoSubGrupo = t.autoSubGrupo,
                                                        AutoTasa = t.autoTasa,
                                                        Cantidad = t.cantidad,
                                                        CantidadUnd = t.cantidadUnd,
                                                        Categoria = t.categoria,
                                                        CodigoProducto = t.codigoProducto,
                                                        CostoCompraUnd = t.costoCompraUnd,
                                                        CostoPromedioUnd = t.costoPromedioUnd,
                                                        CostoVenta = t.costoVenta,
                                                        Decimales = t.decimales,
                                                        DiaEmpaqueGarantia = (int)t.diaEmpaqueGarantia,
                                                        EmpaqueContenido = (int)t.empaqueContenido,
                                                        EmpaqueDescripcion = t.empaqueDescripcion,
                                                        EmpaqueCodigo = t.empaqueCodigo,
                                                        Id = (int)t.id,
                                                        MontoDscto_1 = t.montoDesc1,
                                                        MontoDscto_2 = t.montoDesc2,
                                                        MontoDscto_3 = t.montoDesc3,
                                                        MontoIva = t.montoIva,
                                                        NombreProducto = t.NombreProducto,
                                                        Notas = t.notas,
                                                        PorcDscto_1 = t.porctDesc1,
                                                        PorcDscto_2 = t.porctDesc2,
                                                        PorcDscto_3 = t.porctDesc3,
                                                        PrecioFinal = t.precioFinal,
                                                        PrecioItem = t.precioItem,
                                                        PrecioNeto = t.precioNeto,
                                                        PrecioSugerido = t.precioSugerido,
                                                        PrecioUnd = t.precioUnd,
                                                        Tarifa = t.tarifa,
                                                        TasaIva = t.tasaIva,
                                                        Total = t.total,
                                                        TotalDescuento = t.totalDescuento,
                                                        TotalNeto = t.totalNeto,
                                                        UtilidadMonto = t.utilidadMonto,
                                                        UtilidadPorct = t.utilidadPorct,
                                                        EsPesado = esPesado,
                                                        TipoIva = t.tipoIva,
                                                        CostoCompra = t.costoCompra,
                                                        CostoPromedio = t.costoPromedio,
                                                    };
                                                    return rg2Det;
                                                }).ToList();

                                                var pagos = qpago.Select(t =>
                                                {
                                                    var pag = new DtoLibPosOffLine.Servidor.RecogerDataEnviar.DocumentoPago()
                                                    {
                                                        AutoMedioCobro = t.autoMedioCobro,
                                                        CodigoMedioCobro = t.codioMedioCobro,
                                                        LoteNro = t.lote,
                                                        MedioCobro = t.descripMedioCobro,
                                                        MontoImporte = t.importe,
                                                        MontoRecibido = t.montoRecibido,
                                                        ReferenciaNro = t.referencia,
                                                        Tasa = t.tasa,
                                                        Id = (int)t.id,
                                                    };
                                                    return pag;
                                                }).ToList();

                                                nr.Detalles = det;
                                                nr.MetodosPago = pagos;
                                                ListDocumentos.Add(nr);
                                            }
                                        }
                                    }
                                }
                                jornada.Documentos = ListDocumentos;
                                jornada.Cierre = MovCierre;
                                ListJornadas.Add(jornada);
                            }
                        }
                    }

                    result.Entidad = new DtoLibPosOffLine.Servidor.RecogerDataEnviar.Ficha()
                    {
                        Jornadas = ListJornadas,
                        Series = ListSeries,
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

        public DtoLib.Resultado Servidor_EnviarData(DtoLibPosOffLine.Servidor.EnviarData.Ficha ficha)
        {
            var result = new DtoLib.Resultado();

            const string InsertarVenta = @"INSERT INTO ventas (auto, documento, fecha, fecha_vencimiento, razon_social, dir_fiscal, ci_rif, tipo, exento," +
                      "base1, base2, base3, impuesto1, impuesto2, impuesto3, base, impuesto, total, tasa1, tasa2, tasa3, nota, tasa_retencion_iva, " +
                      "tasa_retencion_islr, retencion_iva, retencion_islr, auto_cliente, codigo_cliente, mes_relacion, control, fecha_registro, orden_compra," +
                      "dias, descuento1, descuento2, cargos, descuento1p, descuento2p, cargosp, columna, estatus_anulado, aplica, comprobante_retencion," +
                      "subtotal_neto, telefono, factor_cambio, codigo_vendedor, vendedor, auto_vendedor, fecha_pedido, pedido, condicion_pago, usuario," +
                      "codigo_usuario, codigo_sucursal, hora, transporte, codigo_transporte, monto_divisa, despachado, dir_despacho, estacion, auto_recibo," +
                      "recibo, renglones, saldo_pendiente, ano_relacion, comprobante_retencion_islr, dias_validez, auto_usuario, auto_transporte, situacion," +
                      "signo, serie, tarifa, tipo_remision, documento_remision, auto_remision, documento_nombre, subtotal_impuesto, subtotal, auto_cxc, tipo_cliente," +
                      "planilla, expediente, anticipo_iva, terceros_iva, neto, costo, utilidad, utilidadp, documento_tipo, ci_titular, nombre_titular, ci_beneficiario," +
                      "nombre_beneficiario, clave, denominacion_fiscal, cambio, estatus_validado, cierre, fecha_retencion, estatus_cierre_contable, cierre_ftp) " +
                      "VALUES (?auto, ?documento, ?fecha, ?fecha_vencimiento, ?razon_social, ?dir_fiscal, ?ci_rif, ?tipo, ?exento," +
                      "?base1, ?base2, ?base3, ?impuesto1, ?impuesto2, ?impuesto3, ?base, ?impuesto, ?total, ?tasa1, ?tasa2, ?tasa3, ?nota, ?tasa_retencion_iva, " +
                      "?tasa_retencion_islr, ?retencion_iva, ?retencion_islr, ?auto_cliente, ?codigo_cliente, ?mes_relacion, ?control, ?fecha_registro, ?orden_compra," +
                      "?dias, ?descuento1, ?descuento2, ?cargos, ?descuento1p, ?descuento2p, ?cargosp, ?columna, ?estatus_anulado, ?aplica, ?comprobante_retencion," +
                      "?subtotal_neto, ?telefono, ?factor_cambio, ?codigo_vendedor, ?vendedor, ?auto_vendedor, ?fecha_pedido, ?pedido, ?condicion_pago, ?usuario," +
                      "?codigo_usuario, ?codigo_sucursal, ?hora, ?transporte, ?codigo_transporte, ?monto_divisa, ?despachado, ?dir_despacho, ?estacion, ?auto_recibo," +
                      "?recibo, ?renglones, ?saldo_pendiente, ?ano_relacion, ?comprobante_retencion_islr, ?dias_validez, ?auto_usuario, ?auto_transporte, ?situacion," +
                      "?signo, ?serie, ?tarifa, ?tipo_remision, ?documento_remision, ?auto_remision, ?documento_nombre, ?subtotal_impuesto, ?subtotal, ?auto_cxc, ?tipo_cliente," +
                      "?planilla, ?expediente, ?anticipo_iva, ?terceros_iva, ?neto, ?costo, ?utilidad, ?utilidadp, ?documento_tipo, ?ci_titular, ?nombre_titular, ?ci_beneficiario," +
                      "?nombre_beneficiario, ?clave, ?denominacion_fiscal, ?cambio, ?estatus_validado, ?cierre, ?fecha_retencion, ?estatus_cierre_contable, '')";

            const string InsertarVentaDetalle = @"INSERT INTO ventas_detalle (auto_documento, auto_producto, codigo, nombre, auto_departamento," +
                      "auto_grupo, auto_subgrupo, auto_deposito, cantidad, empaque, precio_neto, descuento1p, descuento2p, descuento3p, descuento1," +
                      "descuento2, descuento3, costo_venta, total_neto, tasa, impuesto, total, auto, estatus_anulado, fecha, tipo, deposito," +
                      "signo, precio_final, auto_cliente, decimales, contenido_empaque, cantidad_und, precio_und, costo_und, utilidad, utilidadp," +
                      "precio_item, estatus_garantia, estatus_serial, codigo_deposito, dias_garantia, detalle, precio_sugerido, auto_tasa, estatus_corte," +
                      "x, y, z, corte, categoria, cobranzap, ventasp, cobranzap_vendedor, ventasp_vendedor, cobranza, ventas, cobranza_vendedor," +
                      "ventas_vendedor, costo_promedio_und, costo_compra, estatus_checked, tarifa, total_descuento, codigo_vendedor, auto_vendedor, hora, cierre_ftp) " +
                      "VALUES (?auto_documento, ?auto_producto, ?codigo, ?nombre, ?auto_departamento," +
                      "?auto_grupo, ?auto_subgrupo, ?auto_deposito, ?cantidad, ?empaque, ?precio_neto, ?descuento1p, ?descuento2p, ?descuento3p, ?descuento1," +
                      "?descuento2, ?descuento3, ?costo_venta, ?total_neto, ?tasa, ?impuesto, ?total, ?auto, ?estatus_anulado, ?fecha, ?tipo, ?deposito," +
                      "?signo, ?precio_final, ?auto_cliente, ?decimales, ?contenido_empaque, ?cantidad_und, ?precio_und, ?costo_und, ?utilidad, ?utilidadp," +
                      "?precio_item, ?estatus_garantia, ?estatus_serial, ?codigo_deposito, ?dias_garantia, ?detalle, ?precio_sugerido, ?auto_tasa, ?estatus_corte," +
                      "?x, ?y, ?z, ?corte, ?categoria, ?cobranzap, ?ventasp, ?cobranzap_vendedor, ?ventasp_vendedor, ?cobranza, ?ventas, ?cobranza_vendedor," +
                      "?ventas_vendedor, ?costo_promedio_und, ?costo_compra, ?estatus_checked, ?tarifa, ?total_descuento, ?codigo_vendedor, ?auto_vendedor, ?hora, '')";

            const string InsertarProductoKardex = @"INSERT INTO productos_kardex (auto_producto, total , auto_deposito , auto_concepto , " +
                      "auto_documento, fecha , hora , documento , modulo , entidad , signo , cantidad , cantidad_bono , cantidad_und , costo_und ," +
                      "estatus_anulado , nota , precio_und , codigo , siglas , codigo_sucursal, cierre_ftp, codigo_deposito, nombre_deposito, "+
                      "codigo_concepto, nombre_concepto) " +
                      "VALUES (?auto_producto, ?total , ?auto_deposito , ?auto_concepto , " +
                      "?auto_documento, ?fecha , ?hora , ?documento , ?modulo , ?entidad , ?signo , ?cantidad , ?cantidad_bono , ?cantidad_und , ?costo_und ," +
                      "?estatus_anulado , ?nota , ?precio_und , ?codigo , ?siglas , ?codigo_sucursal, ?cierre_ftp, ?codigo_deposito, ?nombre_deposito, "+
                      "?codigo_concepto, ?nombre_concepto)";

            const string UpdateProductoDeposito = @"UPDATE productos_deposito set fisica=fisica-?cantidadUnd, disponible=disponible-?cantidadUnd " +
                      "where auto_producto=?autoProducto and auto_deposito=?autoDeposito";

            const string InsertarCxC = @"INSERT INTO cxc (auto , c_cobranza , c_cobranzap , fecha , tipo_documento , documento ," +
                        "fecha_vencimiento , nota , importe , acumulado , auto_cliente , cliente , ci_rif , codigo_cliente , " +
                        "estatus_cancelado , resta , estatus_anulado , auto_documento , numero , auto_agencia , agencia , signo , " +
                        "auto_vendedor , c_departamento , c_ventas , c_ventasp , serie , importe_neto , dias , castigop, cierre_ftp) " +
                        "VALUES (?auto , ?c_cobranza , ?c_cobranzap , ?fecha , ?tipo_documento , ?documento ," +
                        "?fecha_vencimiento , ?nota , ?importe , ?acumulado , ?auto_cliente , ?cliente , ?ci_rif , ?codigo_cliente , " +
                        "?estatus_cancelado , ?resta , ?estatus_anulado , ?auto_documento , ?numero , ?auto_agencia , ?agencia , ?signo , " +
                        "?auto_vendedor , ?c_departamento , ?c_ventas , ?c_ventasp , ?serie , ?importe_neto , ?dias , ?castigop, '')";

            const string InsertarCxCRecibo = @"INSERT INTO cxc_recibos (auto , documento , fecha , auto_usuario , importe , usuario , " +
                        "monto_recibido , cobrador , auto_cliente , cliente , ci_rif , codigo , estatus_anulado , direccion , telefono , " +
                        "auto_cobrador , anticipos , cambio , nota , codigo_cobrador , auto_cxc , retenciones , descuentos , hora , cierre, cierre_ftp) " +
                        "VALUES (?auto , ?documento , ?fecha , ?auto_usuario , ?importe , ?usuario , " +
                        "?monto_recibido , ?cobrador , ?auto_cliente , ?cliente , ?ci_rif , ?codigo , ?estatus_anulado , ?direccion , ?telefono , " +
                        "?auto_cobrador , ?anticipos , ?cambio , ?nota , ?codigo_cobrador , ?auto_cxc , ?retenciones , ?descuentos , ?hora , ?cierre, '')";

            const string InsertarCxCDocumento = @"INSERT INTO cxc_documentos (id  , fecha , tipo_documento , documento , importe , " +
                        "operacion , auto_cxc , auto_cxc_pago , auto_cxc_recibo , numero_recibo , fecha_recepcion , dias , " +
                        "castigop , comisionp, cierre_ftp) " +
                        "VALUES ( ?id  , ?fecha , ?tipo_documento , ?documento , ?importe , " +
                        "?operacion , ?auto_cxc , ?auto_cxc_pago , ?auto_cxc_recibo , ?numero_recibo , ?fecha_recepcion , ?dias , " +
                        "?castigop , ?comisionp, '')";

            const string InsertarCxCMedioPago = @"INSERT INTO cxc_medio_pago (auto_recibo , auto_medio_pago , auto_agencia , " +
                        "medio , codigo , monto_recibido , fecha , estatus_anulado , numero , agencia , auto_usuario , lote , " +
                        "referencia , auto_cobrador , cierre , fecha_agencia, cierre_ftp) " +
                        "VALUES (?auto_recibo , ?auto_medio_pago , ?auto_agencia , " +
                        "?medio , ?codigo , ?monto_recibido , ?fecha , ?estatus_anulado , ?numero , ?agencia , ?auto_usuario , ?lote , " +
                        "?referencia , ?auto_cobrador , ?cierre , ?fecha_agencia, '')";

            const string InsertarPosJornadas = @"INSERT INTO pos_jornadas (id, fecha, estatus_cierre, cierre_ftp) " +
                        "VALUES (NULL, ?fecha, '0', '')";

            const string InsertarPosArqueo = @"INSERT INTO pos_arqueo (auto_cierre, auto_usuario, codigo, usuario, fecha, hora, " +
                        "diferencia, efectivo, cheque, debito, credito, ticket, firma, retiro, otros, devolucion, subtotal, cobranza, " +
                        "total, mefectivo, mcheque, mbanco1, mbanco2, mbanco3, mbanco4, mtarjeta, mticket, mtrans, mfirma, motros, " +
                        "mgastos, mretiro, mretenciones, msubtotal, mtotal, cierre_ftp, cnt_divisa, cnt_divisa_usuario, "+
                        "cntDoc, cntDocFac, cntDocNcr, montoFac, montoNcr) " +
                        "VALUES (?auto_cierre, ?auto_usuario, ?codigo, ?usuario, ?fecha, ?hora, " +
                        "?diferencia, ?efectivo, ?cheque, ?debito, ?credito, ?ticket, ?firma, ?retiro, ?otros, ?devolucion, ?subtotal, ?cobranza, " +
                        "?total, ?mefectivo, ?mcheque, ?mbanco1, ?mbanco2, ?mbanco3, ?mbanco4, ?mtarjeta, ?mticket, ?mtrans, ?mfirma, ?motros, " +
                        "?mgastos, ?mretiro, ?mretenciones, ?msubtotal, ?mtotal, ?cierre_ftp, ?cnt_divisa, ?cnt_divisa_usuario, "+
                        "?cntDoc, ?cntDocFac, ?cntDocNcr, ?montoFac, ?montoNcr)";

            try
            {
                using (var cn = new MySqlConnection(_cnn2.ConnectionString))
                {
                    cn.Open();
                    MySqlTransaction tr = null;

                    try
                    {
                        tr = cn.BeginTransaction();

                        var sql0 = "select a_ventas from sistema_contadores";
                        MySqlCommand comando1 = new MySqlCommand(sql0, cn, tr);
                        var aVentaObj = comando1.ExecuteScalar();
                        if (aVentaObj == null)
                        {
                            result.Mensaje = "PROBLEMA AL LEER TABLA CONTADORES AUTOMATICO DE VENTAS";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }

                        sql0 = "select a_cxc from sistema_contadores";
                        comando1 = new MySqlCommand(sql0, cn, tr);
                        var aCxCObj = comando1.ExecuteScalar();
                        if (aCxCObj == null)
                        {
                            result.Mensaje = "PROBLEMA AL LEER TABLA CONTADORES AUTOMATICO DE CXC";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        var aCxC = (int)aCxCObj;

                        sql0 = "select a_cxc_recibo from sistema_contadores";
                        comando1 = new MySqlCommand(sql0, cn, tr);
                        var aCxCReciboObj = comando1.ExecuteScalar();
                        if (aCxCReciboObj == null)
                        {
                            result.Mensaje = "PROBLEMA AL LEER TABLA CONTADORES AUTOMATICO DE CXC RECIBO";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }

                        sql0 = "select a_cxc_recibo_numero from sistema_contadores";
                        comando1 = new MySqlCommand(sql0, cn, tr);
                        var aCxCReciboNumeroObj = comando1.ExecuteScalar();
                        if (aCxCReciboNumeroObj == null)
                        {
                            result.Mensaje = "PROBLEMA AL LEER TABLA CONTADORES AUTOMATICO DE RECIBO CXC NUMERO";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }

                        sql0 = "select a_cierre from sistema_contadores";
                        comando1 = new MySqlCommand(sql0, cn, tr);
                        var aCierreObj = comando1.ExecuteScalar();
                        if (aCierreObj == null)
                        {
                            result.Mensaje = "PROBLEMA AL LEER TABLA CONTADORES AUTOMATICO DE CIERRE";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }

                        var aCierre = (int)aCierreObj;
                        var aVenta = (int)aVentaObj;
                        var aCxCRecibo = (int)aCxCReciboObj;
                        var aCxCReciboNumero = (int)aCxCReciboNumeroObj;


                        var sqlVenta = InsertarVenta;
                        comando1 = new MySqlCommand(sqlVenta, cn, tr);

                        var sqlVentaUpdate = "update ventas set auto_recibo=?AutoRecibo, recibo=?Recibo where auto=?Auto";
                        var comandoVtaUpdate = new MySqlCommand(sqlVentaUpdate, cn, tr);

                        var sqlVentaDetalle = InsertarVentaDetalle;
                        var comando2 = new MySqlCommand(sqlVentaDetalle, cn, tr);

                        var sqlMovKardex = InsertarProductoKardex;
                        var comando3 = new MySqlCommand(sqlMovKardex, cn, tr);

                        var sqlDeposito = UpdateProductoDeposito;
                        var comando4 = new MySqlCommand(sqlDeposito, cn, tr);

                        var sqlPosJornadaA = "select 1 from pos_jornadas where fecha=?fecha";
                        var comando5A = new MySqlCommand(sqlPosJornadaA, cn, tr);
                        var sqlPosJornadaB = InsertarPosJornadas;
                        var comando5B = new MySqlCommand(sqlPosJornadaB, cn, tr);

                        var sqlCxC = InsertarCxC;
                        var comandoCxC = new MySqlCommand(sqlCxC, cn, tr);

                        var sqlCxCRecibo = InsertarCxCRecibo;
                        var comandoCxCRecibo = new MySqlCommand(sqlCxCRecibo, cn, tr);

                        var sqlCxCDocumento = InsertarCxCDocumento;
                        var comandoCxCDocumento = new MySqlCommand(sqlCxCDocumento, cn, tr);

                        var sqlCxCMedioPago = InsertarCxCMedioPago;
                        var comandoCxCMedioPago = new MySqlCommand(sqlCxCMedioPago, cn, tr);

                        var sqlPosArqueo = InsertarPosArqueo;
                        var comandoPosArqueo = new MySqlCommand(sqlPosArqueo, cn, tr);


                        foreach (var f in ficha.FechasMov)
                        {
                            comando5A.Parameters.Clear();
                            comando5A.Parameters.AddWithValue("?fecha", f.Date);
                            var rt = comando5A.ExecuteScalar();
                            if (rt == null)
                            {
                                comando5B.Parameters.Clear();
                                comando5B.Parameters.AddWithValue("?fecha", f.Date);
                                var rt2 = comando5B.ExecuteNonQuery();
                                if (rt2 == 0)
                                {
                                    result.Mensaje = "PROBLEMA AL INSERTAR REGISTRO POS JORNADAS";
                                    result.Result = DtoLib.Enumerados.EnumResult.isError;
                                    return result;
                                }
                            }
                        }

                        foreach (var mv in ficha.Movimientos)
                        {
                            aCierre += 1;
                            var autoCierre = mv.Prefijo + aCierre.ToString().Trim().PadLeft(6, '0');

                            comandoPosArqueo.Parameters.Clear();
                            comandoPosArqueo.Parameters.AddWithValue("?auto_cierre", autoCierre);
                            comandoPosArqueo.Parameters.AddWithValue("?auto_usuario", mv.Cierre.autoUsuario);
                            comandoPosArqueo.Parameters.AddWithValue("?codigo", mv.Cierre.codigoUsuario);
                            comandoPosArqueo.Parameters.AddWithValue("?usuario", mv.Cierre.usuario);
                            comandoPosArqueo.Parameters.AddWithValue("?fecha", mv.Cierre.fecha);
                            comandoPosArqueo.Parameters.AddWithValue("?hora", mv.Cierre.hora);
                            comandoPosArqueo.Parameters.AddWithValue("?diferencia", mv.Cierre.diferencia);
                            comandoPosArqueo.Parameters.AddWithValue("?efectivo", mv.Cierre.efectivo);
                            comandoPosArqueo.Parameters.AddWithValue("?cheque", mv.Cierre.divisa);
                            comandoPosArqueo.Parameters.AddWithValue("?debito", mv.Cierre.debito);
                            comandoPosArqueo.Parameters.AddWithValue("?credito", 0.0m);
                            comandoPosArqueo.Parameters.AddWithValue("?ticket", 0.0m);
                            comandoPosArqueo.Parameters.AddWithValue("?firma", mv.Cierre.firma);
                            comandoPosArqueo.Parameters.AddWithValue("?retiro", 0.0m);
                            comandoPosArqueo.Parameters.AddWithValue("?otros", mv.Cierre.otros);
                            comandoPosArqueo.Parameters.AddWithValue("?devolucion", mv.Cierre.devolucion);
                            comandoPosArqueo.Parameters.AddWithValue("?subtotal", mv.Cierre.subTotal);
                            comandoPosArqueo.Parameters.AddWithValue("?cobranza", 0.0m);
                            comandoPosArqueo.Parameters.AddWithValue("?total", mv.Cierre.total);
                            comandoPosArqueo.Parameters.AddWithValue("?mefectivo", mv.Cierre.mfectivo);
                            comandoPosArqueo.Parameters.AddWithValue("?mcheque", mv.Cierre.mdivisa);
                            comandoPosArqueo.Parameters.AddWithValue("?mbanco1", 0.0m);
                            comandoPosArqueo.Parameters.AddWithValue("?mbanco2", 0.0m);
                            comandoPosArqueo.Parameters.AddWithValue("?mbanco3", 0.0m);
                            comandoPosArqueo.Parameters.AddWithValue("?mbanco4", 0.0m);
                            comandoPosArqueo.Parameters.AddWithValue("?mtarjeta", mv.Cierre.mdebito);
                            comandoPosArqueo.Parameters.AddWithValue("?mticket", 0.0m);
                            comandoPosArqueo.Parameters.AddWithValue("?mtrans", 0.0m);
                            comandoPosArqueo.Parameters.AddWithValue("?mfirma", mv.Cierre.mfirma);
                            comandoPosArqueo.Parameters.AddWithValue("?motros", mv.Cierre.motros);
                            comandoPosArqueo.Parameters.AddWithValue("?mgastos", 0.0m);
                            comandoPosArqueo.Parameters.AddWithValue("?mretiro", 0.0m);
                            comandoPosArqueo.Parameters.AddWithValue("?mretenciones", 0.0m);
                            comandoPosArqueo.Parameters.AddWithValue("?msubtotal", mv.Cierre.msubtotal);
                            comandoPosArqueo.Parameters.AddWithValue("?mtotal", mv.Cierre.mtotal);
                            comandoPosArqueo.Parameters.AddWithValue("?cierre_ftp", "");
                            comandoPosArqueo.Parameters.AddWithValue("?cnt_divisa", mv.Cierre.cntDivisa);
                            comandoPosArqueo.Parameters.AddWithValue("?cnt_divisa_usuario", mv.Cierre.cntDivisaUsuario);
                            comandoPosArqueo.Parameters.AddWithValue("?cntDoc", mv.Cierre.cntDoc);
                            comandoPosArqueo.Parameters.AddWithValue("?cntDocFac", mv.Cierre.cntDocFac);
                            comandoPosArqueo.Parameters.AddWithValue("?cntDocNcr", mv.Cierre.cntDocNcr);
                            comandoPosArqueo.Parameters.AddWithValue("?montoFac", mv.Cierre.montoFac);
                            comandoPosArqueo.Parameters.AddWithValue("?montoNcr", mv.Cierre.montoNcr);
                            var rtPosArqueo = comandoPosArqueo.ExecuteNonQuery();
                            if (rtPosArqueo == 0)
                            {
                                result.Mensaje = "PROBLEMA AL INSERTAR REGISTRO POS ARQUEO";
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                return result;
                            }

                            foreach (var v in mv.Documentos)
                            {
                                aVenta += 1;
                                aCxC += 1;

                                //var codSucursal = v.Prefijo.Trim().PadLeft(4, '0');
                                var codSucursal = mv.Prefijo;
                                var autoVenta = codSucursal + aVenta.ToString().Trim().PadLeft(6, '0');
                                var autoCxC = codSucursal + aCxC.ToString().Trim().PadLeft(6, '0');

                                comando1.Parameters.Clear();
                                comando1.Parameters.AddWithValue("?auto", autoVenta);
                                comando1.Parameters.AddWithValue("?documento", v.DocumentoNro);
                                comando1.Parameters.AddWithValue("?fecha", v.Fecha);
                                comando1.Parameters.AddWithValue("?fecha_vencimiento", v.FechaVencimiento);
                                comando1.Parameters.AddWithValue("?razon_social", v.RazonSocial);
                                comando1.Parameters.AddWithValue("?dir_fiscal", v.DirFiscal);
                                comando1.Parameters.AddWithValue("?ci_rif", v.CiRif);
                                comando1.Parameters.AddWithValue("?tipo", v.Tipo);
                                comando1.Parameters.AddWithValue("?exento", v.Exento);
                                comando1.Parameters.AddWithValue("?base1", v.Base1);
                                comando1.Parameters.AddWithValue("?base2", v.Base2);
                                comando1.Parameters.AddWithValue("?base3", v.Base3);
                                comando1.Parameters.AddWithValue("?impuesto1", v.Impuesto1);
                                comando1.Parameters.AddWithValue("?impuesto2", v.Impuesto2);
                                comando1.Parameters.AddWithValue("?impuesto3", v.Impuesto3);
                                comando1.Parameters.AddWithValue("?base", v.MBase);
                                comando1.Parameters.AddWithValue("?impuesto", v.Impuesto);
                                comando1.Parameters.AddWithValue("?total", v.Total);
                                comando1.Parameters.AddWithValue("?tasa1", v.Tasa1);
                                comando1.Parameters.AddWithValue("?tasa2", v.Tasa2);
                                comando1.Parameters.AddWithValue("?tasa3", v.Tasa3);
                                comando1.Parameters.AddWithValue("?nota", v.Nota);
                                comando1.Parameters.AddWithValue("?tasa_retencion_iva", v.TasaRetencionIva);
                                comando1.Parameters.AddWithValue("?tasa_retencion_islr", v.TasaRetencionIslr);
                                comando1.Parameters.AddWithValue("?retencion_iva", v.RetencionIva);
                                comando1.Parameters.AddWithValue("?retencion_islr", v.RetencionIslr);
                                comando1.Parameters.AddWithValue("?auto_cliente", v.AutoCliente);
                                comando1.Parameters.AddWithValue("?codigo_cliente", v.CodigoCliente);
                                comando1.Parameters.AddWithValue("?mes_relacion", v.MesRelacion);
                                comando1.Parameters.AddWithValue("?control", v.Control);
                                comando1.Parameters.AddWithValue("?fecha_registro", v.FechaRegistro);
                                comando1.Parameters.AddWithValue("?orden_compra", v.OrdenCompra);
                                comando1.Parameters.AddWithValue("?dias", v.Dias);
                                comando1.Parameters.AddWithValue("?descuento1", v.Descuento1);
                                comando1.Parameters.AddWithValue("?descuento2", v.Descuento2);
                                comando1.Parameters.AddWithValue("?cargos", v.Cargos);
                                comando1.Parameters.AddWithValue("?descuento1p", v.Descuento1p);
                                comando1.Parameters.AddWithValue("?descuento2p", v.Descuento2p);
                                comando1.Parameters.AddWithValue("?cargosp", v.Cargosp);
                                comando1.Parameters.AddWithValue("?columna", v.Columna);
                                comando1.Parameters.AddWithValue("?estatus_anulado", v.EstatusAnulado);
                                comando1.Parameters.AddWithValue("?aplica", v.Aplica);
                                comando1.Parameters.AddWithValue("?comprobante_retencion", v.ComprobanteRetencion);
                                comando1.Parameters.AddWithValue("?subtotal_neto", v.SubTotalNeto);
                                comando1.Parameters.AddWithValue("?telefono", v.Telefono);
                                comando1.Parameters.AddWithValue("?factor_cambio", v.FactorCambio);
                                comando1.Parameters.AddWithValue("?codigo_vendedor", v.CodigoVendedor);
                                comando1.Parameters.AddWithValue("?vendedor", v.Vendedor);
                                comando1.Parameters.AddWithValue("?auto_vendedor", v.AutoVendedor);
                                comando1.Parameters.AddWithValue("?fecha_pedido", v.FechaPedido);
                                comando1.Parameters.AddWithValue("?pedido", v.Pedido);
                                comando1.Parameters.AddWithValue("?condicion_pago", v.CondicionPago);
                                comando1.Parameters.AddWithValue("?usuario", v.Usuario);
                                comando1.Parameters.AddWithValue("?codigo_usuario", v.CodigoUsuario);
                                comando1.Parameters.AddWithValue("?codigo_sucursal", v.CodigoSucursal);
                                comando1.Parameters.AddWithValue("?hora", v.Hora);
                                comando1.Parameters.AddWithValue("?transporte", v.Transporte);
                                comando1.Parameters.AddWithValue("?codigo_transporte", v.CodigoTransporte);
                                comando1.Parameters.AddWithValue("?monto_divisa", v.MontoDivisa);
                                comando1.Parameters.AddWithValue("?despachado", v.Despachado);
                                comando1.Parameters.AddWithValue("?dir_despacho", v.DirDespacho);
                                comando1.Parameters.AddWithValue("?estacion", v.Estacion);
                                comando1.Parameters.AddWithValue("?auto_recibo", v.AutoRecibo);
                                comando1.Parameters.AddWithValue("?recibo", v.Recibo);
                                comando1.Parameters.AddWithValue("?renglones", v.Renglones);
                                comando1.Parameters.AddWithValue("?saldo_pendiente", v.SaldoPendiente);
                                comando1.Parameters.AddWithValue("?ano_relacion", v.AnoRelacion);
                                comando1.Parameters.AddWithValue("?comprobante_retencion_islr", v.ComprobanteRetencionIslr);
                                comando1.Parameters.AddWithValue("?dias_validez", v.DiasValidez);
                                comando1.Parameters.AddWithValue("?auto_usuario", v.AutoUsuario);
                                comando1.Parameters.AddWithValue("?auto_transporte", v.AutoTransporte);
                                comando1.Parameters.AddWithValue("?situacion", v.Situacion);
                                comando1.Parameters.AddWithValue("?signo", v.Signo);
                                comando1.Parameters.AddWithValue("?serie", v.Serie);
                                comando1.Parameters.AddWithValue("?tarifa", v.Tarifa);
                                comando1.Parameters.AddWithValue("?tipo_remision", v.TipoRemision);
                                comando1.Parameters.AddWithValue("?documento_remision", v.DocumentoRemision);
                                comando1.Parameters.AddWithValue("?auto_remision", v.AutoRemision);
                                comando1.Parameters.AddWithValue("?documento_nombre", v.DocumentoNombre);
                                comando1.Parameters.AddWithValue("?subtotal_impuesto", v.SubTotalImpuesto);
                                comando1.Parameters.AddWithValue("?subtotal", v.SubTotal);
                                comando1.Parameters.AddWithValue("?auto_cxc", autoCxC);
                                comando1.Parameters.AddWithValue("?tipo_cliente", v.TipoCliente);
                                comando1.Parameters.AddWithValue("?planilla", v.Planilla);
                                comando1.Parameters.AddWithValue("?expediente", v.Expendiente);
                                comando1.Parameters.AddWithValue("?anticipo_iva", v.AnticipoIva);
                                comando1.Parameters.AddWithValue("?terceros_iva", v.TercerosIva);
                                comando1.Parameters.AddWithValue("?neto", v.Neto);
                                comando1.Parameters.AddWithValue("?costo", v.Costo);
                                comando1.Parameters.AddWithValue("?utilidad", v.Utilidad);
                                comando1.Parameters.AddWithValue("?utilidadp", v.Utilidadp);
                                comando1.Parameters.AddWithValue("?documento_tipo", v.DocumentoTipo);
                                comando1.Parameters.AddWithValue("?ci_titular", v.CiTitular);
                                comando1.Parameters.AddWithValue("?nombre_titular", v.NombreTitular);
                                comando1.Parameters.AddWithValue("?ci_beneficiario", v.CiBeneficiario);
                                comando1.Parameters.AddWithValue("?nombre_beneficiario", v.NombreBeneficiario);
                                comando1.Parameters.AddWithValue("?clave", v.Clave);
                                comando1.Parameters.AddWithValue("?denominacion_fiscal", v.DenominacionFiscal);
                                comando1.Parameters.AddWithValue("?cambio", v.Cambio);
                                comando1.Parameters.AddWithValue("?estatus_validado", v.EstatusValidado);
                                comando1.Parameters.AddWithValue("?cierre", autoCierre);
                                comando1.Parameters.AddWithValue("?fecha_retencion", v.FechaRetencion);
                                comando1.Parameters.AddWithValue("?estatus_cierre_contable", v.EstatusCierreContable);
                                var rt = comando1.ExecuteNonQuery();
                                if (rt == 0)
                                {
                                    result.Mensaje = "PROBLEMA AL INSERTAR REGISTRO DE VENTA";
                                    result.Result = DtoLib.Enumerados.EnumResult.isError;
                                    return result;
                                }


                                // INSERTAR CXC
                                comandoCxC.Parameters.Clear();
                                comandoCxC.Parameters.AddWithValue("?auto", autoCxC);
                                comandoCxC.Parameters.AddWithValue("?c_cobranza", v.DocCxC.CCobranza);
                                comandoCxC.Parameters.AddWithValue("?c_cobranzap", v.DocCxC.CCobranzap);
                                comandoCxC.Parameters.AddWithValue("?fecha", v.DocCxC.Fecha);
                                comandoCxC.Parameters.AddWithValue("?tipo_documento", v.DocCxC.TipoDocumento);
                                comandoCxC.Parameters.AddWithValue("?documento", v.DocCxC.Documento);
                                comandoCxC.Parameters.AddWithValue("?fecha_vencimiento", v.DocCxC.FechaVencimiento);
                                comandoCxC.Parameters.AddWithValue("?nota", v.DocCxC.Nota);
                                comandoCxC.Parameters.AddWithValue("?importe", v.DocCxC.Importe);
                                comandoCxC.Parameters.AddWithValue("?acumulado", v.DocCxC.Acumulado);
                                comandoCxC.Parameters.AddWithValue("?auto_cliente", v.DocCxC.AutoCliente);
                                comandoCxC.Parameters.AddWithValue("?cliente", v.DocCxC.Cliente);
                                comandoCxC.Parameters.AddWithValue("?ci_rif", v.DocCxC.CiRif);
                                comandoCxC.Parameters.AddWithValue("?codigo_cliente", v.DocCxC.CodigoCliente);
                                comandoCxC.Parameters.AddWithValue("?estatus_cancelado", v.DocCxC.EstatusCancelado);
                                comandoCxC.Parameters.AddWithValue("?resta", v.DocCxC.Resta);
                                comandoCxC.Parameters.AddWithValue("?estatus_anulado", v.DocCxC.EstatusAnulado);
                                comandoCxC.Parameters.AddWithValue("?auto_documento", autoVenta);
                                comandoCxC.Parameters.AddWithValue("?numero", v.DocCxC.Numero);
                                comandoCxC.Parameters.AddWithValue("?auto_agencia", v.DocCxC.AutoAgencia);
                                comandoCxC.Parameters.AddWithValue("?agencia", v.DocCxC.Agencia);
                                comandoCxC.Parameters.AddWithValue("?signo", v.DocCxC.Signo);
                                comandoCxC.Parameters.AddWithValue("?auto_vendedor", v.DocCxC.AutoVendedor);
                                comandoCxC.Parameters.AddWithValue("?c_departamento", v.DocCxC.CDepartamento);
                                comandoCxC.Parameters.AddWithValue("?c_ventas", v.DocCxC.CVentas);
                                comandoCxC.Parameters.AddWithValue("?c_ventasp", v.DocCxC.CVentasp);
                                comandoCxC.Parameters.AddWithValue("?serie", v.DocCxC.Serie);
                                comandoCxC.Parameters.AddWithValue("?importe_neto", v.DocCxC.ImporteNeto);
                                comandoCxC.Parameters.AddWithValue("?dias", v.DocCxC.Dias);
                                comandoCxC.Parameters.AddWithValue("?castigop", v.DocCxC.Castigop);
                                var rtCxC = comandoCxC.ExecuteNonQuery();
                                if (rtCxC == 0)
                                {
                                    result.Mensaje = "PROBLEMA AL INSERTAR REGISTRO DE CXC";
                                    result.Result = DtoLib.Enumerados.EnumResult.isError;
                                    return result;
                                }

                                if (v.DocCxCPago != null)
                                {
                                    aCxC += 1;
                                    var autoCxCPago = codSucursal + aCxC.ToString().Trim().PadLeft(6, '0');

                                    aCxCRecibo += 1;
                                    var autoCxCRecibo = codSucursal + aCxCRecibo.ToString().Trim().PadLeft(6, '0');

                                    aCxCReciboNumero += 1;
                                    var ReciboCxCNumero = codSucursal + aCxCReciboNumero.ToString().Trim().PadLeft(6, '0');

                                    //ACTUALZA VENTA , CON RECIBO Y NUMERO
                                    comandoVtaUpdate.Parameters.Clear();
                                    comandoVtaUpdate.Parameters.AddWithValue("?Auto", autoVenta);
                                    comandoVtaUpdate.Parameters.AddWithValue("?AutoRecibo", autoCxCRecibo);
                                    comandoVtaUpdate.Parameters.AddWithValue("?Recibo", ReciboCxCNumero);
                                    var rtVtaUpdate = comandoVtaUpdate.ExecuteNonQuery();
                                    if (rtVtaUpdate == 0)
                                    {
                                        result.Mensaje = "PROBLEMA AL ACTUALIZAR TABLA VENTA RECIBO DE PAGO";
                                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                                        return result;
                                    }

                                    var pago = v.DocCxCPago.Pago;
                                    // INSERTAR CXC PAGO
                                    comandoCxC.Parameters.Clear();
                                    comandoCxC.Parameters.AddWithValue("?auto", autoCxCPago);
                                    comandoCxC.Parameters.AddWithValue("?c_cobranza", pago.CCobranza);
                                    comandoCxC.Parameters.AddWithValue("?c_cobranzap", pago.CCobranzap);
                                    comandoCxC.Parameters.AddWithValue("?fecha", pago.Fecha);
                                    comandoCxC.Parameters.AddWithValue("?tipo_documento", pago.TipoDocumento);
                                    comandoCxC.Parameters.AddWithValue("?documento", ReciboCxCNumero);
                                    comandoCxC.Parameters.AddWithValue("?fecha_vencimiento", pago.FechaVencimiento);
                                    comandoCxC.Parameters.AddWithValue("?nota", pago.Nota);
                                    comandoCxC.Parameters.AddWithValue("?importe", pago.Importe);
                                    comandoCxC.Parameters.AddWithValue("?acumulado", pago.Acumulado);
                                    comandoCxC.Parameters.AddWithValue("?auto_cliente", pago.AutoCliente);
                                    comandoCxC.Parameters.AddWithValue("?cliente", pago.Cliente);
                                    comandoCxC.Parameters.AddWithValue("?ci_rif", pago.CiRif);
                                    comandoCxC.Parameters.AddWithValue("?codigo_cliente", pago.CodigoCliente);
                                    comandoCxC.Parameters.AddWithValue("?estatus_cancelado", pago.EstatusCancelado);
                                    comandoCxC.Parameters.AddWithValue("?resta", pago.Resta);
                                    comandoCxC.Parameters.AddWithValue("?estatus_anulado", pago.EstatusAnulado);
                                    comandoCxC.Parameters.AddWithValue("?auto_documento", autoCxCRecibo);
                                    comandoCxC.Parameters.AddWithValue("?numero", pago.Numero);
                                    comandoCxC.Parameters.AddWithValue("?auto_agencia", pago.AutoAgencia);
                                    comandoCxC.Parameters.AddWithValue("?agencia", pago.Agencia);
                                    comandoCxC.Parameters.AddWithValue("?signo", pago.Signo);
                                    comandoCxC.Parameters.AddWithValue("?auto_vendedor", pago.AutoVendedor);
                                    comandoCxC.Parameters.AddWithValue("?c_departamento", pago.CDepartamento);
                                    comandoCxC.Parameters.AddWithValue("?c_ventas", pago.CVentas);
                                    comandoCxC.Parameters.AddWithValue("?c_ventasp", pago.CVentasp);
                                    comandoCxC.Parameters.AddWithValue("?serie", pago.Serie);
                                    comandoCxC.Parameters.AddWithValue("?importe_neto", pago.ImporteNeto);
                                    comandoCxC.Parameters.AddWithValue("?dias", pago.Dias);
                                    comandoCxC.Parameters.AddWithValue("?castigop", pago.Castigop);
                                    rtCxC = comandoCxC.ExecuteNonQuery();
                                    if (rtCxC == 0)
                                    {
                                        result.Mensaje = "PROBLEMA AL INSERTAR REGISTRO DE CXC PAGO";
                                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                                        return result;
                                    }


                                    var rec = v.DocCxCPago.Recibo;
                                    //INSERTAR CXC RECIBO
                                    comandoCxCRecibo.Parameters.Clear();
                                    comandoCxCRecibo.Parameters.AddWithValue("?auto", autoCxCRecibo);
                                    comandoCxCRecibo.Parameters.AddWithValue("?documento", ReciboCxCNumero);
                                    comandoCxCRecibo.Parameters.AddWithValue("?fecha", rec.Fecha);
                                    comandoCxCRecibo.Parameters.AddWithValue("?auto_usuario", rec.AutoUsuario);
                                    comandoCxCRecibo.Parameters.AddWithValue("?importe", rec.Importe);
                                    comandoCxCRecibo.Parameters.AddWithValue("?usuario", rec.Usuario);
                                    comandoCxCRecibo.Parameters.AddWithValue("?monto_recibido", rec.MontoRecibido);
                                    comandoCxCRecibo.Parameters.AddWithValue("?cobrador", rec.Cobrador);
                                    comandoCxCRecibo.Parameters.AddWithValue("?auto_cliente", rec.AutoCliente);
                                    comandoCxCRecibo.Parameters.AddWithValue("?cliente", rec.Cliente);
                                    comandoCxCRecibo.Parameters.AddWithValue("?ci_rif", rec.CiRif);
                                    comandoCxCRecibo.Parameters.AddWithValue("?codigo", rec.Codigo);
                                    comandoCxCRecibo.Parameters.AddWithValue("?estatus_anulado", rec.EstatusAnulado);
                                    comandoCxCRecibo.Parameters.AddWithValue("?direccion", rec.Direccion);
                                    comandoCxCRecibo.Parameters.AddWithValue("?telefono", rec.Telefono);
                                    comandoCxCRecibo.Parameters.AddWithValue("?auto_cobrador", rec.AutoCobrador);
                                    comandoCxCRecibo.Parameters.AddWithValue("?anticipos", rec.Anticipos);
                                    comandoCxCRecibo.Parameters.AddWithValue("?cambio", rec.Cambio);
                                    comandoCxCRecibo.Parameters.AddWithValue("?nota", rec.Nota);
                                    comandoCxCRecibo.Parameters.AddWithValue("?codigo_cobrador", rec.CodigoCobrador);
                                    comandoCxCRecibo.Parameters.AddWithValue("?auto_cxc", autoCxCPago);
                                    comandoCxCRecibo.Parameters.AddWithValue("?retenciones", rec.Retenciones);
                                    comandoCxCRecibo.Parameters.AddWithValue("?descuentos", rec.Descuentos);
                                    comandoCxCRecibo.Parameters.AddWithValue("?hora", rec.Hora);
                                    comandoCxCRecibo.Parameters.AddWithValue("?cierre", autoCierre);
                                    var rtCxCRecibo = comandoCxCRecibo.ExecuteNonQuery();
                                    if (rtCxCRecibo == 0)
                                    {
                                        result.Mensaje = "PROBLEMA AL INSERTAR REGISTRO DE CXC RECIBO";
                                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                                        return result;
                                    }


                                    var cDoc = v.DocCxCPago.Documento;
                                    //INSERTAR CXC DOCUMENTO
                                    comandoCxCDocumento.Parameters.Clear();
                                    comandoCxCDocumento.Parameters.AddWithValue("?id", cDoc.Id);
                                    comandoCxCDocumento.Parameters.AddWithValue("?fecha", cDoc.Fecha);
                                    comandoCxCDocumento.Parameters.AddWithValue("?tipo_documento", cDoc.TipoDocumento);
                                    comandoCxCDocumento.Parameters.AddWithValue("?documento", cDoc.Documento);
                                    comandoCxCDocumento.Parameters.AddWithValue("?importe", cDoc.Importe);
                                    comandoCxCDocumento.Parameters.AddWithValue("?operacion", cDoc.Operacion);
                                    comandoCxCDocumento.Parameters.AddWithValue("?auto_cxc", autoCxC);
                                    comandoCxCDocumento.Parameters.AddWithValue("?auto_cxc_pago", autoCxCPago);
                                    comandoCxCDocumento.Parameters.AddWithValue("?auto_cxc_recibo", autoCxCRecibo);
                                    comandoCxCDocumento.Parameters.AddWithValue("?numero_recibo", ReciboCxCNumero);
                                    comandoCxCDocumento.Parameters.AddWithValue("?fecha_recepcion", cDoc.FechaRecepcion);
                                    comandoCxCDocumento.Parameters.AddWithValue("?dias", cDoc.Dias);
                                    comandoCxCDocumento.Parameters.AddWithValue("?castigop", cDoc.CastigoP);
                                    comandoCxCDocumento.Parameters.AddWithValue("?comisionp", cDoc.ComisionP);
                                    var rtCxCDocumento = comandoCxCDocumento.ExecuteNonQuery();
                                    if (rtCxCDocumento == 0)
                                    {
                                        result.Mensaje = "PROBLEMA AL INSERTAR REGISTRO DE CXC DOCUMENTO";
                                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                                        return result;
                                    }


                                    //INSERTAR CXC MEDIO PAGO
                                    foreach (var cmtPago in v.DocCxCPago.MetodoPago)
                                    {
                                        comandoCxCMedioPago.Parameters.Clear();
                                        comandoCxCMedioPago.Parameters.AddWithValue("?auto_recibo", autoCxCRecibo);
                                        comandoCxCMedioPago.Parameters.AddWithValue("?auto_medio_pago", cmtPago.AutoMedioPago);
                                        comandoCxCMedioPago.Parameters.AddWithValue("?auto_agencia", cmtPago.AutoAgencia);
                                        comandoCxCMedioPago.Parameters.AddWithValue("?medio", cmtPago.Medio);
                                        comandoCxCMedioPago.Parameters.AddWithValue("?codigo", cmtPago.Codigo);
                                        comandoCxCMedioPago.Parameters.AddWithValue("?monto_recibido", cmtPago.MontoRecibido);
                                        comandoCxCMedioPago.Parameters.AddWithValue("?fecha", cmtPago.Fecha);
                                        comandoCxCMedioPago.Parameters.AddWithValue("?estatus_anulado", cmtPago.EstatusAnulado);
                                        comandoCxCMedioPago.Parameters.AddWithValue("?numero", cmtPago.Numero);
                                        comandoCxCMedioPago.Parameters.AddWithValue("?agencia", cmtPago.Agencia);
                                        comandoCxCMedioPago.Parameters.AddWithValue("?auto_usuario", cmtPago.AutoUsuario);
                                        comandoCxCMedioPago.Parameters.AddWithValue("?lote", cmtPago.Lote);
                                        comandoCxCMedioPago.Parameters.AddWithValue("?referencia", cmtPago.Referencia);
                                        comandoCxCMedioPago.Parameters.AddWithValue("?auto_cobrador", cmtPago.AutoCobrador);
                                        comandoCxCMedioPago.Parameters.AddWithValue("?cierre", autoCierre);
                                        comandoCxCMedioPago.Parameters.AddWithValue("?fecha_agencia", cmtPago.FechaAgencia);
                                        var rtCxCMetPago = comandoCxCMedioPago.ExecuteNonQuery();
                                        if (rtCxCMetPago == 0)
                                        {
                                            result.Mensaje = "PROBLEMA AL INSERTAR REGISTRO DE METODO DE PAGO DE CXC ";
                                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                                            return result;
                                        }
                                    };
                                }


                                //DETALLES DEL DOCUMENTO
                                foreach (var vd in v.Detalles)
                                {
                                    comando2.Parameters.Clear();
                                    comando2.Parameters.AddWithValue("?auto_documento", autoVenta);
                                    comando2.Parameters.AddWithValue("?auto_producto", vd.AutoProducto);
                                    comando2.Parameters.AddWithValue("?codigo", vd.Codigo);
                                    comando2.Parameters.AddWithValue("?nombre", vd.Nombre);
                                    comando2.Parameters.AddWithValue("?auto_departamento", vd.AutoDepartamento);
                                    comando2.Parameters.AddWithValue("?auto_grupo", vd.AutoGrupo);
                                    comando2.Parameters.AddWithValue("?auto_subgrupo", vd.AutoSubGrupo);
                                    comando2.Parameters.AddWithValue("?auto_deposito", vd.AutoDeposito);
                                    comando2.Parameters.AddWithValue("?cantidad", vd.Cantidad);
                                    comando2.Parameters.AddWithValue("?empaque", vd.Empaque);
                                    comando2.Parameters.AddWithValue("?precio_neto", vd.PrecioNeto);
                                    comando2.Parameters.AddWithValue("?descuento1p", vd.Descuento1p);
                                    comando2.Parameters.AddWithValue("?descuento2p", vd.Descuento2p);
                                    comando2.Parameters.AddWithValue("?descuento3p", vd.Descuento3p);
                                    comando2.Parameters.AddWithValue("?descuento1", vd.Descuento1);
                                    comando2.Parameters.AddWithValue("?descuento2", vd.Descuento2);
                                    comando2.Parameters.AddWithValue("?descuento3", vd.Descuento3);
                                    comando2.Parameters.AddWithValue("?costo_venta", vd.CostoVenta);
                                    comando2.Parameters.AddWithValue("?total_neto", vd.TotalNeto);
                                    comando2.Parameters.AddWithValue("?tasa", vd.Tasa);
                                    comando2.Parameters.AddWithValue("?impuesto", vd.Impuesto);
                                    comando2.Parameters.AddWithValue("?total", vd.Total);
                                    comando2.Parameters.AddWithValue("?auto", vd.Auto);
                                    comando2.Parameters.AddWithValue("?estatus_anulado", vd.EstatusAnulado);
                                    comando2.Parameters.AddWithValue("?fecha", vd.Fecha);
                                    comando2.Parameters.AddWithValue("?tipo", vd.Tipo);
                                    comando2.Parameters.AddWithValue("?deposito", vd.Deposito);
                                    comando2.Parameters.AddWithValue("?signo", vd.Signo);
                                    comando2.Parameters.AddWithValue("?precio_final", vd.PrecioFinal);
                                    comando2.Parameters.AddWithValue("?auto_cliente", vd.AutoCliente);
                                    comando2.Parameters.AddWithValue("?decimales", vd.Decimales);
                                    comando2.Parameters.AddWithValue("?contenido_empaque", vd.ContenidoEmpaque);
                                    comando2.Parameters.AddWithValue("?cantidad_und", vd.CantidadUnd);
                                    comando2.Parameters.AddWithValue("?precio_und", vd.PrecioUnd);
                                    comando2.Parameters.AddWithValue("?costo_und", vd.CostoUnd);
                                    comando2.Parameters.AddWithValue("?utilidad", vd.Utilidad);
                                    comando2.Parameters.AddWithValue("?utilidadp", vd.Utilidadp);
                                    comando2.Parameters.AddWithValue("?precio_item", vd.PrecioItem);
                                    comando2.Parameters.AddWithValue("?estatus_garantia", vd.EstatusGarantia);
                                    comando2.Parameters.AddWithValue("?estatus_serial", vd.EstatusSerial);
                                    comando2.Parameters.AddWithValue("?codigo_deposito", vd.CodigoDeposito);
                                    comando2.Parameters.AddWithValue("?dias_garantia", vd.DiasGarantia);
                                    comando2.Parameters.AddWithValue("?detalle", vd.Detalle);
                                    comando2.Parameters.AddWithValue("?precio_sugerido", vd.PrecioSugerido);
                                    comando2.Parameters.AddWithValue("?auto_tasa", vd.AutoTasa);
                                    comando2.Parameters.AddWithValue("?estatus_corte", vd.EstatusCorte);
                                    comando2.Parameters.AddWithValue("?x", vd.X);
                                    comando2.Parameters.AddWithValue("?y", vd.Y);
                                    comando2.Parameters.AddWithValue("?z", vd.Z);
                                    comando2.Parameters.AddWithValue("?corte", vd.Corte);
                                    comando2.Parameters.AddWithValue("?categoria", vd.Categoria);
                                    comando2.Parameters.AddWithValue("?cobranzap", vd.Cobranzap);
                                    comando2.Parameters.AddWithValue("?ventasp", vd.Ventasp);
                                    comando2.Parameters.AddWithValue("?cobranzap_vendedor", vd.CobranzapVendedor);
                                    comando2.Parameters.AddWithValue("?ventasp_vendedor", vd.VentaspVendedor);
                                    comando2.Parameters.AddWithValue("?cobranza", vd.Cobranza);
                                    comando2.Parameters.AddWithValue("?ventas", vd.Ventas);
                                    comando2.Parameters.AddWithValue("?cobranza_vendedor", vd.CobranzaVendedor);
                                    comando2.Parameters.AddWithValue("?ventas_vendedor", vd.VentasVendedor);
                                    comando2.Parameters.AddWithValue("?costo_promedio_und", vd.CostoPromedioUnd);
                                    comando2.Parameters.AddWithValue("?costo_compra", vd.CostoCompra);
                                    comando2.Parameters.AddWithValue("?estatus_checked", vd.EstatusChecked);
                                    comando2.Parameters.AddWithValue("?tarifa", vd.Tarifa);
                                    comando2.Parameters.AddWithValue("?total_descuento", vd.TotalDescuento);
                                    comando2.Parameters.AddWithValue("?codigo_vendedor", vd.CodigoVendedor);
                                    comando2.Parameters.AddWithValue("?auto_vendedor", vd.AutoVendedor);
                                    comando2.Parameters.AddWithValue("?hora", vd.Hora);
                                    var rt2 = comando2.ExecuteNonQuery();
                                    if (rt2 == 0)
                                    {
                                        result.Mensaje = "PROBLEMA AL INSERTAR REGISTRO DE VENTA DETALLE";
                                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                                        return result;
                                    }
                                }


                                //MOVIMIENTO KARDEX DEL DOCUMENTO
                                foreach (var mk in v.MovKardex)
                                {
                                    comando3.Parameters.Clear();
                                    comando3.Parameters.AddWithValue("?auto_producto", mk.AutoProducto);
                                    comando3.Parameters.AddWithValue("?total", mk.Total);
                                    comando3.Parameters.AddWithValue("?auto_deposito", mk.AutoDeposito);
                                    comando3.Parameters.AddWithValue("?auto_concepto", mk.AutoConcepto);
                                    comando3.Parameters.AddWithValue("?auto_documento", autoVenta);
                                    comando3.Parameters.AddWithValue("?fecha", mk.Fecha);
                                    comando3.Parameters.AddWithValue("?hora", mk.Hora);
                                    comando3.Parameters.AddWithValue("?documento", mk.Documento);
                                    comando3.Parameters.AddWithValue("?modulo", mk.Modulo);
                                    comando3.Parameters.AddWithValue("?entidad", mk.Entidad);
                                    comando3.Parameters.AddWithValue("?signo", mk.Signo);
                                    comando3.Parameters.AddWithValue("?cantidad", mk.Cantidad);
                                    comando3.Parameters.AddWithValue("?cantidad_bono", mk.CantidadBono);
                                    comando3.Parameters.AddWithValue("?cantidad_und", mk.CantidadUnd);
                                    comando3.Parameters.AddWithValue("?costo_und", mk.CostoUnd);
                                    comando3.Parameters.AddWithValue("?estatus_anulado", mk.EstatusAnulado);
                                    comando3.Parameters.AddWithValue("?nota", mk.Nota);
                                    comando3.Parameters.AddWithValue("?precio_und", mk.PrecioUnd);
                                    comando3.Parameters.AddWithValue("?codigo", mk.Codigo);
                                    comando3.Parameters.AddWithValue("?siglas", mk.Siglas);
                                    comando3.Parameters.AddWithValue("?codigo_sucursal", mk.CodigoSucursal);
                                    comando3.Parameters.AddWithValue("?cierre_ftp", "");
                                    comando3.Parameters.AddWithValue("?codigo_deposito", mk.CodigoDeposito);
                                    comando3.Parameters.AddWithValue("?nombre_deposito", mk.NombreDeposito);
                                    comando3.Parameters.AddWithValue("?codigo_concepto", mk.CodigoConcepto);
                                    comando3.Parameters.AddWithValue("?nombre_concepto", mk.NombreConcepto);
                                    var rt3 = comando3.ExecuteNonQuery();
                                    if (rt3 == 0)
                                    {
                                        result.Mensaje = "PROBLEMA AL INSERTAR REGISTRO DE MOVIMIENTO KARDEX ";
                                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                                        return result;
                                    }
                                }

                                //ACTUALIZAR DEPOSITO 
                                foreach (var pd in v.ActDeposito)
                                {
                                    comando4.Parameters.Clear();
                                    comando4.Parameters.AddWithValue("?autoProducto", pd.AutoProducto);
                                    comando4.Parameters.AddWithValue("?autoDeposito", pd.AutoDeposito);
                                    comando4.Parameters.AddWithValue("?cantidadUnd", pd.CantUnd);
                                    var rt4 = comando4.ExecuteNonQuery();
                                    if (rt4 == 0)
                                    {
                                        result.Mensaje = "PROBLEMA AL ACTUALIZAR DEPOSITO";
                                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                                        return result;
                                    }
                                }
                            };
                        };

                        //ACTUALIZAR CONTADOR DE VENTAS
                        var sql = "update sistema_contadores set a_ventas=@aVentas, a_cxc=@aCxC, a_cxc_recibo=@aCxCRecibo, a_cxc_recibo_numero=@aCxCReciboNumero, a_cierre=@aCierre";
                        comando1 = new MySqlCommand(sql, cn, tr);
                        comando1.Parameters.AddWithValue("@aVentas", aVenta);
                        comando1.Parameters.AddWithValue("@aCxC", aCxC);
                        comando1.Parameters.AddWithValue("@aCxCRecibo", aCxCRecibo);
                        comando1.Parameters.AddWithValue("@aCxCReciboNumero", aCxCReciboNumero);
                        comando1.Parameters.AddWithValue("@aCierre", aCierre);
                        var rtx = comando1.ExecuteNonQuery();
                        if (rtx == 0)
                        {
                            result.Mensaje = "PROBLEMA AL ACTUALIZAR TABLA CONTADORES AUTOMATICOS";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }


                        //ACTUALIZAR SERIES
                        var sqlSerieA = "select correlativo from empresa_series_fiscales where auto=?auto";
                        var comando6A = new MySqlCommand(sqlSerieA, cn, tr);
                        var sqlSerie = "update empresa_series_fiscales set correlativo=?correlativo where auto=?auto";
                        var comando6B = new MySqlCommand(sqlSerie, cn, tr);
                        foreach (var ns in ficha.Series)
                        {
                            //BUSCAR POR AUTO LA SERIE A ACTUALIZAR
                            comando6A.Parameters.Clear();
                            comando6A.Parameters.AddWithValue("?auto", ns.Auto);
                            var rc = comando6A.ExecuteScalar();
                            if (rc == null)
                            {
                                result.Mensaje = "SERIE NO ENCONTRADA";
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                return result;
                            }

                            //VERIFICAR SI LA SERIE A ACTUALIZAR SU CORRELATIVO ES SUPERIOR AL REGISTRADO
                            if (ns.Correlativo > int.Parse(rc.ToString()))
                            {
                                comando6B.Parameters.Clear();
                                comando6B.Parameters.AddWithValue("?correlativo", ns.Correlativo);
                                comando6B.Parameters.AddWithValue("?auto", ns.Auto);
                                var rt6 = comando6B.ExecuteNonQuery();
                                if (rt6 == 0)
                                {
                                    result.Mensaje = "PROBLEMA AL ACTUALIZAR SERIES";
                                    result.Result = DtoLib.Enumerados.EnumResult.isError;
                                    return result;
                                }
                            }
                        }

                        try
                        {
                            using (var cnn = new LibEntitySqLitePosOffLine.LeonuxPosOffLineEntities(_cnn.ConnectionString))
                            {
                                using (var ts = cnn.Database.BeginTransaction())
                                {
                                    try
                                    {
                                        foreach (var j in ficha.Jornadas)
                                        {
                                            var entJornada = cnn.Jornada.Find(j.Id);
                                            if (entJornada == null)
                                            {
                                                result.Mensaje = "[ ID ] JORNADA NO ENCONTRADO";
                                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                                return result;
                                            }
                                            entJornada.estatusTransmitida = j.EstatusTransmitida;
                                            cnn.SaveChanges();
                                        }
                                        ts.Commit();
                                    }
                                    catch (Exception extt)
                                    {
                                        result.Mensaje = extt.Message;
                                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                                        return result;
                                    }
                                }
                            }
                        }
                        catch (Exception ext)
                        {
                            result.Mensaje = ext.Message;
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }

                        tr.Commit();
                    }
                    catch (Exception ex1)
                    {
                        tr.Rollback();
                        result.Mensaje = ex1.Message;
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                    }
                };
            }
            catch (MySqlException ex2)
            {
                result.Mensaje = ex2.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }

        public DtoLib.Resultado Servidor_Principal_CrearBoletin(string pathDestino)
        {
            var result = new DtoLib.Resultado();

            try
            {
                using (var cn = new MySqlConnection(_cnn2.ConnectionString))
                {
                    cn.Open();
                    var sql0 = "";
                    MySqlCommand comando1;
                    var rt = -1;

                    sql0 = "select * into outfile \"" + pathDestino + "usuarios_grupo.txt\" from usuarios_grupo";
                    comando1 = new MySqlCommand(sql0, cn);
                    rt = comando1.ExecuteNonQuery();

                    sql0 = "select * into outfile \"" + pathDestino + "usuarios.txt\" from usuarios";
                    comando1 = new MySqlCommand(sql0, cn);
                    rt = comando1.ExecuteNonQuery();

                    sql0 = "select * into outfile \"" + pathDestino + "usuarios_grupo_permisos.txt\" from usuarios_grupo_permisos";
                    comando1 = new MySqlCommand(sql0, cn);
                    rt = comando1.ExecuteNonQuery();

                    sql0 = "select * into outfile \"" + pathDestino + "empresa.txt\" from empresa";
                    comando1 = new MySqlCommand(sql0, cn);
                    rt = comando1.ExecuteNonQuery();

                    sql0 = "select * into outfile \"" + pathDestino + "empresa_departamentos.txt\" from empresa_departamentos";
                    comando1 = new MySqlCommand(sql0, cn);
                    rt = comando1.ExecuteNonQuery();

                    sql0 = "select * into outfile \"" + pathDestino + "empresa_tasas.txt\" from empresa_tasas";
                    comando1 = new MySqlCommand(sql0, cn);
                    rt = comando1.ExecuteNonQuery();

                    sql0 = "select * into outfile \"" + pathDestino + "sistema_configuracion.txt\" from sistema_configuracion";
                    comando1 = new MySqlCommand(sql0, cn);
                    rt = comando1.ExecuteNonQuery();

                    sql0 = "select * into outfile \"" + pathDestino + "productos.txt\" from productos";
                    comando1 = new MySqlCommand(sql0, cn);
                    rt = comando1.ExecuteNonQuery();

                    sql0 = "select * into outfile \"" + pathDestino + "productos_alterno.txt\" from productos_alterno";
                    comando1 = new MySqlCommand(sql0, cn);
                    rt = comando1.ExecuteNonQuery();

                    sql0 = "select * into outfile \"" + pathDestino + "productos_deposito.txt\" from productos_deposito";
                    comando1 = new MySqlCommand(sql0, cn);
                    rt = comando1.ExecuteNonQuery();

                    sql0 = "select * into outfile \"" + pathDestino + "productos_grupo.txt\" from productos_grupo";
                    comando1 = new MySqlCommand(sql0, cn);
                    rt = comando1.ExecuteNonQuery();

                    sql0 = "select * into outfile \"" + pathDestino + "productos_lista.txt\" from productos_lista";
                    comando1 = new MySqlCommand(sql0, cn);
                    rt = comando1.ExecuteNonQuery();

                    sql0 = "select * into outfile \"" + pathDestino + "productos_marca.txt\" from productos_marca";
                    comando1 = new MySqlCommand(sql0, cn);
                    rt = comando1.ExecuteNonQuery();

                    sql0 = "select * into outfile \"" + pathDestino + "proveeodres_grupo.txt\" from proveedores_grupo";
                    comando1 = new MySqlCommand(sql0, cn);
                    rt = comando1.ExecuteNonQuery();

                    sql0 = "select * into outfile \"" + pathDestino + "proveeodres.txt\" from proveedores";
                    comando1 = new MySqlCommand(sql0, cn);
                    rt = comando1.ExecuteNonQuery();

                    sql0 = "select * into outfile \"" + pathDestino + "compras.txt\" from compras where tipo='04' and  FECHA >= DATE_SUB(CURDATE(), INTERVAL 2 MONTH)";
                    comando1 = new MySqlCommand(sql0, cn);
                    rt = comando1.ExecuteNonQuery();

                    sql0 = "select * into outfile \"" + pathDestino + "compras_detalle.txt\" from compras_detalle where tipo='04' and  FECHA >= DATE_SUB(CURDATE(), INTERVAL 2 MONTH)";
                    comando1 = new MySqlCommand(sql0, cn);
                    rt = comando1.ExecuteNonQuery();

                    sql0 = "select * into outfile \"" + pathDestino + "productos_precios.txt\" from productos_precios where FECHA >= DATE_SUB(CURDATE(), INTERVAL 1 WEEK)";
                    comando1 = new MySqlCommand(sql0, cn);
                    rt = comando1.ExecuteNonQuery();

                    sql0 = "select * into outfile \"" + pathDestino + "clientes.txt\" FROM clientes where auto >'0900000001'";
                    comando1 = new MySqlCommand(sql0, cn);
                    rt = comando1.ExecuteNonQuery();

                    sql0 = "select * into outfile \"" + pathDestino + "productos_kardex.txt\" FROM productos_kardex where modulo<>'Ventas' and fecha>='2020/01/01'";
                    comando1 = new MySqlCommand(sql0, cn);
                    rt = comando1.ExecuteNonQuery();

                    sql0 = "select * into outfile \"" + pathDestino + "empresa_grupo.txt\" FROM empresa_grupo";
                    comando1 = new MySqlCommand(sql0, cn);
                    rt = comando1.ExecuteNonQuery();

                    sql0 = "select * into outfile \"" + pathDestino + "empresa_sucursal.txt\" FROM empresa_sucursal";
                    comando1 = new MySqlCommand(sql0, cn);
                    rt = comando1.ExecuteNonQuery();

                    sql0 = "select * into outfile \"" + pathDestino + "sistema_menu.txt\" FROM sistema_menu";
                    comando1 = new MySqlCommand(sql0, cn);
                    rt = comando1.ExecuteNonQuery();

                    sql0 = "select * into outfile \"" + pathDestino + "sistema_funciones.txt\" FROM sistema_funciones";
                    comando1 = new MySqlCommand(sql0, cn);
                    rt = comando1.ExecuteNonQuery();

                    sql0 = "select * into outfile \"" + pathDestino + "empresa_depositos.txt\" FROM empresa_depositos";
                    comando1 = new MySqlCommand(sql0, cn);
                    rt = comando1.ExecuteNonQuery();

                    sql0 = "select * into outfile \"" + pathDestino + "productos_movimientos.txt\" FROM productos_movimientos where fecha>='2020/01/01' ";
                    comando1 = new MySqlCommand(sql0, cn);
                    rt = comando1.ExecuteNonQuery();

                    sql0 = "select * into outfile \"" + pathDestino + "productos_movimientos_detalle.txt\" FROM productos_movimientos_detalle where fecha>='2020/01/01' ";
                    comando1 = new MySqlCommand(sql0, cn);
                    rt = comando1.ExecuteNonQuery();
                };
            }
            catch (MySqlException ex2)
            {
                result.Mensaje = ex2.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }

        public DtoLib.Resultado Servidor_Principal_InsertarCierre(string pathOrigen)
        {
            var result = new DtoLib.Resultado();
            var listMv = new List<DataKardexResumen>();

            try
            {
                using (var cn = new MySqlConnection(_cnn2.ConnectionString))
                {
                    cn.Open();

                    MySqlTransaction tr = null;
                    try
                    {
                        tr = cn.BeginTransaction();


                        var sql0 = "";
                        MySqlCommand comando1;
                        var rt = -1;


                        sql0 = "SET FOREIGN_KEY_CHECKS=0";
                        comando1 = new MySqlCommand(sql0, cn, tr);
                        rt = comando1.ExecuteNonQuery();


                        //VENTAS
                        sql0 = "load data infile \"" + pathOrigen + "ventas.txt\" into table ventas";
                        comando1 = new MySqlCommand(sql0, cn, tr);
                        rt = comando1.ExecuteNonQuery();

                        sql0 = "load data infile \"" + pathOrigen + "ventas_detalle.txt\" into table ventas_detalle";
                        comando1 = new MySqlCommand(sql0, cn, tr);
                        rt = comando1.ExecuteNonQuery();


                        //CXC
                        sql0 = "load data infile \"" + pathOrigen + "cxc.txt\" into table cxc";
                        comando1 = new MySqlCommand(sql0, cn, tr);
                        rt = comando1.ExecuteNonQuery();

                        sql0 = "load data infile \"" + pathOrigen + "cxc_recibos.txt\" into table cxc_recibos";
                        comando1 = new MySqlCommand(sql0, cn, tr);
                        rt = comando1.ExecuteNonQuery();

                        sql0 = "load data infile \"" + pathOrigen + "cxc_documentos.txt\" into table cxc_documentos";
                        comando1 = new MySqlCommand(sql0, cn, tr);
                        rt = comando1.ExecuteNonQuery();

                        sql0 = "load data infile \"" + pathOrigen + "cxc_medio_pago.txt\" into table cxc_medio_pago";
                        comando1 = new MySqlCommand(sql0, cn, tr);
                        rt = comando1.ExecuteNonQuery();


                        //KARDEX
                        sql0 = "load data infile \"" + pathOrigen + "productos_kardex.txt\" into table productos_kardex";
                        comando1 = new MySqlCommand(sql0, cn, tr);
                        rt = comando1.ExecuteNonQuery();


                        //ARQUEO
                        sql0 = "load data infile \"" + pathOrigen + "pos_arqueo.txt\" into table pos_arqueo";
                        comando1 = new MySqlCommand(sql0, cn, tr);
                        rt = comando1.ExecuteNonQuery();


                        //COMPRAS
                        sql0 = "load data infile \"" + pathOrigen + "compras.txt\" into table compras";
                        comando1 = new MySqlCommand(sql0, cn, tr);
                        rt = comando1.ExecuteNonQuery();

                        sql0 = "load data infile \"" + pathOrigen + "compras_detalle.txt\" into table compras_detalle";
                        comando1 = new MySqlCommand(sql0, cn, tr);
                        rt = comando1.ExecuteNonQuery();


                        //PRODUCTOS MOVIMIENTOS
                        sql0 = "load data infile \"" + pathOrigen + "productos_movimientos.txt\" into table productos_movimientos";
                        comando1 = new MySqlCommand(sql0, cn, tr);
                        rt = comando1.ExecuteNonQuery();

                        sql0 = "load data infile \"" + pathOrigen + "productos_movimientos_detalle.txt\" into table productos_movimientos_detalle";
                        comando1 = new MySqlCommand(sql0, cn, tr);
                        rt = comando1.ExecuteNonQuery();


                        //ACTULIZAR DEPOSITO
                        sql0 = "delete from productos_kardex_resumen";
                        comando1 = new MySqlCommand(sql0, cn, tr);
                        rt = comando1.ExecuteNonQuery();

                        sql0 = "load data infile \"" + pathOrigen + "productos_kardex_resumen.txt\" into table productos_kardex_resumen";
                        comando1 = new MySqlCommand(sql0, cn, tr);
                        rt = comando1.ExecuteNonQuery();

                        sql0 = "select auto_producto, auto_deposito, cnt from productos_kardex_resumen";
                        comando1 = new MySqlCommand(sql0, cn, tr);
                        var reader = comando1.ExecuteReader();
                        while (reader.Read())
                        {
                            var nr = new DataKardexResumen()
                            {
                                autoProducto = reader.GetString("auto_producto"),
                                autoDeposito = reader.GetString("auto_deposito"),
                                cnt = reader.GetDecimal("cnt")
                            };
                            listMv.Add(nr);
                        }
                        reader.Close();

                        sql0 = "update productos_deposito set fisica=fisica+?cnt, disponible=disponible+?cnt where auto_producto=?ap and auto_deposito=?ad";
                        comando1 = new MySqlCommand(sql0, cn, tr);
                        foreach (var mv in listMv)
                        {
                            comando1.Parameters.Clear();
                            comando1.Parameters.AddWithValue("?cnt", mv.cnt);
                            comando1.Parameters.AddWithValue("?ap", mv.autoProducto);
                            comando1.Parameters.AddWithValue("?ad", mv.autoDeposito);
                            rt = comando1.ExecuteNonQuery();
                        }

                        sql0 = "SET FOREIGN_KEY_CHECKS=1";
                        comando1 = new MySqlCommand(sql0, cn, tr);
                        rt = comando1.ExecuteNonQuery();

                        tr.Commit();
                    }
                    catch (Exception ex1)
                    {
                        tr.Rollback();
                        result.Mensaje = ex1.Message;
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                    }
                };
            }
            catch (MySqlException ex2)
            {
                result.Mensaje = ex2.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        
        public DtoLib.Resultado Servidor_Principal_PreprararCierre(string codigoEmpresa, string rutaLeonuxBandeja, string rutaLeonuxFtpBandejaData)
        {
            var result = new DtoLib.Resultado();

            try
            {
                using (var cn = new MySqlConnection(_cnn2.ConnectionString))
                {
                    cn.Open();

                    MySqlTransaction tr = null;
                    try
                    {
                        var sql0 = "";
                        MySqlCommand comando1;
                        var rt = -1;

                        var pathBandeja = rutaLeonuxBandeja;
                        var pathTemp = rutaLeonuxBandeja + @"/temp";
                        var pathDestino = rutaLeonuxBandeja + @"/temp/";
                        var pathFtpData = rutaLeonuxFtpBandejaData;

                        tr = cn.BeginTransaction();

                        //VENTAS
                        sql0 = "select * into outfile \"" + pathDestino + "ventas.txt\" FROM ventas where (tipo='01' or tipo='02' or tipo='03' or tipo='04') and cierre_ftp=''";
                        comando1 = new MySqlCommand(sql0, cn,tr);
                        rt = comando1.ExecuteNonQuery();

                        sql0 = "select * into outfile \"" + pathDestino + "ventas_detalle.txt\" FROM ventas_detalle where (tipo='01' or tipo='02' or tipo='03' or tipo='04') and cierre_ftp=''";
                        comando1 = new MySqlCommand(sql0, cn,tr);
                        rt = comando1.ExecuteNonQuery();


                        //CXC
                        sql0 = "select * into outfile \"" + pathDestino + "cxc.txt\" FROM cxc where cierre_ftp=''";
                        comando1 = new MySqlCommand(sql0, cn,tr);
                        rt = comando1.ExecuteNonQuery();

                        sql0 = "select * into outfile \"" + pathDestino + "cxc_recibos.txt\" FROM cxc_recibos where cierre_ftp=''";
                        comando1 = new MySqlCommand(sql0, cn, tr);
                        rt = comando1.ExecuteNonQuery();

                        sql0 = "select * into outfile \"" + pathDestino + "cxc_documentos.txt\" FROM cxc_documentos where cierre_ftp=''";
                        comando1 = new MySqlCommand(sql0, cn, tr);
                        rt = comando1.ExecuteNonQuery();

                        sql0 = "select * into outfile \"" + pathDestino + "cxc_medio_pago.txt\" FROM cxc_medio_pago where cierre_ftp=''";
                        comando1 = new MySqlCommand(sql0, cn, tr);
                        rt = comando1.ExecuteNonQuery();


                        //MOV KARDEX
                        sql0 = "select * into outfile \"" + pathDestino + "productos_kardex.txt\" FROM productos_kardex where  modulo='Ventas' and cierre_ftp='' ";
                        comando1 = new MySqlCommand(sql0, cn, tr);
                        rt = comando1.ExecuteNonQuery();


                        //JORNADA
                        sql0 = "SELECT NULL as id,fecha,estatus_cierre INTO OUTFILE \"" + pathDestino + "pos_jornadas.txt\" FROM pos_jornadas where cierre_ftp='' ";
                        comando1 = new MySqlCommand(sql0, cn, tr);
                        rt = comando1.ExecuteNonQuery();


                        //ARQUEO
                        sql0 = "select * into OUTFILE \"" + pathDestino + "pos_arqueo.txt\" FROM pos_arqueo where cierre_ftp='' ";
                        comando1 = new MySqlCommand(sql0, cn, tr);
                        rt = comando1.ExecuteNonQuery();


                        //COMPRAS
                        sql0 = "select * into OUTFILE \"" + pathDestino + "compras.txt\" FROM compras where tipo='05' and cierre_ftp='' ";
                        comando1 = new MySqlCommand(sql0, cn, tr);
                        rt = comando1.ExecuteNonQuery();

                        sql0 = "select * into OUTFILE \"" + pathDestino + "compras_detalle.txt\" FROM compras_detalle where tipo='05' and cierre_ftp='' ";
                        comando1 = new MySqlCommand(sql0, cn, tr);
                        rt = comando1.ExecuteNonQuery();


                        //KARDEX RESUMEN
                        sql0 = "select auto_producto, sum(cantidad*signo) as cnt, auto_deposito " +
                               "into outfile \"" + pathDestino + "productos_kardex_resumen.txt\"" +
                               "FROM productos_kardex where estatus_anulado='0' and modulo='Ventas' and cierre_ftp='' " +
                               "group by auto_producto, auto_deposito";
                        comando1 = new MySqlCommand(sql0, cn, tr);
                        rt = comando1.ExecuteNonQuery();


                        //PRODUCTOS MOVIMIENTOS
                        sql0 = "select * into OUTFILE \"" + pathDestino + "productos_movimientos.txt\" FROM productos_movimientos where tipo='05' and cierre_ftp='' ";
                        comando1 = new MySqlCommand(sql0, cn, tr);
                        rt = comando1.ExecuteNonQuery();

                        sql0 = "select * into OUTFILE \"" + pathDestino + "productos_movimientos_detalle.txt\" FROM productos_movimientos_detalle where tipo='05' and cierre_ftp='' ";
                        comando1 = new MySqlCommand(sql0, cn, tr);
                        rt = comando1.ExecuteNonQuery();


                        //EMPAQUETAR CIERRE
                        var fecha = DateTime.Now;
                        var df= "data"+codigoEmpresa+"_";
                        df += fecha.Year.ToString() + "_";
                        df += fecha.Month.ToString().Trim().PadLeft(2, '0') + "_";
                        df += fecha.Day.ToString().Trim().PadLeft(2, '0') + "_";
                        df += "h_"+fecha.Hour.ToString().Trim().PadLeft(2, '0') + "_";
                        df += fecha.Minute.ToString().Trim().PadLeft(2, '0');
                        df += ".zip";

                        var destino = "";
                        destino += pathBandeja + @"/"+df;
                        ZipFile.CreateFromDirectory(pathTemp, destino, CompressionLevel.Fastest, false);


                        //TRASLADAR ARCHIVO A DESTINO
                        string sourceFile = System.IO.Path.Combine(pathBandeja, df);
                        string destFile = System.IO.Path.Combine(pathFtpData , df);
                        System.IO.File.Copy(sourceFile, destFile, true);


                        //ACTUALIZAR TABLAS 
                        sql0 = "update sistema_contadores set a_cierre_ftp=a_cierre_ftp+1";
                        comando1 = new MySqlCommand(sql0, cn, tr);
                        rt = comando1.ExecuteNonQuery();

                        sql0 = "select a_cierre_ftp from sistema_contadores";
                        comando1 = new MySqlCommand(sql0, cn, tr);
                        var v = comando1.ExecuteScalar();
                        if (v == null)
                        { 
                        }

                        var cierre = int.Parse(v.ToString());
                        var aCierre = cierre.ToString().Trim().PadLeft(10, '0');


                        //VENTAS
                        sql0 = "update ventas set cierre_ftp=?cierre where (tipo='01' or tipo='02' or tipo='03' or tipo='04') and cierre_ftp=''";
                        comando1 = new MySqlCommand(sql0, cn, tr);
                        comando1.Parameters.Clear();
                        comando1.Parameters.AddWithValue("?cierre", aCierre);
                        rt = comando1.ExecuteNonQuery();

                        sql0 = "update ventas_detalle set cierre_ftp=?cierre where (tipo='01' or tipo='02' or tipo='03' or tipo='04') and cierre_ftp=''";
                        comando1 = new MySqlCommand(sql0, cn, tr);
                        comando1.Parameters.Clear();
                        comando1.Parameters.AddWithValue("?cierre", aCierre);
                        rt = comando1.ExecuteNonQuery();


                        //CXC
                        sql0 = "update cxc set cierre_ftp=?cierre  where cierre_ftp=''";
                        comando1 = new MySqlCommand(sql0, cn, tr);
                        comando1.Parameters.Clear();
                        comando1.Parameters.AddWithValue("?cierre", aCierre);
                        rt = comando1.ExecuteNonQuery();

                        sql0 = "update cxc_recibos set cierre_ftp=?cierre  where cierre_ftp=''";
                        comando1 = new MySqlCommand(sql0, cn, tr);
                        comando1.Parameters.Clear();
                        comando1.Parameters.AddWithValue("?cierre", aCierre);
                        rt = comando1.ExecuteNonQuery();

                        sql0 = "update cxc_documentos set cierre_ftp=?cierre  where cierre_ftp=''";
                        comando1 = new MySqlCommand(sql0, cn, tr);
                        comando1.Parameters.Clear();
                        comando1.Parameters.AddWithValue("?cierre", aCierre);
                        rt = comando1.ExecuteNonQuery();

                        sql0 = "update cxc_medio_pago set cierre_ftp=?cierre  where cierre_ftp=''";
                        comando1 = new MySqlCommand(sql0, cn, tr);
                        comando1.Parameters.Clear();
                        comando1.Parameters.AddWithValue("?cierre", aCierre);
                        rt = comando1.ExecuteNonQuery();


                        //PRODUCTOS KARDEX
                        sql0 = "update productos_kardex set cierre_ftp=?cierre  where modulo='Ventas' and cierre_ftp=''";
                        comando1 = new MySqlCommand(sql0, cn, tr);
                        comando1.Parameters.Clear();
                        comando1.Parameters.AddWithValue("?cierre", aCierre);
                        rt = comando1.ExecuteNonQuery();


                        //POS JORNADAS 
                        sql0 = "update pos_jornadas set cierre_ftp=?cierre where cierre_ftp=''";
                        comando1 = new MySqlCommand(sql0, cn, tr);
                        comando1.Parameters.Clear();
                        comando1.Parameters.AddWithValue("?cierre", aCierre);
                        rt = comando1.ExecuteNonQuery();


                        //POS ARQUEO
                        sql0 = "update pos_arqueo set cierre_ftp=?cierre where cierre_ftp=''";
                        comando1 = new MySqlCommand(sql0, cn, tr);
                        comando1.Parameters.Clear();
                        comando1.Parameters.AddWithValue("?cierre", aCierre);
                        rt = comando1.ExecuteNonQuery();


                        //COMPRAS
                        sql0 = "update compras set cierre_ftp=?cierre where tipo='05' and cierre_ftp=''";
                        comando1 = new MySqlCommand(sql0, cn, tr);
                        comando1.Parameters.Clear();
                        comando1.Parameters.AddWithValue("?cierre", aCierre);
                        rt = comando1.ExecuteNonQuery();

                        sql0 = "update compras_detalle set cierre_ftp=?cierre where tipo='05' and cierre_ftp=''";
                        comando1 = new MySqlCommand(sql0, cn, tr);
                        comando1.Parameters.Clear();
                        comando1.Parameters.AddWithValue("?cierre", aCierre);
                        rt = comando1.ExecuteNonQuery();


                        //PRODUCTOS MOVIMIENTOS
                        sql0 = "update productos_movimientos set cierre_ftp=?cierre where tipo='05' and cierre_ftp=''";
                        comando1 = new MySqlCommand(sql0, cn, tr);
                        comando1.Parameters.Clear();
                        comando1.Parameters.AddWithValue("?cierre", aCierre);
                        rt = comando1.ExecuteNonQuery();

                        sql0 = "update productos_movimientos_detalle set cierre_ftp=?cierre where tipo='05' and cierre_ftp=''";
                        comando1 = new MySqlCommand(sql0, cn, tr);
                        comando1.Parameters.Clear();
                        comando1.Parameters.AddWithValue("?cierre", aCierre);
                        rt = comando1.ExecuteNonQuery();





                        tr.Commit();
                    }
                    catch (Exception ex1)
                    {
                        tr.Rollback();
                        result.Mensaje = ex1.Message;
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                    }
                };
            }
            catch (MySqlException ex2)
            {
                result.Mensaje = ex2.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }

        public DtoLib.Resultado Servidor_Principal_InsertarBoletin(string codigoSuc, string rutaArchivoTxt)
        {
            var result = new DtoLib.Resultado();

            try
            {
                using (var cn = new MySqlConnection(_cnn2.ConnectionString))
                {
                    cn.Open();

                    MySqlTransaction tr = null;
                    try
                    {
                        var sql0 = "";
                        MySqlCommand comando1;
                        var rt = -1;

                        var pathData = rutaArchivoTxt;

                        tr = cn.BeginTransaction();

                        //DESACTIVAR RESTRICCIONES FORANEAS
                        sql0 = "SET FOREIGN_KEY_CHECKS=0";
                        comando1 = new MySqlCommand(sql0, cn, tr);
                        rt = comando1.ExecuteNonQuery();


                        //LIMPIANDO TABLAS
                        sql0 = "delete from sistema_configuracion";
                        comando1 = new MySqlCommand(sql0, cn, tr);
                        rt = comando1.ExecuteNonQuery();

                        sql0 = "delete from usuarios_grupo_permisos";
                        comando1 = new MySqlCommand(sql0, cn, tr);
                        rt = comando1.ExecuteNonQuery();

                        sql0 = "delete from usuarios";
                        comando1 = new MySqlCommand(sql0, cn, tr);
                        rt = comando1.ExecuteNonQuery();

                        sql0 = "delete from usuarios_grupo";
                        comando1 = new MySqlCommand(sql0, cn, tr);
                        rt = comando1.ExecuteNonQuery();

                        sql0 = "delete from empresa";
                        comando1 = new MySqlCommand(sql0, cn, tr);
                        rt = comando1.ExecuteNonQuery();

                        sql0 = "delete from empresa_tasas";
                        comando1 = new MySqlCommand(sql0, cn, tr);
                        rt = comando1.ExecuteNonQuery();

                        sql0 = "delete from empresa_departamentos";
                        comando1 = new MySqlCommand(sql0, cn, tr);
                        rt = comando1.ExecuteNonQuery();

                        sql0 = "delete from productos_alterno";
                        comando1 = new MySqlCommand(sql0, cn, tr);
                        rt = comando1.ExecuteNonQuery();

                        sql0 = "delete from productos_deposito";
                        comando1 = new MySqlCommand(sql0, cn, tr);
                        rt = comando1.ExecuteNonQuery();

                        sql0 = "delete from productos";
                        comando1 = new MySqlCommand(sql0, cn, tr);
                        rt = comando1.ExecuteNonQuery();

                        sql0 = "delete from productos_grupo";
                        comando1 = new MySqlCommand(sql0, cn, tr);
                        rt = comando1.ExecuteNonQuery();

                        sql0 = "delete from productos_marca";
                        comando1 = new MySqlCommand(sql0, cn, tr);
                        rt = comando1.ExecuteNonQuery();

                        sql0 = "delete from productos_lista";
                        comando1 = new MySqlCommand(sql0, cn, tr);
                        rt = comando1.ExecuteNonQuery();

                        //

                        sql0 = "delete from proveedores_grupo";
                        comando1 = new MySqlCommand(sql0, cn, tr);
                        rt = comando1.ExecuteNonQuery();

                        sql0 = "delete from proveedores";
                        comando1 = new MySqlCommand(sql0, cn, tr);
                        rt = comando1.ExecuteNonQuery();

                        sql0 = "delete from compras_detalle WHERE TIPO='04'";
                        comando1 = new MySqlCommand(sql0, cn, tr);
                        rt = comando1.ExecuteNonQuery();

                        sql0 = "delete from compras WHERE TIPO='04'";
                        comando1 = new MySqlCommand(sql0, cn, tr);
                        rt = comando1.ExecuteNonQuery();

                        sql0 = "delete from productos_precios";
                        comando1 = new MySqlCommand(sql0, cn, tr);
                        rt = comando1.ExecuteNonQuery();

                        sql0 = "delete from clientes where auto > '0900000001'";
                        comando1 = new MySqlCommand(sql0, cn, tr);
                        rt = comando1.ExecuteNonQuery();

                        //

                        sql0 = "delete from productos_kardex where modulo <> 'Ventas'";
                        comando1 = new MySqlCommand(sql0, cn, tr);
                        rt = comando1.ExecuteNonQuery();

                        sql0 = "delete from empresa_grupo";
                        comando1 = new MySqlCommand(sql0, cn, tr);
                        rt = comando1.ExecuteNonQuery();

                        sql0 = "delete from empresa_sucursal";
                        comando1 = new MySqlCommand(sql0, cn, tr);
                        rt = comando1.ExecuteNonQuery();

                        sql0 = "delete from sistema_menu";
                        comando1 = new MySqlCommand(sql0, cn, tr);
                        rt = comando1.ExecuteNonQuery();

                        sql0 = "delete from sistema_funciones";
                        comando1 = new MySqlCommand(sql0, cn, tr);
                        rt = comando1.ExecuteNonQuery();

                        sql0 = "delete from empresa_depositos";
                        comando1 = new MySqlCommand(sql0, cn, tr);
                        rt = comando1.ExecuteNonQuery();

                        sql0 = "delete from productos_movimientos_detalle";
                        comando1 = new MySqlCommand(sql0, cn, tr);
                        rt = comando1.ExecuteNonQuery();

                        sql0 = "delete from productos_movimientos";
                        comando1 = new MySqlCommand(sql0, cn, tr);
                        rt = comando1.ExecuteNonQuery();


                        // PROCESO DE INSERTAR
                        sql0 = "load data infile \"" + pathData + "/sistema_configuracion.txt\" into table sistema_configuracion";
                        comando1 = new MySqlCommand(sql0, cn, tr);
                        rt = comando1.ExecuteNonQuery();

                        sql0 = "load data infile \"" + pathData + "/usuarios_grupo.txt\" into table usuarios_grupo";
                        comando1 = new MySqlCommand(sql0, cn, tr);
                        rt = comando1.ExecuteNonQuery();

                        sql0 = "load data infile \"" + pathData + "/usuarios.txt\" into table usuarios";
                        comando1 = new MySqlCommand(sql0, cn, tr);
                        rt = comando1.ExecuteNonQuery();

                        sql0 = "load data infile \"" + pathData + "/usuarios_grupo_permisos.txt\" into table usuarios_grupo_permisos";
                        comando1 = new MySqlCommand(sql0, cn, tr);
                        rt = comando1.ExecuteNonQuery();

                        sql0 = "load data infile \"" + pathData + "/empresa.txt\" into table empresa";
                        comando1 = new MySqlCommand(sql0, cn, tr);
                        rt = comando1.ExecuteNonQuery();

                        sql0 = "load data infile \"" + pathData + "/empresa_tasas.txt\" into table empresa_tasas";
                        comando1 = new MySqlCommand(sql0, cn, tr);
                        rt = comando1.ExecuteNonQuery();

                        sql0 = "load data infile \"" + pathData + "/empresa_departamentos.txt\" into table empresa_departamentos";
                        comando1 = new MySqlCommand(sql0, cn, tr);
                        rt = comando1.ExecuteNonQuery();

                        sql0 = "load data infile \"" + pathData + "/productos_alterno.txt\" into table productos_alterno";
                        comando1 = new MySqlCommand(sql0, cn, tr);
                        rt = comando1.ExecuteNonQuery();

                        sql0 = "load data infile \"" + pathData + "/productos_deposito.txt\" into table productos_deposito";
                        comando1 = new MySqlCommand(sql0, cn, tr);
                        rt = comando1.ExecuteNonQuery();

                        sql0 = "load data infile \"" + pathData + "/productos.txt\" into table productos";
                        comando1 = new MySqlCommand(sql0, cn, tr);
                        rt = comando1.ExecuteNonQuery();

                        sql0 = "load data infile \"" + pathData + "/productos_grupo.txt\" into table productos_grupo";
                        comando1 = new MySqlCommand(sql0, cn, tr);
                        rt = comando1.ExecuteNonQuery();

                        sql0 = "load data infile \"" + pathData + "/productos_marca.txt\" into table productos_marca";
                        comando1 = new MySqlCommand(sql0, cn, tr);
                        rt = comando1.ExecuteNonQuery();

                        sql0 = "load data infile \"" + pathData + "/productos_lista.txt\" into table productos_lista";
                        comando1 = new MySqlCommand(sql0, cn, tr);
                        rt = comando1.ExecuteNonQuery();

                        //

                        sql0 = "load data infile \"" + pathData + "/proveeodres_grupo.txt\" into table proveedores_grupo";
                        comando1 = new MySqlCommand(sql0, cn, tr);
                        rt = comando1.ExecuteNonQuery();

                        sql0 = "load data infile \"" + pathData + "/proveeodres.txt\" into table proveedores";
                        comando1 = new MySqlCommand(sql0, cn, tr);
                        rt = comando1.ExecuteNonQuery();

                        sql0 = "load data infile \"" + pathData + "/compras.txt\" into table compras";
                        comando1 = new MySqlCommand(sql0, cn, tr);
                        rt = comando1.ExecuteNonQuery();

                        sql0 = "load data infile \"" + pathData + "/compras_detalle.txt\" into table compras_detalle";
                        comando1 = new MySqlCommand(sql0, cn, tr);
                        rt = comando1.ExecuteNonQuery();

                        sql0 = "load data infile \"" + pathData + "/productos_precios.txt\" into table productos_precios";
                        comando1 = new MySqlCommand(sql0, cn, tr);
                        rt = comando1.ExecuteNonQuery();
                       
                        //

                        sql0 = "load data infile \"" + pathData + "/clientes.txt\" into table clientes";
                        comando1 = new MySqlCommand(sql0, cn, tr);
                        rt = comando1.ExecuteNonQuery();

                        //

                        sql0 = "load data infile \"" + pathData + "/productos_kardex.txt\" into table productos_kardex";
                        comando1 = new MySqlCommand(sql0, cn, tr);
                        rt = comando1.ExecuteNonQuery();

                        sql0 = "delete from productos_kardex where codigo_sucursal<>?codigoSucursal";
                        comando1 = new MySqlCommand(sql0, cn, tr);
                        comando1.Parameters.Clear();
                        comando1.Parameters.AddWithValue("?codigoSucursal", codigoSuc);
                        rt = comando1.ExecuteNonQuery();

                        sql0 = "load data infile \"" + pathData + "/empresa_grupo.txt\" into table empresa_grupo";
                        comando1 = new MySqlCommand(sql0, cn, tr);
                        rt = comando1.ExecuteNonQuery();

                        sql0 = "load data infile \"" + pathData + "/empresa_sucursal.txt\" into table empresa_sucursal";
                        comando1 = new MySqlCommand(sql0, cn, tr);
                        rt = comando1.ExecuteNonQuery();

                        sql0 = "load data infile \"" + pathData + "/sistema_menu.txt\" into table sistema_menu";
                        comando1 = new MySqlCommand(sql0, cn, tr);
                        rt = comando1.ExecuteNonQuery();

                        sql0 = "load data infile \"" + pathData + "/sistema_funciones.txt\" into table sistema_funciones";
                        comando1 = new MySqlCommand(sql0, cn, tr);
                        rt = comando1.ExecuteNonQuery();

                        sql0 = "load data infile \"" + pathData + "/empresa_depositos.txt\" into table empresa_depositos";
                        comando1 = new MySqlCommand(sql0, cn, tr);
                        rt = comando1.ExecuteNonQuery();

                        sql0 = "delete from empresa_depositos where codigo_sucursal<>?codigoSucursal";
                        comando1 = new MySqlCommand(sql0, cn, tr);
                        comando1.Parameters.Clear();
                        comando1.Parameters.AddWithValue("?codigoSucursal", codigoSuc);
                        rt = comando1.ExecuteNonQuery();

                        sql0 = "load data infile \"" + pathData + "/productos_movimientos.txt\" into table productos_movimientos";
                        comando1 = new MySqlCommand(sql0, cn, tr);
                        rt = comando1.ExecuteNonQuery();

                        sql0 = "delete from productos_movimientos where codigo_sucursal<>?codigoSucursal";
                        comando1 = new MySqlCommand(sql0, cn, tr);
                        comando1.Parameters.Clear();
                        comando1.Parameters.AddWithValue("?codigoSucursal", codigoSuc);
                        rt = comando1.ExecuteNonQuery();

                        sql0 = "load data infile \"" + pathData + "/productos_movimientos_detalle.txt\" into table productos_movimientos_detalle";
                        comando1 = new MySqlCommand(sql0, cn, tr);
                        rt = comando1.ExecuteNonQuery();

                        sql0 = "update sistema set deposito_principal=(select autodepositoprincipal from empresa_sucursal where codigo=?codigoSucursal)";
                        comando1 = new MySqlCommand(sql0, cn, tr);
                        comando1.Parameters.Clear();
                        comando1.Parameters.AddWithValue("?codigoSucursal", codigoSuc);
                        rt = comando1.ExecuteNonQuery();

                        //ESTADO NORMAL RESTRICCIONES FORANEAS
                        sql0 = "SET FOREIGN_KEY_CHECKS=1";
                        comando1 = new MySqlCommand(sql0, cn, tr);
                        rt = comando1.ExecuteNonQuery();

                        tr.Commit();
                    }
                    catch (Exception ex1)
                    {
                        tr.Rollback();
                        result.Mensaje = ex1.Message;
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                    }
                };
            }
            catch (MySqlException ex2)
            {
                result.Mensaje = ex2.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }

    }

}