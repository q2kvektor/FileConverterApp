namespace FileConverterApp.Models
{
    public class FileUploadModel
    {
        public List<IFormFile> formFiles { get; set; }
        public List<string> fileNames { get; set; }
    }
}
