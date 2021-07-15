using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProvSqLitePosOffLine
{
    
    public partial class Provider : IPosOffLine.IProvider
    {

        public DtoLib.ResultadoEntidad<DtoLibPosOffLine.Cliente.Ficha> Cliente(int id)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibPosOffLine.Cliente.Ficha>();

            try
            {
                using (var cnn = new LibEntitySqLitePosOffLine.LeonuxPosOffLineEntities(_cnn.ConnectionString))
                {

                    var entCliente = cnn.Cliente.Find(id);
                    if (entCliente == null)
                    {
                        result.Mensaje = "CLIENTE [ ID ] NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    };

                    var nr = new DtoLibPosOffLine.Cliente.Ficha()
                    {
                        Id = (int)entCliente.id,
                        CiRif = entCliente.cirif,
                        NombreRazaonSocial = entCliente.nombreRazonSocial,
                        DirFiscal = entCliente.dirFiscal,
                        Telefono = entCliente.telefono,
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

        public DtoLib.ResultadoEntidad<DtoLibPosOffLine.Cliente.Ficha> Cliente_BuscarPorCiRif(string ciRif)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibPosOffLine.Cliente.Ficha>();

            try
            {
                using (var cnn = new LibEntitySqLitePosOffLine.LeonuxPosOffLineEntities(_cnn.ConnectionString))
                {

                    var entCliente = cnn.Cliente.FirstOrDefault(w => w.cirif.Trim().ToUpper() == ciRif.Trim().ToUpper());
                    if (entCliente == null)
                    {
                        return result;
                    };

                    var nr = new DtoLibPosOffLine.Cliente.Ficha()
                    {
                        Id = (int)entCliente.id,
                        CiRif = entCliente.cirif,
                        NombreRazaonSocial = entCliente.nombreRazonSocial,
                        DirFiscal = entCliente.dirFiscal,
                        Telefono = entCliente.telefono,
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

        public DtoLib.ResultadoId Cliente_Agregar(DtoLibPosOffLine.Cliente.Agregar ficha)
        {
            var result = new DtoLib.ResultadoId();

            try
            {
                using (var cnn = new LibEntitySqLitePosOffLine.LeonuxPosOffLineEntities(_cnn.ConnectionString))
                {

                    var entCliente = new LibEntitySqLitePosOffLine.Cliente()
                    {
                        cirif= ficha.CiRif,
                        nombreRazonSocial= ficha.NombreRazaonSocial,
                        dirFiscal=ficha.DirFiscal,
                        telefono=ficha.Telefono,
                    };
                    cnn.Cliente.Add(entCliente);
                    cnn.SaveChanges();
                    result.Id =(int) entCliente.id;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }

        public DtoLib.ResultadoLista<DtoLibPosOffLine.Cliente.Resumen> Cliente_Lista(string xfiltro)
        {
            var result = new DtoLib.ResultadoLista<DtoLibPosOffLine.Cliente.Resumen>();

            try
            {
                using (var cnn = new LibEntitySqLitePosOffLine.LeonuxPosOffLineEntities(_cnn.ConnectionString))
                {
                    var filtro = xfiltro.Trim().ToUpper();
                    var q = cnn.Cliente.ToList();
                    if (filtro != "")
                    {
                        if (filtro.Substring(0, 1) == "*")
                        {
                            filtro = filtro.Substring(1);
                            q = q.Where(w => w.nombreRazonSocial.Contains(filtro)).ToList();
                        }
                        else
                        {
                            q = q.Where(w =>
                            {
                                var r = w.nombreRazonSocial.Trim().ToUpper();
                                if (r.Length >= filtro.Length && r.Substring(0, filtro.Length) == filtro)
                                {
                                    return true;
                                }
                                else
                                {
                                    return false;
                                }
                            }).ToList();
                        }
                    }

                    var list = new List<DtoLibPosOffLine.Cliente.Resumen>();
                    if (q != null)
                    {
                        if (q.Count() > 0)
                        {
                            result.Lista = q.Select(s =>
                            {
                                var r = new DtoLibPosOffLine.Cliente.Resumen()
                                {
                                     Id=(int)s.id,
                                     CiRif=s.cirif,
                                     NombreRazaonSocial=s.nombreRazonSocial
                                };
                                return r;
                            }).ToList();
                        }
                        else
                        {
                            result.Lista = list;
                        }
                    }
                    else
                    {
                        result.Lista = list;
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

        public DtoLib.ResultadoLista<DtoLibPosOffLine.Cliente.ExportarData.Ficha> Cliente_ExportarData(DtoLibPosOffLine.Cliente.ExportarData.Filtro filtro)
        {
            var result = new DtoLib.ResultadoLista<DtoLibPosOffLine.Cliente.ExportarData.Ficha>();

            try
            {
                using (var cnn = new LibEntitySqLitePosOffLine.LeonuxPosOffLineEntities(_cnn.ConnectionString))
                {
                    var xsql_1= "select nombreRazonSocial, ciRif, dirFiscal, telefono ";
                    var xsql_2 = " from cliente ";
                    var xsql= xsql_1 +xsql_2;
                    var list = cnn.Database.SqlQuery<DtoLibPosOffLine.Cliente.ExportarData.Ficha>(xsql).ToList();
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