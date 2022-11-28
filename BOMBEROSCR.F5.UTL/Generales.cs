using BOMBEROSCR.F5.ENT;
using System;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;
using System.Xml.Serialization;
using static BOMBEROSCR.F5.UTL.Constantes;

namespace BOMBEROSCR.F5.UTL
{
  public class Generales
  {
    public DateTime validacionVaciosCamposFechaDesde(string campo)
    {
      CultureInfo us = new CultureInfo("en-US");
      string format = "yyyy-MM-dd";
      if (campo.Trim().Length == 0)
      {
        return DateTime.Now;
      }
      else
      {
        return DateTime.ParseExact(campo, format, us);
      }


    }
    public DateTime validacionVaciosCamposFechaHasta(string campo)
    {
      CultureInfo us = new CultureInfo("en-US");
      string format = "yyyy-MM-dd";
      if (campo.Trim().Length == 0)
      {
        return DateTime.Now;
      }
      else
      {
        return DateTime.ParseExact(campo, format, us);
      }


    }

    public string validacionVaciosCampos(string campo)
    {
      if (campo.Trim().Length == 0)
      {
        return " ";
      }
      else
      {
        return campo;
      }
    }

    /// <summary>
    /// Obtener array de bytes a partir de HttpPostedFileBase
    /// </summary>
    /// <param name="files"></param>
    /// <returns></returns>
    public static byte[] ObtenerArchivoBytes(HttpPostedFileBase files)
    {
      byte[] data = null;
      try
      {
        using (Stream inputStream = files.InputStream)
        {
          MemoryStream memoryStream = inputStream as MemoryStream;
          if (memoryStream == null)
          {
            memoryStream = new MemoryStream();
            inputStream.CopyTo(memoryStream);
          }
          data = memoryStream.ToArray();
        }
      }
      catch (Exception)
      {
      }
      return data;
    }
    public static byte[] ObtenerArchivoBytesDesdeRuta(string files)
    {
      byte[] data = null;
      try
      {
        if (!File.Exists(files))
        {
          return null;
        }
        using (Stream inputStream = new FileInfo(files).OpenRead())
        {
          MemoryStream memoryStream = inputStream as MemoryStream;
          if (memoryStream == null)
          {
            memoryStream = new MemoryStream();
            inputStream.CopyTo(memoryStream);
          }
          data = memoryStream.ToArray();
        }
      }
      catch (Exception)
      {
      }
      return data;
    }

    public static string ObtenerArchivoRutaDesdeBytes(byte[] fileBytes, string rutaCompleta)
    {
      string _path = @"" + rutaCompleta;
      using (Stream file = File.OpenWrite(_path))
      {
        file.Write(fileBytes, 0, fileBytes.Length);
      }
      if (File.Exists(_path))
      {
        return new FileInfo(_path).FullName;
      }
      else
        return null;
    }

    public static string SerializarObjetoXML(Type tipo, object pObjeto)
    {
      var serializer = new XmlSerializer(tipo);
      XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
      //Add an empty namespace and empty value
      ns.Add("", "");
      var settings = new XmlWriterSettings();
      settings.Indent = true;
      settings.OmitXmlDeclaration = true;

      using (var stream = new StringWriter())
      {
        using (var writer = XmlWriter.Create(stream, settings))
        {
          new XmlSerializer(tipo).Serialize(writer, pObjeto, ns);
          return stream.ToString();
        }
      }
    }

    public static T DeserializarObjetoXml<T>(string objectData)
    {
      return (T)XmlDeserializeFromString(objectData, typeof(T));
    }

    public static string SerializarObjetoXml(object objectInstance)
    {
      var serializer = new XmlSerializer(objectInstance.GetType());
      var sb = new StringBuilder();

      using (TextWriter writer = new StringWriter(sb))
      {
        serializer.Serialize(writer, objectInstance);
      }

      return sb.ToString();
    }

    public static object XmlDeserializeFromString(string objectData, Type type)
    {
      var serializer = new XmlSerializer(type);
      object result;

