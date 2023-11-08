using FileConverterApp.Interfaces;
using System.Xml;
using Newtonsoft.Json;
using FileConverterApp.Models;
using Microsoft.Extensions.Configuration;
using System.Text;
using FileConverterApp.Entities;
using static FileConverterApp.Entities.ResponseType;

namespace FileConverterApp.Services
{
    public class FileService : IFileService
    {       
        private readonly IVerificationService _vService;
        private string? _xmlStream;
        private string? _saveFolder;
        public ResponseModel? rsps;
        
        public string xmlStream 
        {
            get
            {
                return _xmlStream;
            }
            set
            {
                _xmlStream = value;
            }
        }
        public string saveFolder
        {
            get
            {
                return _saveFolder;
            }
            set
            {
                _saveFolder = value;
            }
        }

        public FileService(IVerificationService vService)        
        {                      
            _vService = vService;
            _saveFolder = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("FileConfig")["SaveFolder"];            
        }

        /// <summary>
        /// Process, validate and save file
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public async Task<ResponseModel> ProcessFile(IFormFile file)
        {
            //loads a response model object
            rsps = new ResponseModel();
            rsps.fileName = file.FileName;
            rsps.SetRspsValues(200, 0, ResponseTypes.File_Successfully_Converted);

            //validation for file type
            if (!_vService.IsValidXMLFile(file))
            {
                rsps.SetRspsValues(200, 1, ResponseTypes.Wrong_File_Extension);
                return rsps;
            }

            var xml = _vService.IsValidXMLStream(file);

            //validation for valid XML contents
            if (xml.ChildNodes.Count == 0)
            {
                rsps.SetRspsValues(200, 4, ResponseTypes.Invalid_XML_Contents);
                return rsps;
            }

            //Json conversion and validation
            string json = ConvertToJson(xml);

            if (json == "")
            {
                rsps.SetRspsValues(200, 2, ResponseTypes.File_Conversion_Failed);
                return rsps;
            }

            //result for the save if file already exists or not
            var saveResult = await SaveFile(json);

            if (!saveResult) rsps.SetRspsValues(200, 3, ResponseTypes.File_Could_Not_Be_Saved_Or_Already_Exists);

            return rsps;
        }

        /// <summary>
        /// Converts XML Stream to JSON
        /// </summary>
        /// <param name="xmlStream"></param>
        /// <returns></returns>
        public string ConvertToJson(XmlDocument xmlStream)
        {
            try
            {
                return JsonConvert.SerializeXmlNode(xmlStream);
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        /// <summary>
        /// Saves the file to a designated location
        /// </summary>
        /// <param name="fileStream"></param>
        /// <returns></returns>
        public async Task<bool> SaveFile(string fileStream)
        {
            if (File.Exists(saveFolder + rsps.fileName.Replace(".xml", ".json")))
            {
                return false;
            }

            try
            {               
                File.WriteAllText(saveFolder + rsps.fileName.Replace(".xml", ".json"), fileStream);
                return true;
            }
            catch(Exception ex) 
            {
                return false;
            }            
        }        
    }
}
