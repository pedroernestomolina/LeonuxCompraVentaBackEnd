using LibEntityInventario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProvLibInventario
{
    
    public partial class Provider : ILibInventario.IProvider
    {

        public DtoLib.ResultadoLista<DtoLibInventario.Proveedor.Resumen> Proveedor_GetLista(DtoLibInventario.Proveedor.Filtro filtro)
        {
            var rt = new DtoLib.ResultadoLista<DtoLibInventario.Proveedor.Resumen>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var q = cnn.proveedores.ToList();

                    if (filtro.cadena != "")
                    {
                        var cad = filtro.cadena.Trim().ToUpper();
                        if (cad.Substring(0, 1) == "*")
                        {
                            cad = cad.Substring(1);
                            q = q.Where(w => w.razon_social.Contains(cad)).ToList();
                        }
                        else
                        {
                            q = q.Where(w =>
                            {
                                var r = w.razon_social.Trim().ToUpper();
                                if (r.Length >= cad.Length && r.Substring(0, cad.Length) == cad)
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

                    var list = new List<DtoLibInventario.Proveedor.Resumen>();
                    if (q != null)
                    {
                        if (q.Count() > 0)
                        {
                            list = q.Select(s =>
                            {
                                var r = new DtoLibInventario.Proveedor.Resumen()
                                {
                                    auto = s.auto,
                                    codigo = s.codigo,
                                    nombreRazonSocial=s.razon_social,
                                    ciRif=s.ci_rif,
                                };
                                return r;
                            }).ToList();
                        }
                    }
                    rt.Lista = list;
                }
            }
            catch (Exception e)
            {
                rt.Mensaje = e.Message;
                rt.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return rt;
        }

    }

}