      using (TextReader reader = new StringReader(objectData))
      {
        result = serializer.Deserialize(reader);
      }

      return result;
    }

    /// <summary>
    /// Retorna la URL de repositorio de archivos si existe, si no la crea
    /// </summary>
    /// <param name="modulo"></param>
    /// <returns></returns>
    public static string ObtenerRutaRepositorio(string modulo)
    {
      try
      {
        string _URLREPO = ConfigurationManager.AppSettings["URL_Repositorio"] + "";
        if (_URLREPO != null)
        {
          _URLREPO += modulo;
          return _URLREPO;
        }
        else
          return _URLREPO;
      }
      catch (Exception)
      {
        return null;
      }
    }

    public static string ValidarFechaString(DateTime? pFecha)
    {
      if (pFecha == null || pFecha == DateTime.MinValue)
      {
        return null;
      }
      return pFecha.Value.ToString("dd/MM/yyyy");
    }

    public static string ValidarHoraString(DateTime? pFecha)
    {
      if (pFecha == null || pFecha == DateTime.MinValue)
      {
        return null;
      }
      return pFecha.Value.ToString("HH:mm:ss");
    }

    public static string ValidarHoraStringAMPM(DateTime? pFecha)
    {
      if (pFecha == null || pFecha == DateTime.MinValue)
      {
        return null;
      }
      return pFecha.Value.ToString("hh:mm tt");
    }

    public static string ValidarHoraStringAMPM(string pHora)
    {
      if (string.IsNullOrEmpty(pHora))
      {
        return null;
      }

      var hora = pHora.Substring(0, 5);
      var fecha = string.Concat(ValidarFechaString(DateTime.Now), " ", hora);
      var horaAconvertir = DateTime.ParseExact(fecha, ParametrosSistema.FormatoFechaHora, null);

      return ValidarHoraStringAMPM(horaAconvertir);
    }

    public static string ValidarFechaHoraString(DateTime? pFecha)
    {
      if (pFecha == null || pFecha == DateTime.MinValue)
      {
        return null;
      }
      return pFecha.Value.ToString("dd/MM/yyyy hh:mm tt");
    }

    public static string ValidarDecimalString(decimal pNumero)
    {
      var provider = new CultureInfo("en-US");
      if (pNumero == -1)
      {
        return null;
      }
      return pNumero.ToString("0.##", provider);
    }

    public static string ValidarIntString(int pNumero)
    {
      if (pNumero == -1)
      {
        return null;
      }
      return pNumero.ToString();
    }

    public static decimal ValidarStringDecimal(string pValor)
    {
      var style = NumberStyles.Number;
      var provider = new CultureInfo("en-US");

      try
      {
        return decimal.Parse(string.IsNullOrEmpty(pValor) ? "0" : pValor, style, provider);
      }
      catch (FormatException)
      {
        throw;
      }
    }

        public static decimal ConvertirStringDecimal(string pValor)
        {
            pValor = pValor.Substring(1);
            pValor = pValor.Replace(",", "");

            try
            {
                return decimal.Parse(string.IsNullOrEmpty(pValor) ? "0" : pValor);
            }
            catch (FormatException)
            {
                throw;
            }
        }

        public static decimal ValidarStringInt(string pValor)
    {
      try
      {
        return int.Parse(pValor);
      }
      catch (FormatException)
      {
        throw;
      }
    }

    public static string ValidarStringTotalHoras(string pValor)
    {
      try
      {
        var fechaHora = Convert.ToDateTime(pValor);

        var horas = fechaHora.Hour;
        var minutosString = ValidarIntString(fechaHora.Minute);
        var minutos = ValidarStringDecimal(minutosString);

        var totalHoras = (horas + (minutos / 60));

        return ValidarDecimalString(totalHoras);
      }
      catch (FormatException)
      {
        throw;
      }
    }

