using FileConverterApp.Interfaces;
using System.Text;
using System.Xml;

namespace FileConverterApp.Services
{
    public class VerificationService : IVerificationService
    {
        public VerificationService()
        {
            
        }

        /// <summary>
        /// Validates file by extension
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public bool IsValidXMLFile(IFormFile file)
        {
            if(file.FileName.EndsWith("xml"))
            {
                return true;
            }

            return false;           
        }

        /// <summary>
        /// Converts and validates XML based on contents
        /// </summary>
        /// <param name="xmlFile"></param>
        /// <returns></returns>
        public XmlDocument IsValidXMLStream(IFormFile xmlFile)
        {
            XmlDocument xml = new XmlDocument();
            try
            {
                var result = new StringBuilder();
                using (var reader = new StreamReader(xmlFile.OpenReadStream()))
                {
                    while (reader.Peek() >= 0)
                        result.AppendLine(reader.ReadLine());
                }
                
                xml.LoadXml(result.ToString());
            }
            catch (Exception ex)
            {
                return new XmlDocument();
            }

            return xml;
        }
    }
}
