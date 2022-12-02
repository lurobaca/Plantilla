using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Web;

namespace BOMBEROSCR.F5.UI.UrlHelper
{
    /// <summary>
    /// Permite crear el URL de los distintos metodos creados en el API
    /// </summary>

    public class UrlDispositivos
    {

        private readonly string _dominio = "";
        public const string Dispositivos = "/ api / Dispositivos /";
        private readonly string _url;

        public UrlDispositivos()
        {
           _url = $"{_dominio}{Dispositivos}";
        }

        public string RecargarDispositivos()
        {
            return $"{_url}RecargarDispositivos/";
        }

        public string EnviarMensaje()
        {
            return $"{_url}EnviarMensaje/";
        }

        public string EnviaMensajeADispositivo()
        {           
            return $"{_url}EnviaMensajeADispositivo/";
        }

        public string EnviaMensajeATodosLosDispositivosActivos()
        {
            return $"{_url}EnviaMensajeATodosLosDispositivosActivos/";
        }
    }

}