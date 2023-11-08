using System.Xml;

namespace FileConverterApp.Interfaces
{
    public interface IVerificationService
    {
        //1. check if file is a valid file with extension
        //2. check if file contents are XML if validated
        //3. check file name 
        
        public bool IsValidXMLFile(IFormFile file);
        public XmlDocument IsValidXMLStream(IFormFile xmlFile);

    }
}
