using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;

namespace BOMBEROSCR.F5.UTL
{
    /// <summary>
    /// Clase <c>GeneralApi</c> se encarga de consumir los métodos api.
    /// </summary>
    public static class GeneralApi
    {
		/// <summary>
		/// Método <c>Enviar</c> envía un request sin el token.
		/// </summary>
		/// <param name="urlMetodo"></param>
		/// <param name="objectRequest"></param>
		/// <param name="method"></param>
		/// <returns>Respuesta del request en forma de string.</returns>
		public static string Enviar<T>(string urlMetodo, T objectRequest, string method = "POST")
        {
            string result = "";

            try
            {
                //serializamos el objeto
                string json = JsonConvert.SerializeObject(objectRequest);

                //peticion
                WebRequest request = WebRequest.Create(urlMetodo);
                //headers
                request.Method = method;
                request.PreAuthenticate = true;
                request.ContentType = "application/json;charset=utf-8'";
                request.Headers.Add("Authorization", "");

                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    streamWriter.Write(json);
                    streamWriter.Flush();
                }

                var httpResponse = (HttpWebResponse)request.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                result = e.Message;
            }
            return result;
        }

        /// <summary>
        /// Método <c>Enviar</c> envía un request con un token.
        /// </summary>
        /// <param name="urlMetodo"></param>
        /// <param name="objectRequest"></param>
        /// <param name="method"></param>
        /// <returns>Respuesta del request en forma de string.</returns>
        public static string EnviarConToken<T>(string urlMetodo, T objectRequest, string token, string method = "POST")
        {
            string result = "";

            try
            {
                //serializamos el objeto
                string json = JsonConvert.SerializeObject(objectRequest);

                //Si la consulta viene de las app movil, el token viene en el header
                //var token = HttpContext.Current.Request.Headers.Get("Authorization");

                //Si la consulta viene del web, obtenemos el token desde la sesion del usuario logueado.
                //if (token == null)
                //    token = SessionApi.GetToken();

                //Si la consulta no viene de las apps ni de la web, significa que viene desde un proceso diario y de ser asi, se obtendra de la 
                //sesion el token generico, si sigue retornando null significa que no es una consulta autorizada
                //if (token == null)
                //    token = SessionApi.GetTokenGenerico();

                //peticion
                WebRequest request = WebRequest.Create(urlMetodo);
                //headers
                request.Method = method;
                request.PreAuthenticate = true;
                request.ContentType = "application/json;charset=utf-8'";
                request.Headers.Add("Authorization", token);
                //request.Timeout = 10000; //esto es opcional

                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    streamWriter.Write(json);
                    streamWriter.Flush();
                }

                var httpResponse = (HttpWebResponse)request.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                if (e.HResult == -2146233079)
                    result = "{'Error': 401 }";
                else
                    result = "{'Error': " + e.Message + " }";
            }
            return result;
        }

        /// <summary>
        /// Método <c>ObtenerConToken</c> obtiene un response utilizando un token.
        /// </summary>
        /// <param name="urlMetodo"></param>
        /// <returns>Respuesta del request en forma de string.</returns>
        public static string ObtenerConToken(string urlMetodo, string token)
        {
            string result = "";

            //Si la consulta viene de las app movil, el token viene en el header
            //var token = HttpContext.Current.Request.Headers.Get("Authorization");

            //Si la consulta viene del web, obtenemos el token desde la sesion del usuario logueado.
            //if (token == null)
            //    token = SessionApi.GetToken();

            //Si la consulta no viene de las apps ni de la web, significa que viene desde un proceso diario y de ser asi, se obtendra de la 
            //sesion el token generico, si sigue retornando null significa que no es una consulta autorizada
            //if (token == null)
            //    token = SessionApi.GetTokenGenerico();

            try
            {
                //peticion
                WebRequest request = WebRequest.Create(urlMetodo);
                //headers
                request.Method = "GET";
                request.PreAuthenticate = true;
                request.ContentType = "application/json;charset=utf-8'";
                request.Headers.Add("Authorization", token);
                request.Headers.Add("ContentType", "application/json");
                //request.Timeout = 10000; //esto es opcional

                var httpResponse = (HttpWebResponse)request.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                if (e.HResult == -2146233079)
                    result = "{'Error': 401 }";
                else
                    result = "{'Error': "+e.Message+" }";
            }
            return result;
        }

		/// <summary>
		/// Método <c>PostConVariableURL</c> obtiene un response utilizando un token y un post por medio de variable url sin objeto.
		/// </summary>
		/// <param name="urlMetodo"></param>
		/// <returns>Respuesta del request en forma de string.</returns>
		public static string PostConVariableURL(string urlMetodo, string token, string method = "POST")
            {
                string result = "";

                try
                {
                    //serializamos el objeto
                    string json = JsonConvert.SerializeObject("");

                    //Si la consulta viene de las app movil, el token viene en el header
                    //var token = HttpContext.Current.Request.Headers.Get("Authorization");

                    //Si la consulta viene del web, obtenemos el token desde la sesion del usuario logueado.
                    //if (token == null)
                    //    token = SessionApi.GetToken();

                    //Si la consulta no viene de las apps ni de la web, significa que viene desde un proceso diario y de ser asi, se obtendra de la 
                    //sesion el token generico, si sigue retornando null significa que no es una consulta autorizada
                    //if (token == null)
                    //    token = SessionApi.GetTokenGenerico();

                    //peticion
                    WebRequest request = WebRequest.Create(urlMetodo);
                    //headers
                    request.Method = method;
                    request.PreAuthenticate = true;
                    request.ContentType = "application/json;charset=utf-8'";
                    request.Headers.Add("Authorization", token);
                    //request.Timeout = 10000; //esto es opcional

                    using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                    {
                        streamWriter.Write(json);
                        streamWriter.Flush();
                    }

                    var httpResponse = (HttpWebResponse)request.GetResponse();
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        result = streamReader.ReadToEnd();
                    }
                }
                catch (Exception e)
                {
                    if (e.HResult == -2146233079)
                        result = "El usuario indicado no tiene acceso a este sistema.";
                    else
                        result = "{'Error': " + e.Message + " }";
                }
                return result;
            }
            public static string Obtener(string urlMetodo)
        {
            string result = "";

            try
            {
                //peticion
                WebRequest request = WebRequest.Create(urlMetodo);
                //headers
                request.Method = "GET";
                request.PreAuthenticate = true;
                request.ContentType = "application/json;charset=utf-8'";
                request.Headers.Add("ContentType", "application/json");
                //request.Timeout = 10000; //esto es opcional

                var httpResponse = (HttpWebResponse)request.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                if (e.HResult == -2146233079)
                    result = "{'Error': 401 }";
                else
                    result = "{'Error': " + e.Message + " }";
            }
            return result;
        }
    }
}