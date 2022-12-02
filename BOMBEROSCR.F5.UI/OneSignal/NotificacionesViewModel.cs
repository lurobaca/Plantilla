using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BOMBEROSCR.F5.UI.OneSignal
{
    public class NotificacionesViewModel
    {
        //Nombre del movimiento
        public String movementName { get; set; }
        public String detail { get; set; }
        public int fireStationId { get; set; }
        public int? incidentId { get; set; }
        public DateTime date { get; set; }
        public String unitInternalId { get; set; }
        public String statusId { get; set; }
        public int unitId { get; set; }
        public String movementId { get; set; }
        public String idMensaje { get; set; }
        public long notificationId { get; set; }
    }
}