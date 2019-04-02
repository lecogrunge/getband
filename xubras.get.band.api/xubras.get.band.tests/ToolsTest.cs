using System;
using xubras.get.band.domain.Enums;
using xubras.get.band.domain.Util;
using Xunit;

namespace xubras.get.band.tests
{
    public class ToolsTest
    {
        [Fact]
        public void Test1()
        {
            var mimeTypeString = GetMimeType(FileExtension.JPG);


            Assert.Equal("image/jpeg", mimeTypeString);
        }

        private string GetMimeType(FileExtension extension)
        {
            var nameExtension = Enum.GetName(typeof(FileExtension), extension).ToLower();
            MimeTypes mimeType = (MimeTypes)System.Enum.Parse(typeof(MimeTypes), nameExtension);
            return mimeType.GetDescription();
        }
    }
}
