namespace xubras.get.band.tests
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Options;
    using Moq;
    using System;
    using System.IO;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;
    using xubras.get.band.api.Controllers;
    using xubras.get.band.data.Transactions;
    using xubras.get.band.domain.Enums;
    using xubras.get.band.domain.Models.General;
    using xubras.get.band.domain.Util;
    using xubras.globalization;
    using Xunit;

    public class FileUploadTest
    {
        private readonly Mock<IOptions<Configuration>> _configuration = new Mock<IOptions<Configuration>>();
        private readonly FileController _fileController;
        private readonly IUnitOfWork _unitOfWork;

        public FileUploadTest(IUnitOfWork unitOfWork)
        {
            _fileController = new FileController(_configuration.Object, unitOfWork);
        }

        [Fact]
        public async Task UploadTxtFileToAsync()
        {
            var file = new Mock<IFormFile>();
            var folderName = DateTime.Today.Year.ToString();
            var pathToSave = @"F:\FileUploaded\";
            var sourceImg = File.OpenRead(@"F:\FileToUpload\json.txt");
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(sourceImg);
            writer.Flush();
            ms.Position = 0;
            var fileName = @"F:\FileToUpload\json.txt";
            file.Setup(f => f.FileName).Returns(fileName); ;
            file.Setup(_ => _.CopyToAsync(It.IsAny<Stream>(), It.IsAny<CancellationToken>()))
                .Returns((Stream stream, CancellationToken token) => ms.CopyToAsync(stream))
                .Verifiable();

            var inputFile = file.Object;

            var result = await _fileController.FileUpload(inputFile, GetFileParameters(FileExtension.TXT, new FileExtension[] { FileExtension.TXT, FileExtension.DOC },
                                                                                                 pathToSave, folderName, string.Empty, "json.txt", "json2",
                                                                                                 DateTime.Today.Month.ToString()));

            Assert.Equal("FileCreated".Translate(), result.ReasonPhrase);
            Assert.Equal(HttpStatusCode.Created, result.StatusCode);
        }

        [Fact]
        public async Task UploadImgFileToAsyncWithInvalidExtension()
        {
            var file = new Mock<IFormFile>();
            var folderName = DateTime.Today.Year.ToString();
            var pathToSave = @"F:\FileUploaded\";
            var sourceImg = File.OpenRead(@"F:\FileToUpload\bobo2.jpg");
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(sourceImg);
            writer.Flush();
            ms.Position = 0;
            var fileName = @"F:\FileToUpload\bobo2.jpg";
            file.Setup(f => f.FileName).Returns(fileName); ;
            file.Setup(_ => _.CopyToAsync(It.IsAny<Stream>(), It.IsAny<CancellationToken>()))
                .Returns((Stream stream, CancellationToken token) => ms.CopyToAsync(stream))
                .Verifiable();

            var inputFile = file.Object;

            var result = await _fileController.FileUpload(inputFile, GetFileParameters(FileExtension.JPG, new FileExtension[] { FileExtension.TXT, FileExtension.DOC },
                                                                                                 pathToSave, folderName, string.Empty, "json.txt", "json2",
                                                                                                 DateTime.Today.Month.ToString()));

            Assert.Equal("FileExtensionInvalid".Translate(), result.ReasonPhrase);
            Assert.Equal(HttpStatusCode.NotAcceptable, result.StatusCode);
        }

        [Fact]
        public async Task UploadImgFileAsync()
        {
            var file = new Mock<IFormFile>();
            var folderName = DateTime.Today.Year.ToString();
            var pathToSave = @"F:\FileUploaded\";
            var sourceImg = File.OpenRead(@"F:\FileToUpload\bobo2.jpg");
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(sourceImg);
            writer.Flush();
            ms.Position = 0;
            var fileName = @"F:\FileToUpload\bobo2.jpg";
            file.Setup(f => f.FileName).Returns(fileName); ;
            file.Setup(_ => _.CopyToAsync(It.IsAny<Stream>(), It.IsAny<CancellationToken>()))
                .Returns((Stream stream, CancellationToken token) => ms.CopyToAsync(stream))
                .Verifiable();

            var inputFile = file.Object;

            var result = await _fileController.FileUpload(inputFile, GetFileParameters(FileExtension.JPG, new FileExtension[] { FileExtension.JPG, FileExtension.JPGE },
                                                                                                 pathToSave, folderName, string.Empty, "bobo2.jpg", "bobo22",
                                                                                                 DateTime.Today.Month.ToString()));

            Assert.Equal("FileCreated".Translate(), result.ReasonPhrase);
            Assert.Equal(HttpStatusCode.Created, result.StatusCode);
        }

        [Fact]
        public async Task UploadImgFileWithoutPathAsync()
        {
            var file = new Mock<IFormFile>();
            var folderName = DateTime.Today.Year.ToString();
            var pathToSave = "";
            var sourceImg = File.OpenRead(@"F:\FileToUpload\bobo2.jpg");
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(sourceImg);
            writer.Flush();
            ms.Position = 0;
            var fileName = @"F:\FileToUpload\bobo2.jpg";
            file.Setup(f => f.FileName).Returns(fileName); ;
            file.Setup(_ => _.CopyToAsync(It.IsAny<Stream>(), It.IsAny<CancellationToken>()))
                .Returns((Stream stream, CancellationToken token) => ms.CopyToAsync(stream))
                .Verifiable();

            var inputFile = file.Object;

            var result = await _fileController.FileUpload(inputFile, GetFileParameters(FileExtension.JPG, new FileExtension[] { FileExtension.JPG, FileExtension.JPGE },
                                                                                                 pathToSave, folderName, string.Empty, "json.txt", "json2",
                                                                                                 DateTime.Today.Month.ToString()));

            Assert.Equal("DirectoryNotExists".Translate(), result.ReasonPhrase);
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }

        [Fact]
        public async Task UploadMovieFileAsync()
        {
            var file = new Mock<IFormFile>();
            var folderName = DateTime.Today.Year.ToString();
            var pathToSave = @"F:\FileUploaded\";
            var sourceImg = File.OpenRead(@"F:\FileToUpload\loucura.mp4");
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(sourceImg);
            writer.Flush();
            ms.Position = 0;
            var fileName = @"F:\FileToUpload\loucura.mp4";
            file.Setup(f => f.FileName).Returns(fileName); ;
            file.Setup(_ => _.CopyToAsync(It.IsAny<Stream>(), It.IsAny<CancellationToken>()))
                .Returns((Stream stream, CancellationToken token) => ms.CopyToAsync(stream))
                .Verifiable();

            var inputFile = file.Object;

            var result = await _fileController.FileUpload(inputFile, GetFileParameters(FileExtension.MP4, new FileExtension[] { FileExtension.MP4 },
                                                                                                 pathToSave, folderName, string.Empty, "loucura.mp4", "loucura2",
                                                                                                 DateTime.Today.Month.ToString()));

            Assert.Equal("FileCreated".Translate(), result.ReasonPhrase);
            Assert.Equal(HttpStatusCode.Created, result.StatusCode);
        }

        [Fact]
        public async Task ListImgFileToAsync()
        {
            var pathToSave = @"F:\FileUploaded\";
            var folderName = DateTime.Today.Year.ToString();
            var fileName = "bobozinho.jpg";
            var userName = "Tasso";
            var result = await _fileController.File(GetFileParameters(FileExtension.JPG, new FileExtension[] { FileExtension.JPG, FileExtension.JPGE },
                                                                      pathToSave, folderName, userName, fileName, string.Empty, string.Empty));
        }


        private FileParameters GetFileParameters(FileExtension extension, FileExtension[] extensionPermited,
                                                 string pathToSave, string folderName, string fromUser,
                                                 string fileName, string renameFileName, string month)
        {
            return new FileParameters
            {
                Extension = extension,
                ExtensionPermited = extensionPermited,
                FileName = fileName,
                FolderName = folderName,
                FromUser = fromUser,
                Month = month,
                PathToSave = pathToSave,
                RenameFileName = renameFileName
            };
        }
    }
}
