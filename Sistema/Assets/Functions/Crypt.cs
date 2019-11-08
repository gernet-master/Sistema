/*
Descrição: Criptografia em MD5
Data: 01/01/2020 - v.1.0
*/

using System;
using System.Globalization;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Security.Cryptography;

namespace Functions
{
    public class Crypt
    {
        // Valida usuário e senha
        public static string CreateHash(string input = "")
        {
            MD5 md5Hash = MD5.Create();
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }

    }
}