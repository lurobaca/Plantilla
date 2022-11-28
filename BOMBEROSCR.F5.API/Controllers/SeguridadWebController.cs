using AutoMapper;
using BOMBEROSCR.F5.ENT;
using System;
using System.Web.Http;

namespace Plantilla.API.Controllers
{
	/// <summary>
	/// Clase <c>SeguridadWeb</c> se encarga de ser el puente entre el api y el servicio de seguridad.
	/// </summary>
	public class SeguridadWebController : ApiController
	{
		/// <summary>
		/// Método <c>Autenticar</c> autentica los datos del usuario.
		/// </summary>
		/// <param name="oAutenticar"></param>
		/// <returns>Objeto respuesta con los datos de verificación de la cuenta o el error presentado.</returns>
		public RespuestaGenerica<BOMBEROSCR.F5.ENT.Seguridad.AutenticarResponseBCBC> Autenticar(BOMBEROSCR.F5.ENT.Seguridad.AutenticarRequestBCBC oAutenticar)
		{
			var respuesta = new RespuestaGenerica<BOMBEROSCR.F5.ENT.Seguridad.AutenticarResponseBCBC>();
			try
			{
				using (var servicio = new WSISeguridad.SeguridadSoapClient())
				{
					var request = Mapper.Map<BOMBEROSCR.F5.ENT.Seguridad.AutenticarRequestBCBC, WSISeguridad.AutenticarRequestBCBC>(oAutenticar);
					var response = servicio.Autenticar(request);
					respuesta.ObjetoGenerico = Mapper.Map<WSISeguridad.AutenticarResponseBCBC, BOMBEROSCR.F5.ENT.Seguridad.AutenticarResponseBCBC>(response);
				}
			}
			catch (Exception ex)
			{
				respuesta.CodigoRespuesta = 99;
				respuesta.DescripcionRespuesta = ex.Message;
			}
			return respuesta;
		}

		/// <summary>
		/// Método <c>Modulos</c> obtiene los modulos de seguridad del sistema.
		/// </summary>
		/// <param name="oSeguridad"></param>
		/// <returns>Retorna los modulos del sistema.</returns>
		public RespuestaGenerica<BOMBEROSCR.F5.ENT.Seguridad.ModulosResponseBCBC> Modulos(BOMBEROSCR.F5.ENT.Seguridad.RequestBCBCSeguridad oSeguridad)
		{
			var respuesta = new RespuestaGenerica<BOMBEROSCR.F5.ENT.Seguridad.ModulosResponseBCBC>();
			try
			{
				using (var servicio = new WSISeguridad.SeguridadSoapClient())
				{
					var request = Mapper.Map<BOMBEROSCR.F5.ENT.Seguridad.RequestBCBCSeguridad, WSISeguridad.RequestBCBCSeguridad>(oSeguridad);
					var response = servicio.Modulos(request);
					respuesta.ObjetoGenerico = Mapper.Map<WSISeguridad.ModulosResponseBCBC, BOMBEROSCR.F5.ENT.Seguridad.ModulosResponseBCBC>(response);
				}
			}
			catch (Exception ex)
			{
				respuesta.CodigoRespuesta = 99;
				respuesta.DescripcionRespuesta = ex.Message;
			}
			return respuesta;
		}

		/// <summary>
		/// Método <c>ObtenerUsuariosRolSistema</c> obtiene los roles del usuario en el sistema.
		/// </summary>
		/// <param name="oRol"></param>
		/// <returns>Retorna lista de roles del usuario.</returns>
		public RespuestaGenerica<BOMBEROSCR.F5.ENT.Seguridad.ObtenerUsuariosRolSistemaResponseBCBC> ObtenerUsuariosRolSistema(BOMBEROSCR.F5.ENT.Seguridad.ObtenerUsuariosRolSistemaRequestBCBC oRol)
		{
			var respuesta = new RespuestaGenerica<BOMBEROSCR.F5.ENT.Seguridad.ObtenerUsuariosRolSistemaResponseBCBC>();
			try
			{
				using (var servicio = new WSISeguridad.SeguridadSoapClient())
				{
					var request = Mapper.Map<BOMBEROSCR.F5.ENT.Seguridad.ObtenerUsuariosRolSistemaRequestBCBC, WSISeguridad.ObtenerUsuariosRolSistemaRequestBCBC>(oRol);
					var response = servicio.ObtenerUsuariosRolSistema(request);
					respuesta.ObjetoGenerico = Mapper.Map<WSISeguridad.ObtenerUsuariosRolSistemaResponseBCBC,BOMBEROSCR.F5.ENT.Seguridad.ObtenerUsuariosRolSistemaResponseBCBC>(response);
				}
			}
			catch (Exception ex)
			{
				respuesta.CodigoRespuesta = 99;
				respuesta.DescripcionRespuesta = ex.Message;
			}
			return respuesta;
		}

