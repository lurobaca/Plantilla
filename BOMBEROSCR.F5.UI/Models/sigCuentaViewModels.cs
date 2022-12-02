using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BOMBEROSCR.F5.UI.Models
{
    /// <summary>
    /// Contiene la estrucutra de la tabla de la base de datos [sigCuenta]
    /// </summary>
    public class sigCuentaViewModels
    {
        public long conCuenta { get; set; }
        public string desCorreoAndroid { get; set; }
        public string cocUsuario { get; set; }
        public string estCuenta { get; set; }
        public string codIOS { get; set; }
    }
}