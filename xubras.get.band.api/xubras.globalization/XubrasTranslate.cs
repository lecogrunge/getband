namespace xubras.globalization
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Xml;
    using System.Xml.Linq;

    public static class XubrasTranslate
    {
        #region [ Attributes ]

        private static string _extension = ".xml";
        private static string _culture = Thread.CurrentThread.CurrentUICulture.Name;
        private static string _path = $@"{Directory.GetCurrentDirectory().Split("bin")[0]}\Globalization\";
        private static string _defaultPath = $@"{Directory.GetCurrentDirectory().Split("bin")[0]}\Globalization\{_culture}{_extension}";

        #endregion

        #region [ public methods ]

        /// <summary>
        /// 
        /// </summary>
        /// <param name="culture"></param>
        /// <returns></returns>
        public static string Translate(CultureInfo culture)
        {
            return string.Empty;
        }

        /// <summary>
        ///     Get word translate using default O. S language. Necessary create xml file with culture. If don't exist, return string empty
        /// </summary>
        /// <param name="word">word to translate</param>
        /// <returns></returns>
        public static string Translate(this string wordKey)
        {
            return GetXmlSection(wordKey);
        }

        /// <summary>
        ///     Get word translate using default O. S language. Necessary create xml file with culture. If don't exist, return string empty
        /// </summary>
        /// <param name="word">word to translate</param>
        /// <returns></returns>
        public static string Translate(this string wordKey, CultureInfo culture)
        {
            return GetXmlSection(wordKey, culture.Name);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="wordKeys"></param>
        /// <returns></returns>
        public static Dictionary<string, string> Translate(this List<string> wordKeys)
        {
            return GetXmlSection(wordKeys);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="wordKeys"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public static Dictionary<string, string> Translate(this List<string> wordKeys, CultureInfo culture)
        {
            return GetXmlSection(wordKeys, culture.Name);
        }

        #endregion

        #region [ private methods ]

        private static string GetXmlSection(string key, string culture = null)
        {
            XDocument doc = !string.IsNullOrEmpty(culture) ? XDocument.Load($"{_path}{culture}{_extension}") : XDocument.Load(_defaultPath);
            var section = doc.Descendants().ToList().Where(n => n.FirstAttribute.Value == key);
            return !string.IsNullOrEmpty(section.FirstOrDefault()?.Value) ? section.FirstOrDefault().Value : key;
        }

        private static Dictionary<string, string> GetXmlSection(List<string> keys, string culture = null)
        {
            var dictionary = new Dictionary<string, string>();

            XDocument doc = !string.IsNullOrEmpty(culture) ? XDocument.Load($"{_path}{culture}{_extension}") : XDocument.Load(_defaultPath);

            foreach (var key in keys)
            {
                var section = doc.Descendants().ToList().Where(n => n.FirstAttribute.Value == key);
                dictionary.Add(key, !string.IsNullOrEmpty(section.FirstOrDefault()?.Value) ? section.FirstOrDefault().Value : key);
            }

            return dictionary;
        }

        #endregion
    }
}
