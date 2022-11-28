using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOMBEROSCR.F5.ENT.Seguridad
{
	public class OpcionesRequestBCBC: RequestBCBCSeguridad
	{
		public System.Nullable<int> ID_MODULO
		{
			get;
			set;
		}

		public bool OPCIONESMENU
		{
			get;
			set;
		}
	}
}
