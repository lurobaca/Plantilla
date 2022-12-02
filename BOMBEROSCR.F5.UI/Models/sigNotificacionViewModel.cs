using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BOMBEROSCR.F5.UI.Models
{
    /// <summary>
    /// Contiene la estrucutra de la tabla de la base de datos [sigNotificacion]
    /// </summary>
    public class sigNotificacionViewModel
    {
        public String conNotificacion { get; set; }
        public String desdetalle { get; set; }
        public String desEncabezado { get; set; }
        public String codInternoUnidad { get; set; }
        public String codMensaje { get; set; }
        public int numEstacion { get; set; }
        public String numEstadoDisponibilidad { get; set; }
        public int numUnidad { get; set; }
        public int numincidente { get; set; }
        public DateTime fecNotificacion { get; set; }
        public DateTime fecEnvioNotificacion { get; set; }
    }
}