    public static string ValidarTotalHorasString(string pValor)
    {
      try
      {
        var plazo = ValidarStringDecimal(pValor);

        decimal horas = Math.Floor(plazo);
        decimal minutos = (plazo - horas) * 60.0M;
        int D = (int)Math.Floor(plazo / 24);
        int H = (int)Math.Floor(horas - (D * 24));
        int M = (int)Math.Floor(minutos);
        var formatoHoraString = String.Format("{0:00}:{1:00}", H, M);

        return formatoHoraString;
      }
      catch (FormatException)
      {
        throw;
      }
    }

    public static string FormatoMoneda(decimal monto, string codMoneda = null)
    {
      string _resp = "";

      CultureInfo culture = CultureInfo.CreateSpecificCulture("es-CR");//default coloness
      switch (codMoneda)
      {
        case CatalogoMoneda.Colones:
          culture = CultureInfo.CreateSpecificCulture("es-CR");
          break;
        case CatalogoMoneda.Dolares:
          culture = CultureInfo.CreateSpecificCulture("en-US");
          break;
      }
      culture.NumberFormat.CurrencyDecimalDigits = 2;
      culture.NumberFormat.CurrencyDecimalSeparator = ".";
      culture.NumberFormat.CurrencyGroupSeparator = ",";
      culture.NumberFormat.CurrencySymbol = string.IsNullOrEmpty(codMoneda) ? "" : culture.NumberFormat.CurrencySymbol;

      _resp = monto.ToString("C2", culture);//dos decimales
      return _resp;
    }

    public static string SimboloMoneda(string codMoneda = null)
    {
      string simbolo = "";

      CultureInfo culture = CultureInfo.CreateSpecificCulture("es-CR");//default coloness
      switch (codMoneda)
      {
        case CatalogoMoneda.Colones:
          culture = CultureInfo.CreateSpecificCulture("es-CR");
          break;
        case CatalogoMoneda.Dolares:
          culture = CultureInfo.CreateSpecificCulture("en-US");
          break;
      }

      culture.NumberFormat.CurrencySymbol = string.IsNullOrEmpty(codMoneda) ? "" : culture.NumberFormat.CurrencySymbol;
      simbolo = culture.NumberFormat.CurrencySymbol;
      return simbolo;
    }

    public static string FormatoMoneda(string monto, string codMoneda = null)
    {
      return FormatoMoneda(string.IsNullOrEmpty(monto) ? 0 : Generales.ValidarStringDecimal(monto), codMoneda);
    }


    public static string FormatoPorcentaje(decimal porcentaje)
    {
      string _resp = "";

      _resp = (porcentaje * 100).ToString() + "%";

      return _resp;
    }

    /// <summary>
    /// Descripcion: Metodo que se usa para las opciones de menu para poder asignarle el metodo que va a ejecutar 
    /// y el controller que se va a ejecutar, no todas las opciones poseen url por lo que sino hay que enviar vacio
    /// </summary>
    /// <param name="url"></param>
    /// <param name="proceso"></param>
    /// <returns></returns>
    public static string ValidarOpcionMenu(string url, string proceso)
    {
      var valor = string.Empty;

      if (!string.IsNullOrEmpty(url))
      {
        var arreglo = url.Split('/');
        if (proceso == "metodo")
        {
          if (arreglo.Length > 0)
          {
            valor = arreglo[0];
          }
        }
        else if (proceso == "controller")
        {
          if (arreglo.Length > 1)
          {
            valor = arreglo[1];
          }
        }
        else if (proceso == "area")
        {
          if (arreglo.Length > 2)
          {
            valor = arreglo[2];
          }
        }
      }
      return valor;
    }

    /// <summary>
    /// Descripción: Metodo que se usa para determinar algunas opciones de menú que requieren una accion especial
    /// Lo que hace es verificar si la opcion que mando por parametro pertenece a una de las opciones que tengo clasificadas como 
    /// especiales en el config, si existe esa opcion se envía el proceso que requiere esa opcion
    /// sino se manda un -1
    /// fecha: 24/07/2018
    /// autor: José Alonso Solís Benavides
    /// </summary>
    /// <param name="opcion"></param>
    /// <returns></returns>
    public static int ValidarAccionEspecial(string opcion)
    {
      var opcionesespeciales = ConfigurationManager.AppSettings["OpcionesEspecialesMenu"].Split(',');

      if (opcionesespeciales.Any(p => p.Trim() == opcion))
        return Convert.ToInt16(ConfigurationManager.AppSettings[opcionesespeciales.Where(p => p.Trim() == opcion).LastOrDefault().Trim().ToString()]);
      else
        return -1;
    }

