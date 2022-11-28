using System;
using System.Text;
using System.Security.Cryptography;
using System.Configuration;
using BOMBEROSCR.F5.ENT;
using System.Net;

namespace BOMBEROSCR.F5.UTL
{
  public class Seguridad
  {
    /// <summary>
    /// Autor: José Alonso Solís Benavides
    /// Fecha: 05/03/2018
    /// Descripcion: Encripta un texto dado el algoritmo 3DES a través de un password
    /// </summary>
    /// <param name="texto">Texto a Encriptar</param>
    /// <returns></returns>
    public static string Encriptar(string texto)
    {
      var password = Constantes.ParametrosSistema.LlaveEncriptarUsuario;
      TripleDESCryptoServiceProvider tripleDes = new TripleDESCryptoServiceProvider();
      tripleDes = new TripleDESCryptoServiceProvider();
      DESCryptoServiceProvider des = new DESCryptoServiceProvider();
      byte[] buffer;
      tripleDes.Mode = CipherMode.ECB;
      tripleDes.Key = Encoding.UTF8.GetBytes(password);
      buffer = Encoding.UTF8.GetBytes(texto);
      string base64String = Convert.ToBase64String(tripleDes.CreateEncryptor().TransformFinalBlock(buffer, 0, (int)buffer.Length));


      return base64String;
    }


    /// <summary>
    /// Autor: José Alonso Solís Benavides
    /// Fecha: 05/03/2018
    /// Descripcion: Decripta un texto dado el algoritmo 3DES a través de un password
    /// </summary>
    /// <param name="texto"></param>
    /// <returns></returns>
    public static string Decriptar(string texto)
    {
      try
      {
        var password = Constantes.ParametrosSistema.LlaveEncriptar;
        TripleDESCryptoServiceProvider tripleDes = new TripleDESCryptoServiceProvider();
        tripleDes = new TripleDESCryptoServiceProvider();
        DESCryptoServiceProvider des = new DESCryptoServiceProvider();
        byte[] buffer;

        tripleDes.Mode = CipherMode.ECB;
        tripleDes.Key = Encoding.UTF8.GetBytes(password);
        buffer = Convert.FromBase64String(texto);
        string str = Encoding.UTF8.GetString(tripleDes.CreateDecryptor().TransformFinalBlock(buffer, 0, (int)buffer.Length));
        return str;
      }
      catch (Exception)
      {
        string str = "ErrorAlConectarse";
        return str;
      }

    }


    public static string DecriptarUsuario(string texto)
    {
      try
      {
        var password = Constantes.ParametrosSistema.LlaveEncriptarUsuario;
        TripleDESCryptoServiceProvider tripleDes = new TripleDESCryptoServiceProvider();
        tripleDes = new TripleDESCryptoServiceProvider();
        DESCryptoServiceProvider des = new DESCryptoServiceProvider();
        byte[] buffer;

        tripleDes.Mode = CipherMode.ECB;
        tripleDes.Key = Encoding.UTF8.GetBytes(password);
        buffer = Convert.FromBase64String(texto);
        string str = Encoding.UTF8.GetString(tripleDes.CreateDecryptor().TransformFinalBlock(buffer, 0, (int)buffer.Length));
        return str;
      }
      catch (Exception)
      {
        string str = "ErrorAlConectarse";
        return str;
      }

    }
  }
}
