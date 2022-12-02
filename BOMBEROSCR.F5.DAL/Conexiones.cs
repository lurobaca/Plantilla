using BOMBEROSCR.F5.ENT;
using BOMBEROSCR.F5.UTL;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Reflection;

namespace BOMBEROSCR.F5.DAL
{
    class Conexiones : Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase
    {
        internal DbConnection conn;
        public DbTransaction transaccion;
        public Database dbsigae = CrearBaseDatosSIGAE();
        public static UsuarioAutenticado UsuarioLogueado;
        public Conexiones ConexionF5;
        public bool IndEsTransaccionLogica;

        public Conexiones(UsuarioAutenticado pUsuario, bool pIndEsTransaccion = false) : base(UTL.Seguridad.Decriptar(ConfigurationManager.ConnectionStrings["BDSIMAV"].ConnectionString))
        {
            UsuarioLogueado = pUsuario;
            IndEsTransaccionLogica = pIndEsTransaccion;
            CrearBaseDatosSIMAV();
        }

        public override IDataReader ExecuteReader(DbCommand command) => ConexionF5.ExecuteReader(command, transaccion);
        public override int ExecuteNonQuery(DbCommand command) => ConexionF5.ExecuteNonQuery(command, transaccion);

        internal static Database CrearBaseDatos()
        {
            try
            {
                string connectionString = UTL.Seguridad.Decriptar(ConfigurationManager.ConnectionStrings["BDF5"].ConnectionString);
                if (connectionString.CompareTo("ErrorAlConectarse") == 0)
                {
                    return null;
                }
                Database db = new Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(connectionString);
                return db;
            }
            catch (Exception ex)
            {
                BitacoraDAL.RegistrarBitacora(ObtenerDatosBitacora(ex));
                return null;
            }
        }

        internal void CrearBaseDatosSIMAV()
        {
            try
            {
                ConexionF5 = this;
                ActualizarContextInfo();
            }
            catch (Exception ex)
            {
                BitacoraDAL.RegistrarBitacora(ObtenerDatosBitacora(ex));
                ConexionF5 = null;
            }
        }

        internal static Database CrearBaseDatosSIGAE()
        {
            try
            {
                string connectionString = UTL.Seguridad.Decriptar(ConfigurationManager.ConnectionStrings["BDSIGAE"].ConnectionString);
                if (connectionString.CompareTo("ErrorAlConectarse") == 0)
                {
                    return null;
                }
                Database db = new Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(connectionString);
                return db;
            }
            catch (Exception ex)
            {
                BitacoraDAL.RegistrarBitacora(ObtenerDatosBitacora(ex));
                return null;
            }
        }

        internal DbConnection CrearConexion()
        {
            try
            {
                conn = ConexionF5.CreateConnection();
                return conn;
            }
            catch (Exception ex)
            {
                BitacoraDAL.RegistrarBitacora(ObtenerDatosBitacora(ex));
                return null;
            }
        }

        internal void AbrirConexion()
        {
            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {
                BitacoraDAL.RegistrarBitacora(ObtenerDatosBitacora(ex));
            }
        }

        internal void CerrarConexion()
        {
            try
            {
                conn.Close();
            }
            catch (Exception ex)
            {
                BitacoraDAL.RegistrarBitacora(ObtenerDatosBitacora(ex));
            }
        }

        public void CrearTransaccion()
        {
            try
            {
                CrearConexion();
                AbrirConexion();
                transaccion = conn.BeginTransaction();

            }
            catch (Exception ex)
            {
                BitacoraDAL.RegistrarBitacora(ObtenerDatosBitacora(ex));
            }
        }

        public void FinalizarTransaccion(bool pCierreManual = false)
        {
            try
            {
                if (conn.State == System.Data.ConnectionState.Open && (!IndEsTransaccionLogica || pCierreManual))
                {
                    transaccion.Commit();
                    CerrarConexion();
                }

            }
            catch (Exception ex)
            {
                BitacoraDAL.RegistrarBitacora(ObtenerDatosBitacora(ex));
            }
        }

        public void CancelarTransaccion()
        {
            try
            {
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    transaccion.Rollback();
                    CerrarConexion();
                }

            }
            catch (Exception ex)
            {
                BitacoraDAL.RegistrarBitacora(ObtenerDatosBitacora(ex));
            }
        }
        private static BitacoraGescon ObtenerDatosBitacora(Exception ex)
        {
            var bitacora = new BitacoraGescon();
            bitacora.Proyecto = Assembly.GetExecutingAssembly().GetName().Name;
            bitacora.Metodo = MethodBase.GetCurrentMethod().Name;
            bitacora.Clase = "Conexiones.cs";
            bitacora.DetalleError = ex.StackTrace;
            bitacora.Mensaje = ex.Message;

            return bitacora;
        }

