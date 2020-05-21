using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Globalization;
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
        public string GrupoAuto { get; set; }
        public string GrupoDescripcion { get; set; }
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
            var list4 = new List<DataFiscal>();
            var list5 = new List<DataDeposito>();
            var list6 = new List<DataVendedor>();
            var list7 = new List<DataMedioCobro>();
            var list8 = new List<DataCobrador>();
            var list9 = new List<DataTransporte>();
            var exito = false;
            var factorCambio=0.0m;
            var clave1 = "";
            var clave2 = "";
            var clave3 = "";

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
                        "pm4.nombre as pv4Nombre, pm4.decimales as pv4Decimales " +
                        "from productos as p join empresa_departamentos as d on p.auto_departamento=d.auto " +
                        " join productos_grupo as g on p.auto_grupo=g.auto " +
                        " join productos_marca as m on p.auto_marca=m.auto " +
                        " join productos_medida as pm on p.auto_precio_1=pm.auto " +
                        " join productos_medida as pm2 on p.auto_precio_2=pm2.auto " +
                        " join productos_medida as pm3 on p.auto_precio_3=pm3.auto " +
                        " join productos_medida as pm4 on p.auto_precio_4=pm4.auto ";
                    var sql2 = "select * from productos_alterno";
                    var sql3 = "select u.*,g.nombre as nombreGrupo from usuarios as u " +
                        " join usuarios_grupo as g on u.auto_grupo=g.auto";
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


                    var sql1 = "select now()";
                    MySqlCommand comando1 = new MySqlCommand(sql1);
                    comando1.Connection = cn;
                    cn.Open();
                    var fechaServ = comando1.ExecuteScalar().ToString();

                    MySqlCommand comando = new MySqlCommand(sql);
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
                        var isDivisa = reader.GetString("estatus_divisa").Trim().ToUpper();
                        var isOferta = reader.GetString("estatus_oferta").Trim().ToUpper();
                        var isPesado = reader.GetString("estatus_pesado").Trim().ToUpper();

                        nr.IsActivo = isActivo == "ACTIVO" ? true : false;
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

                        nr.Precio_1 = pv1;
                        nr.Precio_2 = pv2;
                        nr.Precio_3 = pv3;
                        nr.Precio_4 = pv4;

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
                        nr.GrupoAuto = reader.GetString("auto_grupo");
                        nr.GrupoDescripcion = reader.GetString("nombreGrupo");
                        list3.Add(nr);
                    }
                    reader.Close();

                    MySqlCommand comando4 = new MySqlCommand(sql4);
                    comando4.Connection = cn;
                    reader = comando4.ExecuteReader();
                    while (reader.Read())
                    {
                        var nr = new DataFiscal();
                        nr.Auto = reader.GetString("auto");
                        nr.Nombre= reader.GetString("nombre");
                        nr.Tasa = reader.GetDecimal("tasa");
                        list4.Add(nr);
                    }
                    reader.Close();

                    MySqlCommand comando5 = new MySqlCommand(sql5);
                    comando5.Connection = cn;
                    var _cm5=comando5.ExecuteScalar();
                    if (_cm5!=null)
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
                        nr.Descripcion= reader.GetString("nombre");
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
                        clave2=_cmC.ToString();
                    }

                    MySqlCommand comandoD = new MySqlCommand(sqlD);
                    comandoD.Connection = cn;
                    var _cmD = comandoD.ExecuteScalar();
                    if (_cmD != null)
                    {
                        clave3=_cmD.ToString();
                    }

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
                        var prdList = cnn.Producto.ToList();
                        var prdBarra = cnn.ProductoBarra.ToList();
                        var usuList = cnn.UsuarioGrupo.ToList();
                        var fiscalList = cnn.Fiscal.ToList();
                        var vendedorList = cnn.Vendedor.ToList();
                        var medioList = cnn.MedioCobro.ToList();
                        var depositoList = cnn.Deposito.ToList();
                        var cobradorList = cnn.Cobrador.ToList();
                        var transporteList = cnn.Transporte.ToList();

                        
                        var sistema = cnn.Sistema.Find("0000000001");
                        if (factorCambio>0)
                        {
                            sistema.factorCambio =factorCambio;
                            sistema.clave1 = clave1;
                            sistema.clave2 = clave2;
                            sistema.clave3 = clave3;
                            cnn.SaveChanges();
                        }

                        cnn.Configuration.AutoDetectChangesEnabled = false;
                        cnn.Producto.RemoveRange(prdList);
                        cnn.SaveChanges();
                        cnn.ProductoBarra.RemoveRange(prdBarra);
                        cnn.SaveChanges();
                        cnn.UsuarioGrupo.RemoveRange(usuList);
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
                                costo = r.Costo,
                                costoPromedio = r.CostoPromedio,
                                costoPromedioUnidad = r.CostoPromedioUnidad,
                                costoUnidad = r.CostoUnidad,
                                dec_1 = r.Precio_1.Decimales,
                                dec_2 = r.Precio_2.Decimales,
                                dec_3 = r.Precio_3.Decimales,
                                dec_4 = r.Precio_4.Decimales,
                                departamento = r.NombreDepartamento,
                                descripcionPrd = "",
                                dias_Empaque_Garantia = r.DiasEmpaqueGarantia,
                                emp_1 = r.Precio_1.Empaque,
                                emp_2 = r.Precio_2.Empaque,
                                emp_3 = r.Precio_3.Empaque,
                                emp_4 = r.Precio_4.Empaque,
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
                                referencia = r.Referencia,
                                tasaImpuesto = r.TasaImpuesto,
                                autoDepartamento=r.AutoDepartamento,
                                autoGrupo=r.AutoGrupo,
                                autoSubGrupo=r.AutoSubGrupo,
                                autoMarca=r.AutoMarca,
                                autoTasaIva=r.AutoTasaIva,
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
                            var nr = new LibEntitySqLitePosOffLine.UsuarioGrupo()
                            {
                                autoGrupo = r.GrupoAuto,
                                autoUsuario = r.UsuarioAuto,
                                grupoDescripcion = r.GrupoDescripcion,
                                usuarioClave = r.UsuarioClave,
                                usuarioCodigo = r.UsuarioCodigo,
                                usuarioDescripcion = r.UsuarioDescripcion,
                            };
                            listUsuario.Add(nr);
                        }
                        cnn.UsuarioGrupo.AddRange(listUsuario);
                        cnn.SaveChanges();


                        var listFiscal = new List<LibEntitySqLitePosOffLine.Fiscal>();
                        foreach (var r in list4)
                        {
                            var nr = new LibEntitySqLitePosOffLine.Fiscal()
                            {
                                auto=r.Auto,
                                nombre=r.Nombre,
                                tasa=r.Tasa,
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
                                codigo= r.Codigo,
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
                                descripcion= r.Descripcion,
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


                        cnn.Configuration.AutoDetectChangesEnabled = true;
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

    }

}