using BOMBEROSCR.F5.UI.Models;
using BOMBEROSCR.F5.UI.OneSignal;
using BOMBEROSCR.F5.UI.UrlHelper;
using BOMBEROSCR.F5.UTL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BOMBEROSCR.F5.UI.Controllers
{
    public class DispositivosController : Controller
    {

        private readonly UrlDispositivos _url;

        // GET: Dispositivos
        public ActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// Enviar la notificacion hacia OneSignal
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult sendNotification(List<String> devices, NotificacionesViewModel notificationVM)
        {
            OneSignalNotifications oneSignal = new OneSignalNotifications();

            Dictionary<string, Object> parametros = oneSignal.buildNotification(devices, notificationVM);

            String respuesta = oneSignal.sendRequest(ConfigurationManager.AppSettings["ONE_SIGNAL_API_KEY"], parametros);

            return Json(respuesta);

        }

        /// <summary>
        /// Consulta los dispositivos al API , equivale al metodo refreshDevices del F5 en java
        /// </summary>
        /// <returns>Lista de carpeta</returns>  
        public void RecargarDispositivos()
        {
            //List<sigDispositivoViewModels> listaDispositivos = await GeneralApi.
            //Obtenerlista<sigDispositivoViewModels>(_url.RecargarDispositivos);

            //return Json(listaDispositivos);
        }

        //sendMessage
        //Permite enviar una notificacion
        public void EnviarMensaje()
        {

        }

        //sendMessageToDevice
        //Enviar un mensaje a todos los dispositivos
        public void EnviaMensajeADispositivo()
        {

        }

        //sendMessageAllActive
        //Envia un mensaje a todos los dispositivos activos
        public void EnviaMensajeATodosLosDispositivosActivos()
        {

        }
    }
}