        public RespuestaGenerica<T> ObtenerRespuestaConexion<T>(RespuestaGenerica<T> pRespuesta)
        {
            pRespuesta.CodigoRespuesta = Constantes.Codigos.ErrorNoControlado;
            pRespuesta.DescripcionRespuesta = "No se pudo establecer conexión con la base de datos";
            return pRespuesta;
        }

        public RespuestaGenericaRechazo<T> ObtenerRespuestaConexionRechazo<T>(RespuestaGenericaRechazo<T> pRespuesta)
        {
            pRespuesta.CodigoRespuesta = Constantes.Codigos.ErrorNoControlado;
            pRespuesta.DescripcionRespuesta = "No se pudo establecer conexión con la base de datos";
            return pRespuesta;
        }

        private RespuestaGenerica<string> ActualizarContextInfo()
        {
            var respuesta = new RespuestaGenerica<string>();
            var ParamCadenaInformacion = "@pContextInfoCast";
            BitacoraGescon bitacora = new BitacoraGescon();

            try
            {
                #region CargaDatosEntradaBitacora
                bitacora.Proyecto = Assembly.GetExecutingAssembly().GetName().Name;
                bitacora.Metodo = MethodBase.GetCurrentMethod().Name;
                bitacora.Clase = MethodBase.GetCurrentMethod().DeclaringType.Name;
                #endregion

                CrearTransaccion();

                var str3 = UsuarioLogueado == null ? string.Empty : UsuarioLogueado.Usuario + "|" + UsuarioLogueado == null ? string.Empty : UsuarioLogueado.SessionID;
                var str1 = "";
                str1 += "DECLARE @Ctx varbinary(128)   ";
                str1 = str1 + "SELECT @Ctx = CONVERT(varbinary(128),'" + str3 + "')   ";
                str1 += "SET CONTEXT_INFO @Ctx  ";

                //DbCommand cmd = pIndEsTransaccion ? ConexionSIMAVTrans.GetSqlStringCommand(str1) : ConexionSIMAV.GetSqlStringCommand(str1);
                DbCommand cmd = ConexionF5.GetSqlStringCommand(str1);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = str1;


                ConexionF5.ExecuteNonQuery(cmd, transaccion);
            }
            catch (Exception mensaje)
            {
                respuesta.CodigoRespuesta = Constantes.Codigos.ErrorNoControlado;
                respuesta.DescripcionRespuesta = mensaje.Message;

                #region CargaDatosCatchBitacora
                BitacoraDAL.BitacoraSalidaCatch(mensaje.Message, mensaje.StackTrace, ref bitacora);
                #endregion
            }
            return respuesta;
        }

        public RespuestaGenerica<string> ObtenerContextInfo()
        {
            var respuesta = new RespuestaGenerica<string>();
            BitacoraGescon bitacora = new BitacoraGescon();

            try
            {
                #region CargaDatosEntradaBitacora
                bitacora.Proyecto = Assembly.GetExecutingAssembly().GetName().Name;
                bitacora.Metodo = MethodBase.GetCurrentMethod().Name;
                bitacora.Clase = MethodBase.GetCurrentMethod().DeclaringType.Name;
                #endregion

                DbCommand cmd = ConexionF5.GetStoredProcCommand(Recursos.PA_ObtenerContextInfo,
                     string.Empty,
                     string.Empty);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters[Constantes.Parametros.CodigoError].Direction = ParameterDirection.Output;
                cmd.Parameters[Constantes.Parametros.MensajeError].Direction = ParameterDirection.Output;


                using (IDataReader reader = ConexionF5.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        respuesta.ObjetoGenerico = ValidacionesDeDatos.ConvertirString(reader["contextInfo"]);
                    }
                }

                respuesta.CodigoRespuesta = ValidacionesDeDatos.ConvertirInt32(cmd.Parameters[Constantes.Parametros.CodigoError].Value);
                respuesta.DescripcionRespuesta = ValidacionesDeDatos.ConvertirString(cmd.Parameters[Constantes.Parametros.MensajeError].Value);

            }
            catch (Exception mensaje)
            {
                respuesta.CodigoRespuesta = Constantes.Codigos.ErrorNoControlado;
                respuesta.DescripcionRespuesta = mensaje.Message;

                #region CargaDatosCatchBitacora
                BitacoraDAL.BitacoraSalidaCatch(mensaje.Message, mensaje.StackTrace, ref bitacora);
                #endregion
            }
            return respuesta;
        }

    }
}
