namespace xubras.get.band.domain.Models.General
{
    using xubras.get.band.domain.Enums;

    public class FileParameters
    {
        public FileExtension[] ExtensionPermited { get; set; }
        public string PathToSave { get; set; }
        public string FolderName { get; set; }
        public string FromUser { get; set; }
        public string FileName { get; set; }
        public FileExtension Extension { get; set; }
        public string RenameFileName { get; set; }
        public string Month { get; set; }
    }
}
