using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOMBEROSCR.F5.ENT
{
    public class OpcionesMenu
    {
        public string IdOpcion { get; set; }
        public string IdSistema { get; set; }
        public string idSistemaPadre { get; set; }
        public string idOpcionPadre { get; set; }
        public Int32 IdModulo { get; set; }
        public string DesOpcion { get; set; }
        public string DesUrlFormulario { get; set; }
        public string DesUrlAyuda { get; set; }
        public Int32 IndOpcionMenu { get; set; }
        public string NumOrden { get; set; }
        public string Metodo { get; set; }
        public string Controller { get; set; }
        public string Area { get; set; }
        public Int16 AccionEspecial { get; set; }
    }
}