		/// <summary>
		/// Método <c>Opciones</c> obtiene las opciones del sistema.
		/// </summary>
		/// <param name="p¿oSeguridad"></param>
		/// <returns>Retorna una lista de opciones del sistema .</returns>
		public RespuestaGenerica<BOMBEROSCR.F5.ENT.Seguridad.OpcionesResponseBCBC> Opciones(BOMBEROSCR.F5.ENT.Seguridad.OpcionesRequestBCBC oSeguridad)
		{
			var respuesta = new RespuestaGenerica<BOMBEROSCR.F5.ENT.Seguridad.OpcionesResponseBCBC>();
			try
			{
				using (var servicio = new WSISeguridad.SeguridadSoapClient())
				{
					var request = Mapper.Map<BOMBEROSCR.F5.ENT.Seguridad.OpcionesRequestBCBC, WSISeguridad.OpcionesRequestBCBC>(oSeguridad);
					var response = servicio.Opciones(request);
					respuesta.CodigoRespuesta = Convert.ToInt32(response.Codigo);
					respuesta.DescripcionRespuesta = response.Descripcion;
					respuesta.ObjetoGenerico = Mapper.Map<WSISeguridad.OpcionesResponseBCBC, BOMBEROSCR.F5.ENT.Seguridad.OpcionesResponseBCBC>(response);
				}
			}
			catch (Exception ex)
			{
				respuesta.CodigoRespuesta = 99;
				respuesta.DescripcionRespuesta = ex.Message;
			}
			return respuesta;
		}

		/// <summary>
		/// Método <c>GenerarOTP</c> obtiene un código OTP de seguridad.
		/// </summary>
		/// <param name="oSeguridad"></param>
		/// <returns>Retorna el código otp solicitado.</returns>
		public RespuestaGenerica<BOMBEROSCR.F5.ENT.Seguridad.DobleFactorResponseBCBC> GenerarOTP(BOMBEROSCR.F5.ENT.Seguridad.DobleFactorRequestBCBC oSeguridad)
		{
			var respuesta = new RespuestaGenerica<BOMBEROSCR.F5.ENT.Seguridad.DobleFactorResponseBCBC>();
			try
			{
				using (var servicio = new WSISeguridad.SeguridadSoapClient())
				{
					var request = Mapper.Map<BOMBEROSCR.F5.ENT.Seguridad.DobleFactorRequestBCBC, WSISeguridad.DobleFactorRequestBCBC>(oSeguridad);
					var response = servicio.GenerarOTP(request);
					respuesta.CodigoRespuesta = Convert.ToInt32(response.Codigo);
					respuesta.DescripcionRespuesta = response.Descripcion;
					respuesta.ObjetoGenerico = Mapper.Map<WSISeguridad.DobleFactorResponseBCBC, BOMBEROSCR.F5.ENT.Seguridad.DobleFactorResponseBCBC>(response);
				}
			}
			catch (Exception ex)
			{
				respuesta.CodigoRespuesta = 99;
				respuesta.DescripcionRespuesta = ex.Message;
			}
			return respuesta;
		}

		/// <summary>
		/// Método <c>VerificarOTP</c> válida si el OTP enviado está activo y válido.
		/// </summary>
		/// <param name="p"></param>
		/// <returns>Retorna un objeto respuesta aprobando o rechazando el OTP enviado.</returns>
		public RespuestaGenerica<BOMBEROSCR.F5.ENT.Seguridad.DobleFactorResponseBCBC> VerificarOTP(BOMBEROSCR.F5.ENT.Seguridad.DobleFactorRequestBCBC oSeguridad)
		{
			var respuesta = new RespuestaGenerica<BOMBEROSCR.F5.ENT.Seguridad.DobleFactorResponseBCBC>();
			try
			{
				using (var servicio = new WSISeguridad.SeguridadSoapClient())
				{
					var request = Mapper.Map<BOMBEROSCR.F5.ENT.Seguridad.DobleFactorRequestBCBC, WSISeguridad.DobleFactorRequestBCBC>(oSeguridad);
					var response = servicio.VerificarOTP(request);
					respuesta.CodigoRespuesta = Convert.ToInt32(response.Codigo);
					respuesta.DescripcionRespuesta = response.Descripcion;
					respuesta.ObjetoGenerico = Mapper.Map<WSISeguridad.DobleFactorResponseBCBC, BOMBEROSCR.F5.ENT.Seguridad.DobleFactorResponseBCBC>(response);
				}
			}
			catch (Exception ex)
			{
				respuesta.CodigoRespuesta = 99;
				respuesta.DescripcionRespuesta = ex.Message;
			}
			return respuesta;
		}
	}
}