using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BOMBEROSCR.F5.UI.Models
{
    public class sigDispositivoViewModels
    {
        //[Display(Name = "Código")]
        public long conDispositivo { get; set; }
        public int codRegistro { get; set; }
        public string indActivo { get; set; }
        public string desVersion { get; set; }
        public string desModelo { get; set; }
        public DateTime fecHoraRegistroTelefono { get; set; }
        public DateTime fecHoraRegistroServidor { get; set; }
        public string desFabricante { get; set; }
        public string desMensaje { get; set; }
        public bool? indNotificaTodosEstados { get; set; }
        public bool? indNotificaTodasEstaciones { get; set; }
        public long? numVersionActualizacion { get; set; }
        public long? conCuenta { get; set; }
        public string desSistemaOperativo { get; set; }
        public string desCorreoAndroid { get; set; }
        public string codIOS { get; set; }

    }
}