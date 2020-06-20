using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProvSqLitePosOffLine
{
    
    public partial class Provider : IPosOffLine.IProvider
    {

        static EntityConnectionStringBuilder _cnn;
        static MySqlConnectionStringBuilder _cnn2;
        static string _bdRemotoInstancia;
        static string _bdRemotaBaseDatos;
        static string _bdLocal;


        public Provider(string pathDB) 
        {
            _bdLocal = pathDB;
            _cnn = new EntityConnectionStringBuilder()
            {
                Metadata = @"res://*/ModelPos.csdl|res://*/ModelPos.ssdl|res://*/ModelPos.msl",
                Provider = @"System.Data.SQLite.EF6",
                ProviderConnectionString = new SqlConnectionStringBuilder()
                {
                    DataSource =pathDB,
                }.ConnectionString
            };

            //var _usuario = "root";
            //var _password = "123";
            //var _instancia = "localhost";
            //var _baseDatos = "bogagalpon";
            //var cadena="Database="+_baseDatos+"; Data Source="+_instancia+"; User Id=" +_usuario+"; Password="+_password+"";
            //_cnn2 = new MySqlConnectionStringBuilder();
            //_cnn2.Database = _baseDatos;
            //_cnn2.UserID = _usuario;
            //_cnn2.Password = _password;
            //_cnn2.Server = _instancia;
        }

        public void setServidorRemoto(string instancia, string baseDatos)
        {
            _bdRemotoInstancia = instancia;
            _bdRemotaBaseDatos = baseDatos;

            var _usuario = "root";
            var _password = "123";
            var _instancia = instancia ;
            var _baseDatos = baseDatos;
            var cadena = "Database=" + _baseDatos + "; Data Source=" + _instancia + "; User Id=" + _usuario + "; Password=" + _password + "";
            _cnn2 = new MySqlConnectionStringBuilder();
            _cnn2.Database = _baseDatos;
            _cnn2.UserID = _usuario;
            _cnn2.Password = _password;
            _cnn2.Server = _instancia;
        }

        public DtoLib.ResultadoEntidad<DateTime> FechaServidor()
        {
            var result = new DtoLib.ResultadoEntidad<DateTime>();

            try
            {
                using (var ctx = new LibEntitySqLitePosOffLine.LeonuxPosOffLineEntities(_cnn.ConnectionString))
                {
                    var fechaSistema = ctx.Database.SqlQuery<DateTime>("select date('now')").FirstOrDefault();
                    result.Entidad = fechaSistema.Date;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }

        public DtoLib.ResultadoEntidad<DtoLibPosOffLine.Sistema.InformacionBD.Ficha> InformacionBD()
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibPosOffLine.Sistema.InformacionBD.Ficha>();

            var inf = new DtoLibPosOffLine.Sistema.InformacionBD.Ficha()
            {
                 BD_Remota= _bdRemotoInstancia+"\\"+_bdRemotaBaseDatos,
                 BD_Local=_bdLocal,
            };
            result.Entidad = inf;

            return result;
        }

        public DtoLib.Resultado Inicializar_BdLocal()
        {
            var result = new DtoLib.Resultado();

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
                    var serieList = cnn.Serie.ToList();
                    var clienteList = cnn.Cliente.ToList();
                    var ventaPagoList = cnn.VentaPago.ToList();
                    var ventaDetalleList = cnn.VentaDetalle.ToList();
                    var ventaList = cnn.Venta.ToList();
                    var operadorList = cnn.Operador.ToList();
                    var jornadaList = cnn.Jornada.ToList();
                    var pendienteList = cnn.Pendiente.ToList();
                    var itemList = cnn.Item.ToList();
                    var conceptoInvList = cnn.MovConceptoInv.ToList();


                    var empresa = cnn.Empresa.Find("0000000001");
                    if (empresa == null)
                    {
                        result.Mensaje = "REGISTRO CONTROL EMPRESA NO ENCONTRADO, VERIFIQUE POR VADOR";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }
                    empresa.cirif = "";
                    empresa.direccion = "";
                    empresa.nombre = "";
                    empresa.telefono = "";


                    var sistema = cnn.Sistema.Find("0000000001");
                    if (sistema == null) 
                    {
                        result.Mensaje = "REGISTRO CONTROL SISTEMA NO ENCONTRADO, VERIFIQUE POR VADOR";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }

                    sistema.factorCambio = 0;
                    sistema.clave1 = "";
                    sistema.clave2 = "";
                    sistema.clave3 = "";
                    sistema.sucursalCodigo = "";
                    sistema.equipoNumero = "";
                    sistema.tarifaAsignada = "";
                    sistema.EtiquetarPrecioPorTipoNegocio = "";
                    sistema.serieFactura = "";
                    sistema.serieNotaDebito= "";
                    sistema.serieNotaCredito= "";
                    sistema.serieNotaEntrega = "";

                    sistema.autoCobrador = "";
                    sistema.autoDeposito = "";
                    sistema.autoMedioPagoDivisa = "";
                    sistema.autoMedioPagoEfectivo = "";
                    sistema.autoMedioPagoElectronico = "";
                    sistema.autoMedioPagoOtro = "";
                    sistema.autoTransporte = "";
                    sistema.autoVendedor = "";

                    sistema.clavePos =0;
                    sistema.activarRepesaje = "";
                    sistema.activarBusquedaPorDescripcion = "";
                    sistema.limiteRepesajeInferior = 0;
                    sistema.limiteRepesajeSuperior = 0;

                    sistema.autoConceptoMovVenta = "";
                    sistema.autoConceptoMovDevVenta = "";
                    sistema.autoConceptoMovSalida = "";
                    cnn.SaveChanges();

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
                    cnn.Serie.RemoveRange(serieList);
                    cnn.Cliente.RemoveRange(clienteList);
                    cnn.VentaPago.RemoveRange(ventaPagoList);
                    cnn.VentaDetalle.RemoveRange(ventaDetalleList);
                    cnn.Venta.RemoveRange(ventaList);
                    cnn.Operador.RemoveRange(operadorList);
                    cnn.Jornada.RemoveRange(jornadaList);
                    cnn.Pendiente.RemoveRange(pendienteList);
                    cnn.Item.RemoveRange(itemList);
                    cnn.MovConceptoInv.RemoveRange(conceptoInvList);
                    cnn.SaveChanges();

                    cnn.Configuration.AutoDetectChangesEnabled = true;
                }

            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }

        public DtoLib.ResultadoEntidad<DtoLibPosOffLine.Empresa.Ficha> Empresa_Datos()
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibPosOffLine.Empresa.Ficha>();  

            try
            {
                using (var cnn = new LibEntitySqLitePosOffLine.LeonuxPosOffLineEntities(_cnn.ConnectionString))
                {
                    var ent = cnn.Empresa.Find("0000000001");
                    if (ent == null) 
                    {
                        result.Entidad = null;
                        result.Mensaje = "ENTIDAD EMPRESA NO ENCONTRADA";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }

                    var nr = new DtoLibPosOffLine.Empresa.Ficha()
                    {
                        Auto = ent.auto,
                        CiRif = ent.cirif,
                        DireccionFiscal = ent.direccion,
                        Nombre = ent.nombre,
                        Telefono = ent.telefono,
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

    }

}