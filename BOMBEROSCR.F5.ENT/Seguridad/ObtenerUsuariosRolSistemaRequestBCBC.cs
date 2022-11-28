using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOMBEROSCR.F5.ENT.Seguridad
{
	public class ObtenerUsuariosRolSistemaRequestBCBC: RequestBCBCBase
	{
		public int IdModulo
		{
			get;
			set;
		}

		public long CodRol
		{
			get;
			set;
		}
	}
}
