using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;

namespace BOMBEROSCR.F5.UTL
{ 

  public static class Extensiones
  {

    public static void Add(
      this IDictionary<string, string> dictionary,
      string key,
      long value)
    {
      dictionary.Add(new KeyValuePair<string, string>(key, value.ToString()));
    }

    public static List<TResult> SelectList<TSource, TResult>(
        this List<TSource> list,
        Func<TSource, TResult> expression)
    {
      return list.Select(expression).ToList();
    }

    public static List<T> WhereList<T>(
        this List<T> list,
        Func<T, bool> expression)
    {
      return list.Where(expression).ToList();
    }

    public static bool In(
        this string value,
        params string[] searchedValues)
    {
      return searchedValues.Contains(value);
    }

    public static bool NotIn(
        this string value,
        params string[] searchedValues)
    {
      return value.In(searchedValues) == false;
    }

    public static bool In(
        this int value,
        params int[] searchedValues)
    {
      return searchedValues.Contains(value);
    }

    public static bool Empty<T>(this List<T> list)
    {
      return list.Any() == false;
    }

    public static bool None<T>(
      this List<T> list,
      Func<T, bool> expression)
    {
      return list.Any(expression) == false;
    }

    public static T Deserializar<T>(this string pObjetoSerializado) where T : new()
    {
      var objeto = new T();
      var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(pObjetoSerializado));
      var serializer = new DataContractJsonSerializer(objeto.GetType());

      objeto = (T)serializer.ReadObject(memoryStream);

      memoryStream.Close();

      return objeto;
    }
    
  }

}
