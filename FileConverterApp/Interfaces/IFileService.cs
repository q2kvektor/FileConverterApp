using FileConverterApp.Models;
using System.Xml;

namespace FileConverterApp.Interfaces
{
    public interface IFileService
    {
        string xmlStream { get; set; }
        string saveFolder { get; set; }

        Task<ResponseModel> ProcessFile(IFormFile file);
        string ConvertToJson(XmlDocument xmlStream);
        Task<bool> SaveFile(string fileStream);
    }
}
