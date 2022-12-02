using System.Globalization;
using System.Linq;
using System.Net;

namespace BOMBEROSCR.F5.UTL
{
    public class HelperRespuesta
    {
        /// <summary>
        /// Código de estado de respuesta de la solicitud HTTP
        /// </summary>
        public HttpStatusCode StatusCode { get; set; } //CodigoEstadoHttp

        /// <summary>
        /// Mensaje devuelto en caso de ser necesario
        /// </summary>
        public string Message { get; set; } //Mensaje

        /// <summary>
        /// Resultado de la solicitud (true = correcto, false= incorrecto)
        /// </summary>
        public bool IsSuccess { get; set; } //Resultado

        /// <summary>
        /// Objeto de tipo dinámico para devolver cualquier valor que se necesite
        /// </summary>
        public object Object { get; set; } //Objeto

    }

    //public static class Respuesta
    //{
    //    public static HelperRespuesta Error(TipoMensaje tipoMensaje, string helper = null)
    //    {
    //        return new HelperRespuesta
    //        {
    //            IsSuccess = false,
    //            Message = HelperMensaje.Crear(tipoMensaje, helper),
    //            StatusCode = HttpStatusCode.BadRequest,
    //        };
    //    }

    //    public static HelperRespuesta Exitosa(TipoMensaje tipoMensaje)
    //    {
    //        return new HelperRespuesta
    //        {
    //            IsSuccess = true,
    //            Message = HelperMensaje.Crear(tipoMensaje),
    //            StatusCode = HttpStatusCode.OK,
    //        };
    //    }

    //}

}
