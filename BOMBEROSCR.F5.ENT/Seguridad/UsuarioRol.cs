using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOMBEROSCR.F5.ENT.Seguridad
{
	public class UsuarioRol
	{
		public string Codigo
		{
			get;
			set;
		}

		public string Nombre
		{
			get;
			set;
		}

		public string PrimerApellido
		{
			get;
			set;
		}

		public string SegundoApellido
		{
			get;
			set;
		}

		public string TipoFuncionario
		{
			get;
			set;
		}

		public string Cedula
		{
			get;
			set;
		}

		public string CodigoERP
		{
			get;
			set;
		}

		public string CentroCosto
		{
			get;
			set;
		}

		public string Estado
		{
			get;
			set;
		}

		public Rol[] Roles
		{
			get;
			set;
		}
	}
}
