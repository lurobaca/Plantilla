using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOMBEROSCR.F5.ENT
{
    public class Notificacion
    {
        public string Destinatarios { get; set; }
        public string Asunto { get; set; }
        public string Mensaje { get; set; }
        public int Severidad { get; set; }
        public string Adjuntos { get; set; }
    }
}