    public static DateTime validacionFechaString(string campo)
    {
      CultureInfo us = new CultureInfo("es-CR");
      string format = "d/M/yyyy";
      if (campo.Trim().Length == 0)
      {
        return DateTime.ParseExact("01/01/1999", format, us);
      }
      else
      {
        return DateTime.ParseExact(campo, format, us);
      }
    }

    public static DateTime ConvertirStringAFechaHora(string pfecha, string phora)
    {
      CultureInfo culture = new CultureInfo("es-CR");

      string format = phora.Length.Equals(8) ? "d/M/yyyy HH:mm:ss" : "d/M/yyyy HH:mm";

      pfecha = string.IsNullOrEmpty(pfecha) ? "01/01/1999" : pfecha;
      phora = string.IsNullOrEmpty(phora) ? "00:00" : phora;

      return DateTime.ParseExact(pfecha + " " + phora, format, culture);
    }

    public static DateTime ConvertirStringAFechaConHora(string pfecha, string phora)
    {
      var fecha = DateTime.ParseExact(string.IsNullOrEmpty(pfecha) ? null : pfecha, ParametrosSistema.FormatoFecha, null);
      fecha = fecha.AddHours(Convert.ToDouble(phora.Split(':')[0]));
      fecha = fecha.AddMinutes(Convert.ToDouble(phora.Split(':')[1]));

      return fecha;
    }

    /// <summary>
    /// Se usa para casos donde se uso dos variables diferentes para fecha una en string y otra en datetime
    /// puede que en algunos casos venga el string vacio pero la fecha llena y en realidad son la misma variable
    /// por lo cual, debe tener el mismo valor
    /// </summary>
    /// <param name="pstring"></param>
    /// <param name="pfecha"></param>
    /// <returns></returns>
    public static DateTime? ValidarFechaEspecial(string pstring, DateTime? pfecha)
    {
      if (string.IsNullOrEmpty(pstring))
      {
        pstring = ValidarFechaString(pfecha);
      }

      CultureInfo us = new CultureInfo("es-CR");
      string format = "d/M/yyyy";
      if (string.IsNullOrEmpty(pstring))
      {
        return DateTime.ParseExact("01/01/1999", format, us);
      }
      else
      {
        return DateTime.ParseExact(pstring, format, us);
      }
    }

    public ParametroBase validacionFechaFiltro(string fechainicio, string fechaFin)
    {
      ParametroBase respuesta = new ParametroBase { Codigo = Codigos.Exito.ToString(), Descripcion = string.Empty };
      if (!string.IsNullOrEmpty(fechaFin) && !string.IsNullOrEmpty(fechainicio))
      {
        DateTime FechaInicio = DateTime.ParseExact(fechainicio, ParametrosSistema.FormatoFecha, null);
        DateTime FechaFin = DateTime.ParseExact(fechaFin, ParametrosSistema.FormatoFecha, null);

        if (FechaFin < FechaInicio)
        {
          respuesta.Codigo = Codigos.Advertencia.ToString();
          respuesta.Descripcion = "La fecha fin no puede ser mayor a la fecha inicio, favor validar";
        }
      }

      return respuesta;
    }

    public static bool ValidarDiaSemana(string[] diasaComparar)
    {
      //Cultura que deseas
      CultureInfo ci = new CultureInfo("es-CR");

      if (diasaComparar.Any(p => p.ToUpper().Equals(ci.DateTimeFormat.GetDayName(DateTime.Now.DayOfWeek).ToUpper())))
        return true;
      else
        return false;
    }


