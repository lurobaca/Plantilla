using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOMBEROSCR.F5.ENT.Seguridad
{
	public partial class AutenticarResponseBCBC: ResponseBCBCBase
	{
		public bool Resultado
		{
			get;
			set;
		}

		public string IdFuncionario
		{
			get;
			set;
		}

		public System.Nullable<int> ClasificacionSIABO
		{
			get;
			set;
		}

		public string DistinguishedName
		{
			get;
			set;
		}

		public string NombreUsuario
		{
			get;
			set;
		}

		public string CentroCosto
		{
			get;
			set;
		}

		public int IntentosRestantes
		{
			get;
			set;
		}

		public int ContrasennaProximaVencer
		{
			get;
			set;
		}

		public bool VenceHoy
		{
			get;
			set;
		}

		public int IdEstacion
		{
			get;
			set;
		}

		public string DesEstacion
		{
			get;
			set;
		}
	}
}