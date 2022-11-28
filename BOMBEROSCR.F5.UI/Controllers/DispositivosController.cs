using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BOMBEROSCR.F5.UI.Controllers
{
    public class DispositivosController : Controller
    {
        // GET: Dispositivos
        public ActionResult Index()
        {
            return View();
        }


        //refreshDevices
        //Actualiza todos los dispositivos disposibles
        public void RecargarDispositivos() { 
        
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