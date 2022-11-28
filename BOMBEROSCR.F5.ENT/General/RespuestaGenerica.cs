using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOMBEROSCR.F5.ENT
{
    public class RespuestaGenerica<T>
    {
        public int CodigoRespuesta { get; set; }

        public string DescripcionRespuesta { get; set; }

        public List<T> ListaGenerica { get; set; }

        public T ObjetoGenerico { get; set; }
    }
}
