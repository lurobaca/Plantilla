using BOMBEROSCR.F5.ENT;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Data;
using System.Data.Common;
using System.Reflection;

namespace BOMBEROSCR.F5.DAL
{
    public class BitacoraDAL
    {

        public static void RegistrarBitacora(BitacoraGescon bitacora)
        {
            try
            {
                Database db = Conexiones.CrearBaseDatos();
                DbCommand cmd = db.GetStoredProcCommand(Recursos.PA_RegistrarBitacora,
                ValidacionesDeDatos.ConvertirStringBitacora(bitacora.Usuario),
                ValidacionesDeDatos.ConvertirStringBitacora(bitacora.IP),
                ValidacionesDeDatos.ConvertirStringBitacora(DateTime.Now.ToShortDateString()),
                ValidacionesDeDatos.ConvertirStringBitacora(DateTime.Now.ToShortTimeString()),
                ValidacionesDeDatos.ConvertirStringBitacora(bitacora.Proyecto),
                ValidacionesDeDatos.ConvertirStringBitacora(bitacora.Clase),
                ValidacionesDeDatos.ConvertirStringBitacora(bitacora.Metodo),
                ValidacionesDeDatos.ConvertirStringBitacora(bitacora.ParamentrosEntrada),
                ValidacionesDeDatos.ConvertirStringBitacora(bitacora.ParamentrosSalida),
                ValidacionesDeDatos.ConvertirStringBitacora(bitacora.Mensaje),
                ValidacionesDeDatos.ConvertirStringBitacora(bitacora.DetalleError));

                cmd.CommandType = CommandType.StoredProcedure;
                db.ExecuteNonQuery(cmd);
            }
            catch (Exception)
            {

            }
        }



        public static BitacoraGescon BitacoraSalidaCatch(string message, string stackTrace, ref BitacoraGescon bitacora)
        {
            bitacora.Mensaje = message;
            bitacora.DetalleError = stackTrace;

            return bitacora;
        }

        public static BitacoraGescon BitacoraSalida(string parametrossalida, string descripcionRespuesta, string detalleerror, ref BitacoraGescon bitacora)
        {
            bitacora.ParamentrosSalida = parametrossalida;
            bitacora.Mensaje = descripcionRespuesta;
            bitacora.DetalleError = detalleerror;

            return bitacora;
        }

        public static BitacoraGescon BitacoraEntrada(UsuarioAutenticado pInfoUsuario, string datosentrada, ref BitacoraGescon bitacora)
        {
            bitacora.Usuario = pInfoUsuario.Usuario;
            bitacora.IP = pInfoUsuario.IP;
            bitacora.Proyecto = Assembly.GetExecutingAssembly().GetName().Name;
            bitacora.ParamentrosEntrada = datosentrada;

            return bitacora;
        }

        public static BitacoraGescon BitacoraEntrada(string usuario, string ip, string datosentrada, ref BitacoraGescon bitacora)
        {
            bitacora.Usuario = usuario;
            bitacora.IP = ip;
            bitacora.Proyecto = Assembly.GetExecutingAssembly().GetName().Name;
            bitacora.ParamentrosEntrada = datosentrada;

            return bitacora;
        }
    }
}
