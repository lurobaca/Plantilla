using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOMBEROSCR.F5.ENT
{
    public class UsuarioAutenticado
    {
        public string Usuario { get; set; }

        public string NombreUsuario { get; set; }

        public string ContraseniaEncriptada { get; set; }

        public string IP { get; set; }

        public string Token { get; set; }

        public string ContextInfo { get; set; }

        public List<OpcionesMenu> OpcionesMenu { get; set; }

        public string IdUsuario { get; set; }

        public string SessionID { get; set; }
        public ParametroBase Rol { get; set; }

    }
}
