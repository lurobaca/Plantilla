using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOMBEROSCR.F5.ENT.Seguridad
{
	public class RequestBCBCBase
	{
		public string codSistema
		{
			get;
			set;
		}

		public string Usuario
		{
			get;
			set;
		}

		public string Password
		{
			get;
			set;
		}

		public string IP
		{
			get;
			set;
		}
	}
}
