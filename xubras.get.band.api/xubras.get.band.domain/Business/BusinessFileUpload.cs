namespace xubras.get.band.domain.Business
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Options;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using xubras.get.band.domain.Contract.Business;
    using xubras.get.band.domain.Contract.Repository;
    using xubras.get.band.domain.Enums;
    using xubras.get.band.domain.Models.General;
    using xubras.get.band.domain.Util;
    using xubras.globalization;

    public class BusinessFileUpload : IBusinessFileUpload
    {
        #region [ Attributes ]

        private new HttpResponseMessage Response = new HttpResponseMessage();
        private new HttpRequestMessage Request = new HttpRequestMessage();
        private string _pathFileUploaded = $@"{Directory.GetCurrentDirectory().Split("bin")[0]}FileUploaded\";
        private readonly Configuration _configuration;

        #endregion

        #region [ Constructor ]

        public BusinessFileUpload(IOptions<Configuration> configuration)
        {
            _configuration = configuration.Value;
        }

        #endregion

        #region [ Public Methods ]

        public async Task<HttpResponseMessage> Upload(IFormFile file, FileParameters parameters)
        {
            var permitedExtensions = parameters.ExtensionPermited;
            string fullPathSaveFiles = string.Empty;

            if (!string.IsNullOrEmpty(parameters.PathToSave))
                fullPathSaveFiles = $@"{parameters.PathToSave}{parameters.FolderName}\{parameters.FromUser}\";
            else
                return GetRespose(HttpStatusCode.NotFound, "DirectoryNotExists");

            //Caso não tenha restrição de estensões, verifica se o arquivo não contém extensões nocivas. e Caso tenha extensões definidas, o arquivo deverá conter alguma delas.
            if (ValidateExtensions(permitedExtensions, parameters.Extension))
            {
                // Verifica a existência do diretório proposto
                if (!DirectoryIsExists(fullPathSaveFiles))
                    Directory.CreateDirectory(fullPathSaveFiles);

                try
                {
                    if (file != null)
                    {
                        var extention = parameters.FileName.Split('.')[1];
                        var renamedFile = parameters.RenameFileName + "." + extention;

                        // Salva o arquivo
                        using (FileStream output = new FileStream($@"{fullPathSaveFiles}{renamedFile}", FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
                        {
                            await file.CopyToAsync(output);
                            output.Close();
                        }

                        //Arquivo criado
                        return GetRespose(HttpStatusCode.Created, "FileCreated");
                    }
                    else
                        // Nenhum arquivo foi selecionado
                        return GetRespose(HttpStatusCode.NotFound, "FileNotContent");
                }
                catch (Exception e)
                {
                    return GetRespose(HttpStatusCode.InternalServerError, "GenericError");
                }
            }
            else
                //Extensão de arquivo inválida
                return GetRespose(HttpStatusCode.NotAcceptable, "FileExtensionInvalid");
        }

        public async Task<HttpResponseMessage> Delete(FileParameters parameters)
        {
            string fullPathSavedFiles = $@"{parameters.PathToSave}{parameters.FolderName}\{parameters.FromUser}\";
            var path = $@"{fullPathSavedFiles}{parameters.FileName}";

            if (DirectoryIsExists(fullPathSavedFiles) && FileIsExists(path))
            {
                // Deleta o arquivo
                DeleteExistFile(path);
                return GetRespose(HttpStatusCode.MovedPermanently, "FileRemoved");
            }

            return GetRespose(HttpStatusCode.BadRequest, "FileNotRemoved");
        }

        public async Task<Dictionary<string, Dictionary<string, Stream>>> Download(FileParameters parameters)
        {
            string fullPathSavedFiles = $@"{parameters.PathToSave}{parameters.FolderName}\{parameters.FromUser}\";
            var path = $@"{fullPathSavedFiles}{parameters.FileName}";

            if (DirectoryIsExists(fullPathSavedFiles) && FileIsExists(path))
            {
                var memory = new MemoryStream();
                using (var stream = new FileStream(path, FileMode.Open))
                {
                    var minetype = stream.GetType();

                    await stream.CopyToAsync(memory);
                    stream.Close();
                }
                memory.Position = 0;

                var contentDictionary = new Dictionary<string, Stream>
                {
                    { GetMimeType(parameters.Extension), memory }
                };

                var result = new Dictionary<string, Dictionary<string, Stream>>() {
                    {"File", contentDictionary }
                };

                return result;
            }
            else
            {
                var contentDictionary = new Dictionary<string, Stream>
                {
                    { "", null }
                };

                var result = new Dictionary<string, Dictionary<string, Stream>>() {
                    {"File", contentDictionary }
                };

                return result;
            }
        }

        public async Task<string[]> List(FileParameters parameters, bool getOnce = false)
        {
            string fullPathSavedFiles = $@"{parameters.PathToSave}{parameters.FolderName}\{parameters.FromUser}\";

            if (DirectoryIsExists(fullPathSavedFiles))
            {
                return getOnce ? Directory.GetFiles(fullPathSavedFiles).Select(f => Path.GetFileName(f)).Where(s => s == parameters.FileName).ToArray() : Directory.GetFiles(fullPathSavedFiles).Select(f => Path.GetFileName(f)).ToArray();
            }

            return new string[] { };
        }

        #endregion

        #region [ Private Methods ]

        private string GetMimeType(FileExtension extension)
        {
            var nameExtension = Enum.GetName(typeof(FileExtension), extension).ToLower();
            MimeTypes mimeType = (MimeTypes)System.Enum.Parse(typeof(MimeTypes), nameExtension);
            return mimeType.GetDescription();
        }

        private bool ValidateExtensions(FileExtension[] permitedExtensions, FileExtension extension)
        {
            return (permitedExtensions == null || permitedExtensions.Contains(extension) &&
                   (!(extension == FileExtension.EXE || extension == FileExtension.VBS || extension == FileExtension.CMD ||
                      extension == FileExtension.BAT || extension == FileExtension.SRC || extension == FileExtension.WS ||
                      extension == FileExtension.CONFIG)));
        }

        private HttpResponseMessage GetRespose(HttpStatusCode status, string text)
        {
            Response.StatusCode = status;
            Response.ReasonPhrase = text.Translate();

            return Response;
        }

        private bool FileIsExists(string path)
        {
            return System.IO.File.Exists(path);
        }

        private bool DirectoryIsExists(string path)
        {
            return Directory.Exists(path);
        }

        private void DeleteExistFile(string path)
        {
            System.IO.File.Delete(path);
        }

        private string Paging(int skip = 1, int take = 10)
        {
            return $"{_configuration.BaseUrl}?page={skip}&numberOfRecords={take}";
        }

        private static List<string> GetIncludes()
        {
            return new List<string> { "" };
        }

        #endregion

    }
}
