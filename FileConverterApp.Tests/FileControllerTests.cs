using FileConverterApp.Controllers;
using FileConverterApp.Interfaces;
using FileConverterApp.Models;
using FileConverterApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using System.Reflection;

namespace FileConverterApp.Tests
{
    public class FileControllerTests
    {    
        [Fact]
        public async Task Post_Returns_200Status_Successfull_Conversion_And_Save()
        {
            //arrange           
            string solution_dir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName.Replace("\\bin", "");
            string file = Path.Combine(solution_dir, @"TestFiles\Test1\Books.xml").Replace("\\", "/");
            
            var controller = new FilesController(new FileService(new VerificationService()));
            var stream = new MemoryStream(File.ReadAllBytes(file).ToArray());
            var formFile = new FormFile(stream, 0, stream.Length, "streamFile", file.Split(@"/").Last());

            var formModel = new FileUploadModel();
            formModel.formFiles = new List<IFormFile>() { formFile };
            formModel.fileNames = new List<string>() { file.Split(@"/").Last() };

            //act
            var actionResult = controller.Post(formModel);

            //assert
            var result = actionResult.Result as OkObjectResult;
            var response = (result.Value as IEnumerable<ResponseModel>).ToList();
            Assert.Equal(1, response.Count);
            Assert.Equal(0, response[0].messageId);
        }

        [Fact]
        public async Task Post_Returns_200Status_Wrong_File_Extension()
        {
            //arrange
            string solution_dir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName.Replace("\\bin", "");
            string file = Path.Combine(solution_dir, @"TestFiles\Test2\f1040.pdf").Replace("\\", "/");            
            var controller = new FilesController(new FileService(new VerificationService()));
            var stream = new MemoryStream(File.ReadAllBytes(file).ToArray());
            var formFile = new FormFile(stream, 0, stream.Length, "streamFile", file.Split(@"/").Last());

            var formModel = new FileUploadModel();
            formModel.formFiles = new List<IFormFile>() { formFile };
            formModel.fileNames = new List<string>() { file.Split(@"/").Last() };

            //act
            var actionResult = controller.Post(formModel);

            //assert
            var result = actionResult.Result as OkObjectResult;
            var response = (result.Value as IEnumerable<ResponseModel>).ToList();
            Assert.Equal(1, response.Count);
            Assert.Equal(1, response[0].messageId);
        }

        [Fact]
        public async Task Post_Returns_200Status_Successfull_Conversion_And_Save_MultipleFIles()
        {
            //arrange
            string solution_dir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName.Replace("\\bin", "");
            Path.Combine(solution_dir, @"TestFiles\Test2\f1040.pdf").Replace("\\", "/");
            DirectoryInfo d = new DirectoryInfo(Path.Combine(solution_dir, @"TestFiles\Test3\").Replace("\\", "/")); 
            var formModel = new FileUploadModel();
            formModel.formFiles = new List<IFormFile>();
            formModel.fileNames = new List<string>();
            FileInfo[] Files = d.GetFiles();
            foreach(var file in Files)
            {
                var stream = new MemoryStream(File.ReadAllBytes(file.FullName).ToArray());
                var formFile = new FormFile(stream, 0, stream.Length, "streamFile", file.FullName.Split(@"\").Last());
                formModel.formFiles.Add(formFile);
                formModel.fileNames.Add(file.FullName.Split(@"/").Last());
            }

            var controller = new FilesController(new FileService(new VerificationService()));           
            
            //act
            var actionResult = controller.Post(formModel);

            //assert
            var result = actionResult.Result as OkObjectResult;
            var response = (result.Value as IEnumerable<ResponseModel>).ToList();
            
            foreach(var item in response)
            {
                Assert.Equal(0, item.messageId);
            }
        }

        [Fact]
        public async Task Post_Returns_200Status_Invalid_XML_Content()
        {
            //arrange
            string solution_dir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName.Replace("\\bin", "");
            string file = Path.Combine(solution_dir, @"TestFiles\Test4\Invalid.xml").Replace("\\", "/");      
            var controller = new FilesController(new FileService(new VerificationService()));
            var stream = new MemoryStream(File.ReadAllBytes(file).ToArray());
            var formFile = new FormFile(stream, 0, stream.Length, "streamFile", file.Split(@"/").Last());

            var formModel = new FileUploadModel();
            formModel.formFiles = new List<IFormFile>() { formFile };
            formModel.fileNames = new List<string>() { file.Split(@"/").Last() };

            //act
            var actionResult = controller.Post(formModel);

            //assert
            var result = actionResult.Result as OkObjectResult;
            var response = (result.Value as IEnumerable<ResponseModel>).ToList();
            Assert.Equal(1, response.Count);
            Assert.Equal(4, response[0].messageId);
        }
    }
}