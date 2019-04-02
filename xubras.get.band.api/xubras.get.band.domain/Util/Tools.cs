namespace xubras.get.band.domain.Util
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Security.Cryptography;
    using System.Text;
    using System.Xml;

    public static class Tools
    {
        #region Constants

        private const int Keysize = 256;
        private const int DerivationIterations = 1000;

        #endregion

        #region Converts

        public static decimal ToDecimal(this int value)
        {
            return Convert.ToDecimal(value);
        }

        public static decimal ToDecimal(this int? value)
        {
            return Convert.ToDecimal(value);
        }

        public static int ToInt(this decimal value)
        {
            return Convert.ToInt32(value);
        }

        public static int ToInt(this decimal? value)
        {
            return Convert.ToInt32(value);
        }

        public static long ToLong(this decimal value)
        {
            return Convert.ToInt64(value);
        }

        public static long ToLong(this decimal? value)
        {
            return Convert.ToInt64(value);
        }

        public static DateTime? ToDateTime(this DateTime value)
        {
            return Convert.ToDateTime(value);
        }

        public static DateTime ToDateTime(this DateTime? value)
        {
            return Convert.ToDateTime(value);
        }

        public static string ConvertJsonToStringXml(string json)
        {
            try
            {
                if (string.IsNullOrEmpty(json))
                    return json;
                else
                    return JsonConvert.DeserializeXNode(json, "Parametros").ToString();
            }
            catch (JsonReaderException)
            {
                return string.Empty;
            }
        }

        public static string ConvertStringXmlToJson(string xml)
        {
            return JsonConvert.SerializeXmlNode(ConvertStringXmlToXmlDocument(xml));
        }

        public static XmlDocument ConvertStringXmlToXmlDocument(string xml)
        {
            var doc = new XmlDocument();
            doc.LoadXml(xml);

            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                if (node.Name == "ListaSinteticaTitulos" ||
                    node.Name == "ListaAnaliticaTitulos" ||
                    node.Name == "ListaCarteiras" ||
                    node.Name == "ListaContas")
                {
                    XmlAttribute att = doc.CreateAttribute("json", "Array", "http://james.newtonking.com/projects/json");
                    att.Value = "true";
                    node.Attributes.Append(att);
                }
            }

            return doc;
        }

        public static XmlDocument ConvertStringXmlToXml(string xml)
        {
            var doc = new XmlDocument();
            doc.LoadXml(xml);

            return doc;
        }

        public static string ConvertCsvFileToJsonObject(string path)
        {
            var csv = new List<string[]>();
            var lines = File.ReadAllLines(path);

            foreach (string line in lines)
                csv.Add(line.Split(','));

            var properties = lines[0].Split(',');

            var listObjResult = new List<Dictionary<string, string>>();

            for (int i = 1; i < lines.Length; i++)
            {
                var objResult = new Dictionary<string, string>();
                for (int j = 0; j < properties.Length; j++)
                    objResult.Add(properties[j], csv[i][j]);

                listObjResult.Add(objResult);
            }
            return JsonConvert.SerializeObject(listObjResult);
        }

        #endregion

        #region General

        public static string GetEnumDescription(Enum enumType)
        {
            FieldInfo fi = enumType.GetType().GetField(enumType.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes.Length > 0)
            {
                return attributes[0].Description;
            }
            else
            {
                return enumType.ToString();
            }
        }

        public static T GetEnumByDescription<T>(string descriptionEnum)
        {
            var type = typeof(T);
            if (!type.IsEnum) throw new InvalidOperationException();
            foreach (var field in type.GetFields())
            {
                var attribute = Attribute.GetCustomAttribute(field,
                    typeof(DescriptionAttribute)) as DescriptionAttribute;
                if (attribute != null)
                {
                    if (attribute.Description.Trim().ToUpper().Equals(descriptionEnum.Trim().ToUpper()))
                        return (T)field.GetValue(null);
                }
                else
                {
                    if (field.Name.Trim().ToUpper().Equals(descriptionEnum.Trim().ToUpper()))
                        return (T)field.GetValue(null);
                }
            }
            throw new ArgumentException("Not found.", "description");
        }

        public static bool IsPrimitive(this PropertyInfo p)
        {
            Type t = p.PropertyType;

            return new[] {
                typeof(string),
                typeof(String),
                typeof(char),
                typeof(byte),
                typeof(Byte),
                typeof(SByte),
                typeof(sbyte),
                typeof(ushort),
                typeof(short),
                typeof(Int16),
                typeof(UInt16),
                typeof(uint),
                typeof(int),
                typeof(Int32),
                typeof(UInt32),
                typeof(Int64),
                typeof(UInt64),
                typeof(ulong),
                typeof(long),
                typeof(float),
                typeof(double),
                typeof(Double),
                typeof(decimal),
                typeof(Decimal),
                typeof(DateTime),
                typeof(char?),
                typeof(byte?),
                typeof(Byte?),
                typeof(SByte?),
                typeof(sbyte?),
                typeof(ushort?),
                typeof(short?),
                typeof(Int16?),
                typeof(UInt16?),
                typeof(uint?),
                typeof(int?),
                typeof(Int32?),
                typeof(UInt32?),
                typeof(Int64?),
                typeof(UInt64?),
                typeof(ulong?),
                typeof(long?),
                typeof(float?),
                typeof(double?),
                typeof(Double?),
                typeof(decimal?),
                typeof(Decimal?),
                typeof(DateTime?)
            }.Contains(t);
        }

        public static string GenerateNew256Token(string text)
        {
            return Encrypt(text, string.Empty);
        }

        public static string GerarMensagemRetorno(string origem, string codigo, string mensagem)
        {
            return JsonConvert.SerializeObject(new { Origem = origem, Codigo = codigo, Mensagem = mensagem });
        }

        public static string GerarXmlRequest(string valores)
        {
            string _byteOrderMarkUtf8 = Encoding.UTF8.GetString(Encoding.UTF8.GetPreamble());
            var xml = new XmlDocument();
            xml.CreateElement("Entrada");

            var strXml = xml.ToString();

            if (strXml.StartsWith(_byteOrderMarkUtf8))
            {
                strXml = strXml.Remove(0, _byteOrderMarkUtf8.Length);
            }

            return strXml;
        }

        private static string Encrypt(string plainText, string passPhrase)
        {
            // Salt and IV is randomly generated each time, but is preprended to encrypted cipher text
            // so that the same Salt and IV values can be used when decrypting.  
            var saltStringBytes = Generate256BitsOfRandomEntropy();
            var ivStringBytes = Generate256BitsOfRandomEntropy();
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            using (var password = new Rfc2898DeriveBytes(passPhrase, saltStringBytes, DerivationIterations))
            {
                var keyBytes = password.GetBytes(Keysize / 8);
                using (var symmetricKey = new RijndaelManaged())
                {
                    symmetricKey.BlockSize = 256;
                    symmetricKey.Mode = CipherMode.CBC;
                    symmetricKey.Padding = PaddingMode.PKCS7;
                    using (var encryptor = symmetricKey.CreateEncryptor(keyBytes, ivStringBytes))
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                            {
                                cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                                cryptoStream.FlushFinalBlock();
                                // Create the final bytes as a concatenation of the random salt bytes, the random iv bytes and the cipher bytes.
                                var cipherTextBytes = saltStringBytes;
                                cipherTextBytes = cipherTextBytes.Concat(ivStringBytes).ToArray();
                                cipherTextBytes = cipherTextBytes.Concat(memoryStream.ToArray()).ToArray();
                                memoryStream.Close();
                                cryptoStream.Close();
                                return Convert.ToBase64String(cipherTextBytes);
                            }
                        }
                    }
                }
            }
        }

        private static string Decrypt(string cipherText, string passPhrase)
        {
            // Get the complete stream of bytes that represent:
            // [32 bytes of Salt] + [32 bytes of IV] + [n bytes of CipherText]
            var cipherTextBytesWithSaltAndIv = Convert.FromBase64String(cipherText);
            // Get the saltbytes by extracting the first 32 bytes from the supplied cipherText bytes.
            var saltStringBytes = cipherTextBytesWithSaltAndIv.Take(Keysize / 8).ToArray();
            // Get the IV bytes by extracting the next 32 bytes from the supplied cipherText bytes.
            var ivStringBytes = cipherTextBytesWithSaltAndIv.Skip(Keysize / 8).Take(Keysize / 8).ToArray();
            // Get the actual cipher text bytes by removing the first 64 bytes from the cipherText string.
            var cipherTextBytes = cipherTextBytesWithSaltAndIv.Skip((Keysize / 8) * 2).Take(cipherTextBytesWithSaltAndIv.Length - ((Keysize / 8) * 2)).ToArray();

            using (var password = new Rfc2898DeriveBytes(passPhrase, saltStringBytes, DerivationIterations))
            {
                var keyBytes = password.GetBytes(Keysize / 8);
                using (var symmetricKey = new RijndaelManaged())
                {
                    symmetricKey.BlockSize = 256;
                    symmetricKey.Mode = CipherMode.CBC;
                    symmetricKey.Padding = PaddingMode.PKCS7;
                    using (var decryptor = symmetricKey.CreateDecryptor(keyBytes, ivStringBytes))
                    {
                        using (var memoryStream = new MemoryStream(cipherTextBytes))
                        {
                            using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                            {
                                var plainTextBytes = new byte[cipherTextBytes.Length];
                                var decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                                memoryStream.Close();
                                cryptoStream.Close();
                                return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
                            }
                        }
                    }
                }
            }
        }

        private static byte[] Generate256BitsOfRandomEntropy()
        {
            var randomBytes = new byte[32]; // 32 Bytes will give us 256 bits.
            using (var rngCsp = new RNGCryptoServiceProvider())
            {
                // Fill the array with cryptographically secure random bytes.
                rngCsp.GetBytes(randomBytes);
            }
            return randomBytes;
        }

        public static string GetDescription<T>(this Enum GenericEnum) where T : DescriptionAttribute
        {
            Type genericEnumType = GenericEnum.GetType();
            MemberInfo[] memberInfo = genericEnumType.GetMember(GenericEnum.ToString());
            if ((memberInfo != null && memberInfo.Length > 0))
            {
                var _Attribs = memberInfo[0].GetCustomAttributes(typeof(T), false);
                if ((_Attribs != null && _Attribs.Any()))
                {
                    return ((T)_Attribs.ElementAt(0)).Description;
                }
            }
            return GenericEnum.ToString();
        }

        public static string GetDescription(this Enum GenericEnum)
        {
            return GetDescription<DescriptionAttribute>(GenericEnum);
        }

        #endregion
    }
}