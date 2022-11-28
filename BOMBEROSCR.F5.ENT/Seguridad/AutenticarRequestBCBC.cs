using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOMBEROSCR.F5.ENT.Seguridad
{
	public partial class AutenticarRequestBCBC : RequestBCBCSeguridad
	{
		//public AutenticarRequestBCBC()
		//{
		//	this.EN_ACTIVE_DIRECTORY = false;
		//	this.LDAP = "";
		//}

		public string CLAVE
		{
			get;
			set;
		}

		public bool EN_ACTIVE_DIRECTORY
		{
			get;
			set;
		}

		public string LDAP
		{
			get;
			set;
		}

		public bool OMITIR_BITACORA
		{
			get;
			set;
		}
	}
}