    /// <summary>
    /// Metodo que obtiene la ruta para los archivos pdf de los visores
    /// </summary>
    /// <param name="pMapPath"></param>
    /// <param name="pArea"></param>
    /// <param name="pModel"></param>
    /// <returns></returns>
    public static string ObtenerRutaMapPath(string pMapPath, string pArea, string pModel)
    {
      try
      {
        //Se valida que el server math trae un \ al final, si lo trae es requerido eliminar del serverpath un caracter de mas
        //sino no se tiene que eliminar
        var cantidad = pMapPath.Substring(pMapPath.Length - 1).Equals(@"\") ? 1 : 0;

        var ruta = "/" + /*(string.IsNullOrEmpty(directorio) ? "" : directorio + "\\") + */(string.IsNullOrEmpty(pModel) ? string.Empty : pModel.Replace(pMapPath, string.Empty));

        //new UTL.Bitacora().InsertarBitacoraF5(new BitacoraGescon
        //{
        //  Proyecto = "BOMBEROSCR.F5.UTL",
        //  Clase = "GENERALES",
        //  Metodo = "ObtenerRutaMapPath",
        //  Usuario = string.Empty,
        //  IP = string.Empty,
        //  ParamentrosEntrada = "pMapPath: " + pMapPath + " - pArea: " + pArea + " - pModel: " + pModel,
        //  ParamentrosSalida = /*"directorio: " + directorio + */" - cantidad: " + cantidad,
        //  Mensaje = ruta,
        //  DetalleError = string.Empty
        //});


        return ruta;
      }
      catch (Exception ex)
      {
        //new UTL.Bitacora().InsertarBitacoraF5(new BitacoraF5
        //{
        //  Proyecto = "BOMBEROSCR.F5.UTL",
        //  Clase = "GENERALES",
        //  Metodo = "ObtenerRutaMapPath",
        //  Usuario = string.Empty,
        //  IP = string.Empty,
        //  ParamentrosEntrada = "pMapPath: " + pMapPath + " - pArea: " + pArea + " - pModel: " + pModel,
        //  ParamentrosSalida = string.Empty,
        //  Mensaje = ex.Message,
        //  DetalleError = ex.StackTrace
        //});

        return string.Empty;
      }
    }



    public static string ObtenerRutaAyuda(string Opcion)
    {
      var archivo = string.Empty;
      switch (Opcion)
      {
        case "EjemploDePantalla":
          archivo = "Ejemplo.pdf";
          break;
        default:
          return "-1";
      }

      return archivo;
    }

    public static string ObtenerStringHojaEstilos(string pMapPath)
    {
      try
      {
        return File.ReadAllText(pMapPath);
      }
      catch (Exception ex)
      {
        //new UTL.Bitacora().InsertarBitacoraF5(new BitacoraF5
        //{
        //  Proyecto = "BOMBEROSCR.F5.UTL",
        //  Clase = "GENERALES",
        //  Metodo = "ObtenerStringHojaEstilos",
        //  Usuario = string.Empty,
        //  IP = string.Empty,
        //  ParamentrosEntrada = "pMapPath: " + pMapPath,
        //  ParamentrosSalida = string.Empty,
        //  Mensaje = ex.Message,
        //  DetalleError = ex.StackTrace
        //});

        return string.Empty;
      }
    }

    public static string ConvertirTextoParametroLegible(string pValor)
    {
      try
      {
        var valorLegible = string.Empty;

        if (!string.IsNullOrEmpty(pValor))
        {
          valorLegible = pValor.Replace("\n", "|");
        }

        return valorLegible;
      }
      catch (FormatException)
      {
        return string.Empty;
      }
    }

    public static decimal ConversionMontosMoneda(decimal pMonto, string pMoneda, decimal pTipoCambio)
    {
      decimal vMontoFinal = 0;

      if (pMoneda == "USD")
      {
        vMontoFinal = pTipoCambio * pMonto;
      }
      else
      {
        vMontoFinal = pMonto;
      }

      return vMontoFinal;
    }

  }
}
