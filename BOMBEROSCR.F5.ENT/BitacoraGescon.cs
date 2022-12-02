using BOMBEROSCR.F5.ENT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOMBEROSCR.F5.ENT
{
    public class BitacoraGescon : UsuarioAutenticado
    {
        public string Fecha { get; set; }
        public string Hora { get; set; }
        public string Proyecto { get; set; }
        public string Clase { get; set; }
        public string Metodo { get; set; }
        public string ParamentrosEntrada { get; set; }
        public string ParamentrosSalida { get; set; }
        public string Mensaje { get; set; }
        public string DetalleError { get; set; }
    }
}
