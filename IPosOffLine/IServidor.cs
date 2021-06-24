using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IPosOffLine
{
    
    public interface IServidor
    {

        DtoLib.Resultado Servidor_Test();
        DtoLib.Resultado Servidor_ActualizarData();
        DtoLib.ResultadoEntidad<int> Productos_TotalItems();
        DtoLib.ResultadoEntidad<DtoLibPosOffLine.Servidor.RecogerDataEnviar.Ficha> Servidor_RecogerDataEnviar();
        DtoLib.Resultado Servidor_EnviarData(DtoLibPosOffLine.Servidor.EnviarData.Ficha ficha);
        DtoLib.Resultado Servidor_Principal_CrearBoletin(string pathDestino);
        DtoLib.Resultado Servidor_Principal_InsertarCierre(string pathOrigen);
        DtoLib.Resultado Servidor_Principal_PreprararCierre(string codigoEmpresa, string pathLeonuxBandeja, string pathLeonuxFtpBandejaData);
        DtoLib.Resultado Servidor_Principal_InsertarBoletin(string codigoSuc, string rutaArchivoTxt);

        //
        DtoLib.Resultado Servidor_AgregarCampoTabla_Sistema_Habilitar_Precio_VentaMayor();

    }

}