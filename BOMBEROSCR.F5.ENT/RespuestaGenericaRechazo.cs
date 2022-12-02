using BOMBEROSCR.F5.ENT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOMBEROSCR.F5.UTL
{
    public class RespuestaGenericaRechazo<T> : RespuestaGenerica<T>
    {
        public long ConRechazo { get; set; }
    }
}
