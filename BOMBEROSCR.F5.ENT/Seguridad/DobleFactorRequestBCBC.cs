using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOMBEROSCR.F5.ENT.Seguridad
{
	public class DobleFactorRequestBCBC: RequestBCBCBase
	{
		public string Cod_Usuario
		{
			get;
			set;
		}

		public string OTP_Encriptado
		{
			get;
			set;
		}

		public bool Ind_Reenvio
		{
			get;
			set;
		}
	}
}
