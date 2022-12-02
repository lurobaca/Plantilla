using BOMBEROSCR.F5.UI.Models;
using BOMBEROSCR.F5.UI.OneSignal;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace BOMBEROSCR.F5.UTL
{
    /// <summary>
    /// Codigo Obtenido de la pagina de OneSignal c# (.NET standard) https://documentation.onesignal.com/v7.0/reference/create-notification#example-code---create-notification
    /// Send to a specific segment or all subscribers - Create notification
    /// Enviar una notificacion a un segmento o a todos los susbcritores 
    /// </summary>
    public class OneSignalNotifications
    {
        /// <summary>
        /// Contruye la estructura de los parametros que necesita ONESIGNAL para mandar un notificacion
        /// </summary>
        /// <param name="devices"></param>
        /// <param name="notificationViewModel"></param>
        public Dictionary<string, Object> buildNotification(List<String> devices, NotificacionesViewModel notificationViewModel)
        {

            Dictionary<string, string> data = new Dictionary<string, string>();

            data["header"] = notificationViewModel.movementName;
            data["message"] = generateMessage(notificationViewModel);
            data["fireStation"] = notificationViewModel.fireStationId.ToString();
            data["status"] = notificationViewModel.statusId;
            data["unit"] = notificationViewModel.unitId.ToString();
            data["notificationId"] = notificationViewModel.notificationId.ToString();
            data["date"] = notificationViewModel.date.ToString();

            if (notificationViewModel.incidentId != null)
            {
                data["incident"] = notificationViewModel.incidentId.ToString();
            }

            Dictionary<String, String> headings = new Dictionary<string, string>();

            headings["es"] = notificationViewModel.movementName;
            headings["en"] = notificationViewModel.movementName;

            Dictionary<String, String> contents = new Dictionary<string, string>();

            contents["es"] = generateMessage(notificationViewModel);
            contents["en"] = generateMessage(notificationViewModel);

            Dictionary<String, Object> parametro = new Dictionary<String, Object>();

            parametro["app_id"] = ConfigurationManager.AppSettings["ONE_SIGNAL_APP_ID"];
            parametro["data"] = data;
            parametro["headings"] = headings;
            parametro["contents"] = contents;
            parametro["content_available"] = "1";
            parametro["ios_badype"] = "Increase";
            parametro["ios_badgeCount"] = 1;
            parametro["android_sound"] = "pop";
            parametro["include_player_ids"] = devices;

            return parametro;
        }

        /// <summary>
        /// Permite generar un mensaje de texto optimizando el uso de la memoria del sistema
        /// </summary>
        /// <param name="notificationViewModel"></param>
        /// <returns></returns>
        public String generateMessage(NotificacionesViewModel notificationViewModel)
        {
            StringBuilder sb = new StringBuilder();
            if (notificationViewModel.detail != null && notificationViewModel.detail.Trim().Length > 0)
            {
                sb.Append(notificationViewModel.detail);
            }
            return sb.ToString();
        }

        /// <summary>
        /// Contiene la estructura requerida por OneSignal para crear una notificacion
        /// </summary>
        /// <param name="Authorizatio">Contiene el API_KEY brindado por OneSignal al suscribir la aplicacion</param>
        /// <param name="app_id">Contiene el APP_ID brindado por OneSignal al suscribir la aplicacion</param>
        public string sendRequest(string Authorizatio,Dictionary<String, Object> parametro)
        {
            
            var request = WebRequest.Create("https://onesignal.com/api/v1/notifications") as HttpWebRequest;

            request.KeepAlive = true;
            request.Method = "POST";
            request.ContentType = "application/json; charset=utf-8";

            request.Headers.Add("authorization", Authorizatio);

            //byte[] byteArray = Encoding.UTF8.GetBytes("{"
            //                                        + "\"app_id\": \"" + app_id + "\","
            //                                        + "\"contents\": {\"en\": \"English Message\"},"
            //                                        + "\"included_segments\": [\"Subscribed Users\"]}");

            //Obtiene un JSON de los parametros enviados en el Dictionario
            var ParametrosEnJSON = new JavaScriptSerializer().Serialize(parametro.ToDictionary(item => item.Key.ToString(), item => item.Value.ToString()));

            //Se asignan el json con los parametros para ser enviados ah OneSignal
            byte[] byteArray = Encoding.UTF8.GetBytes(ParametrosEnJSON);

            string responseContent = null;
            HttpStatusCode CodigoDeRespuesta= new HttpStatusCode();
            HttpWebResponse response=null;
            try
            {
                using (var writer = request.GetRequestStream())
                {
                    writer.Write(byteArray, 0, byteArray.Length);
                }

                using (response = request.GetResponse() as HttpWebResponse)
                {
                    CodigoDeRespuesta = response.StatusCode;
               
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        responseContent = reader.ReadToEnd();
                     
                    }
                }
            }
            catch (WebException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(new StreamReader(ex.Response.GetResponseStream()).ReadToEnd());
            }

            System.Diagnostics.Debug.WriteLine(responseContent);
            //Valida si la respuesta fue exitosa
           String jsonrespuesta= ValidaRespuesta(CodigoDeRespuesta, response);

            return jsonrespuesta;
        }

        /// <summary>
        ///  //Valida si la respuesta fue exitosa
        /// </summary>
        /// <param name="CodigoDeRespuesta">Contiene el Status Code de la llamada al api de Onesignal</param>
        public string ValidaRespuesta(HttpStatusCode CodigoDeRespuesta, HttpWebResponse respuesta) {

            string jsonResponse = "";
           
            if(CodigoDeRespuesta  == HttpStatusCode.OK || CodigoDeRespuesta  == HttpStatusCode.BadRequest){
                jsonResponse = JsonConvert.SerializeObject(respuesta.GetResponseStream());
            }
            else {
                jsonResponse = JsonConvert.SerializeObject(respuesta.GetResponseStream());
            }
           

            return jsonResponse;
        }

    }

}


