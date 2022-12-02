using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOMBEROSCR.F5.DAL
{
    public class ValidacionesDeDatos
    {
        public string Error { set; get; }
        public DateTime retornaLaFecha(string date)
        {
            try
            {
                Error = string.Empty;
                return DateTime.Parse(date);
            }
            catch (Exception mensaje)
            {

                Error = mensaje.Message;
                return new DateTime();
            }
        }

        public static string ConvertirString(object pObjeto)
        {
            var valor = string.Empty;
            try
            {
                valor = Convert.ToString(pObjeto);
            }
            catch (Exception) { }

            return valor;
        }


        public static string ConvertirStringBitacora(object pObjeto)
        {
            var valor = "Sin Información";
            try
            {
                if (pObjeto != null)
                {
                    valor = pObjeto.Equals(string.Empty) ? valor : Convert.ToString(pObjeto);
                }
            }
            catch (Exception) { }

            return valor;
        }

        public static byte ConvertirByte(object pObjeto)
        {
            byte valor = 0;
            try
            {
                valor = Convert.ToByte(pObjeto);
            }
            catch (Exception) { }
            return valor;
        }


        public static int ConvertirInt32(object pObjeto)
        {
            var valor = -1;
            try
            {
                valor = Convert.ToInt32(pObjeto);
            }
            catch (Exception) { }
            return valor;
        }

        public static int ConvertirInt32Cero(object pObjeto)
        {
            var valor = 0;
            try
            {
                valor = Convert.ToInt32(pObjeto);
            }
            catch (Exception) { }
            return valor;
        }

        public static int? ConvertirInt32Null(object pObjeto)
        {
            var valor = -1;
            try
            {
                if (pObjeto.ToString() == string.Empty)
                {
                    return null;
                }
                valor = Convert.ToInt32(pObjeto);
            }
            catch (Exception) { }
            return valor;
        }

        /// <summary>
        /// Convertir objecto a long
        /// </summary>
        /// <param name="pObjeto"></param>
        /// <returns></returns>
        public static long ConvertirInt64(object pObjeto)
        {
            long valor = -1;
            try
            {
                valor = Convert.ToInt64(pObjeto);
            }
            catch (Exception) { }
            return valor;
        }

        public static Int64? ConvertirInt64Null(object pObjeto)
        {
            Int64 valor = -1;
            try
            {
                if (pObjeto.ToString() == string.Empty)
                {
                    return null;
                }
                valor = Convert.ToInt64(pObjeto);
            }
            catch (Exception) { }
            return valor;
        }

        public static bool ConvertirBoolen(object pObjeto)
        {
            var valor = false;
            try
            {
                valor = Convert.ToBoolean(pObjeto);
            }
            catch (Exception) { }
            return valor;
        }
        public static bool? ConvertirBoolenNull(string pObjeto)
        {
            try
            {
                if (!string.IsNullOrEmpty(pObjeto))
                {
                    return Convert.ToBoolean(pObjeto);
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static bool? ConvertirObjetoBoolenNull(object pObjeto)
        {
            try
            {
                if (!string.IsNullOrEmpty(pObjeto.ToString()))
                {
                    return Convert.ToBoolean(pObjeto);
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static DateTime ConvertirDatetime(string fecha)
        {
            try
            {
                if (!string.IsNullOrEmpty(fecha))
                {
                    return DateTime.Parse(fecha);
                }
                return new DateTime();
            }
            catch (Exception)
            {
                return new DateTime();
            }
        }

        public static DateTime ConvertirDatetime(object fecha)
        {
            try
            {
                if (fecha != null && fecha.ToString() != string.Empty)
                {
                    return Convert.ToDateTime(fecha);
                }
                return new DateTime();

            }
            catch (Exception)
            {
                return new DateTime();
            }
        }

        /// <summary>
        /// Convertir objeto a decimal
        /// </summary>
        /// <param name="pObjeto"></param>
        /// <returns></returns>
        public static decimal ConvertirDecimal(object pObjeto)
        {
            decimal valor = -1;
            try
            {
                valor = Convert.ToDecimal(pObjeto);
            }
            catch (Exception) { }
            return valor;
        }

        public static decimal? ConvertirDecimalNull(object pObjeto)
        {
            decimal valor = -1;
            try
            {
                if (pObjeto.ToString() == string.Empty)
                {
                    return null;
                }
                valor = Convert.ToDecimal(pObjeto);
            }
            catch (Exception) { }
            return valor;
        }

        public static decimal ConvertirDecimalCero(object pObjeto)
        {
            decimal valor = 0;
            try
            {
                if (pObjeto.ToString() == string.Empty)
                {
                    return valor;
                }
                valor = Convert.ToDecimal(pObjeto);
            }
            catch (Exception) { }
            return valor;
        }

        public static DateTime? ConvertirObjetoDatetimeNull(object fecha)
        {
            try
            {
                if (fecha != null && fecha.ToString() != string.Empty)
                {
                    return Convert.ToDateTime(fecha);
                }
                return null;

            }
            catch (Exception)
            {
                return null;
            }
        }

        public static DateTime? ConvertirDatetimeNull(string fecha)
        {
            try
            {
                if (!string.IsNullOrEmpty(fecha))
                {
                    return DateTime.Parse(fecha);
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static DateTime? ConvertirDatetimeNullExact(string fecha)
        {
            try
            {
                if (!string.IsNullOrEmpty(fecha))
                {
                    var fecha1 = ConvertirDatetime(fecha).ToShortDateString();
                    return DateTime.Parse(fecha1);
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static bool DataReaderHasColumn(System.Data.IDataReader reader, string columnName)
        {
            return reader.GetSchemaTable().Select("ColumnName = '" + columnName + "'").Length > 0;
        }
    }
}
