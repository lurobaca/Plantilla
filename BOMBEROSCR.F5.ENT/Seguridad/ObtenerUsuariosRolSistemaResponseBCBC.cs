using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOMBEROSCR.F5.ENT.Seguridad
{
	public class ObtenerUsuariosRolSistemaResponseBCBC: ResponseBCBCBase
	{
		public UsuarioRol[] Items
		{
			get;
			set;
		}
	}
}
