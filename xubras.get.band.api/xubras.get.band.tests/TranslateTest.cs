namespace xubras.get.band.tests
{
    using Microsoft.AspNetCore.Mvc;
    using System;
    using xubras.globalization;
    using Xunit;
    using Moq;
    using System.Collections.Generic;

    public class TranslateTest
    {
        public TranslateTest()
        {
        }

        [Fact]
        public void GetTranslateStrinToDefaultLanguage()
        {
            var word = "testkey";
            Assert.Equal("Teste Chave", word.Translate());
        }

        [Fact]
        public void GetTranslateStringToEnglish()
        {
            var word = "testkey";
            Assert.Equal("Key Test", word.Translate(new System.Globalization.CultureInfo("en-US")));
        }

        [Fact]
        public void GetTranslateStringToFrench()
        {
            var word = "testkey";
            Assert.Equal("Test clé", word.Translate(new System.Globalization.CultureInfo("fr-FR")));
        }

        [Fact]
        public void GetTranslateListStringToDefaultLanguage()
        {
            var words = new List<string> { "testkey", "testkey1", "testkey2", "testkey3" };
            var result = words.Translate();
            Assert.True(result.Count > 0);
            Assert.Equal("Teste Chave", result["testkey"]);
            Assert.Equal("Teste Chave1", result["testkey1"]);
            Assert.Equal("Teste Chave2", result["testkey2"]);
            Assert.Equal("Teste Chave3", result["testkey3"]);
        }

        [Fact]
        public void GetTranslateListStringToEnglish()
        {
            var words = new List<string> { "testkey", "testkey1", "testkey2", "testkey3" };
            var result = words.Translate(new System.Globalization.CultureInfo("en-US"));
            Assert.True(result.Count > 0);
            Assert.Equal("Key Test", result["testkey"]);
            Assert.Equal("Key Test1", result["testkey1"]);
            Assert.Equal("Key Test2", result["testkey2"]);
            Assert.Equal("Key Test3", result["testkey3"]);
        }

        [Fact]
        public void GetTranslateListStringToThrowException()
        {
            var words = new List<string> { "testkey", "testkey1", "testkey2", "testkey3" };

            Exception ex = Assert.Throws<System.Globalization.CultureNotFoundException>(() => words.Translate(new System.Globalization.CultureInfo("jjjjjj")));

            Assert.Contains("Culture is not supported.", ex.Message);
        }

        [Fact]
        public void GetTranslateStringWithoutGlobalization()
        {
            var result = "GenericError".Translate();

            Assert.Equal("GenericError", result);
        }
    }
}
