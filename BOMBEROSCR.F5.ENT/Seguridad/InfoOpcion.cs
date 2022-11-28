using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOMBEROSCR.F5.ENT.Seguridad
{
	public class InfoOpcion
	{
		public string IdOpcion
		{
			get;
			set;
		}

		public string IdSistema
		{
			get;
			set;
		}

		public string idSistemaPadre
		{
			get;
			set;
		}

		public string idOpcionPadre
		{
			get;
			set;
		}

		public int IdModulo
		{
			get;
			set;
		}

		public string DesOpcion
		{
			get;
			set;
		}

		public string DesUrlFormulario
		{
			get;
			set;
		}

		public string DesUrlAyuda
		{
			get;
			set;
		}

		public int IndOpcionMenu
		{
			get;
			set;
		}

		public System.Nullable<int> NumOrden
		{
			get;
			set;
		}
	}
}
