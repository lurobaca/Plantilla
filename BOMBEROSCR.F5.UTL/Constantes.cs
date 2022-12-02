using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOMBEROSCR.F5.UTL
{
    public class Constantes
    {
        //public static string UrlBaseApi = "http://localhost:49894/";

		public class ParametrosSistema
        {
            public const string LlaveEncriptarUsuario = "Af-zHX%e;oyVrmA6";
            public const string LlaveEncriptar = "BLnYAsm+YsDMMtn2";
            public const string CodigoMonedaDolaresSigaeEnterprice = "D";
            public const string CodigoMonedaColonesSigaeEnterprice = "C";
            public const string FormatoFecha = "d/M/yyyy";
            public const string FormatoFechaHora = "d/M/yyyy HH:mm";
            public const string FormatoFechaHoraAMPM = "d/M/yyyy hh:mm tt";

			
            public class RolesSistema
            {
                public const string RolAdministradorTIC = "1";
            }

            public class ServiciosWindows
            {
                public const string ServicioNotificacionesAutomaticas = "1";
            }

            public class DiasSemana
            {
                public const string Lunes = "Lunes";
                public const string Martes = "Martes";
                public const string Miercoles = "Miércoles";
                public const string Jueves = "Jueves";
                public const string Viernes = "Viernes";
                public const string Sábado = "Sábado";
                public const string Domingo = "Domingo";
            }
        }

		//public class MetodosApiSeguridad
		//{
		//	public const string Autenticar = "Autenticar";
		//	public const string Modulos = "Modulos";
		//	public const string ObtenerUsuariosRolSistema = "ObtenerUsuariosRolSistema";
		//	public const string Opciones = "Opciones";
		//	public const string GenerarOTP = "GenerarOTP";
		//	public const string VerificarOTP = "VerificarOTP";
		//}

		public class CatalogoMoneda
        {
            public const string Colones = "CRC";
            public const string Dolares = "USD";
            public const string DescripcionColones = "Colones";
            public const string DescripcionDolares = "Dólares";
        }

        public class Codigos
        {
            public const int Exito = 0;
            public const int ErrorPersonalizado = 2;
            public const int Advertencia = 3;
            public const int AccionEspecial = 97;
            public const int SessionCaducada = 98;
            public const int ErrorNoControlado = 99;
        }

        public class Parametros
        {
            public const string CodigoError = "@pCodError";
            public const string MensajeError = "@pMensajeError";
            public const string ExisteDiferencia = "@pExisteDiferencia";
        }
    }
}
