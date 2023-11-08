using FileConverterApp.Entities;
using FileConverterApp.Interfaces;
using FileConverterApp.Models;
using FileConverterApp.Services;
using Microsoft.AspNetCore.Mvc;
using static FileConverterApp.Entities.ResponseType;

namespace FileConverterApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private IFileService _service;
        public FilesController(IFileService fileService)
        {
            _service = fileService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> Post([FromForm] FileUploadModel files)
        {          
            //check if method received 0 files
            if (files.formFiles.Count == 0)
            {
                var rsps = new ResponseModel();
                rsps.SetRspsValues(200, 100, ResponseTypes.No_Files_Were_Uploaded);
                return Ok(rsps);                
            }

            List<Task<ResponseModel>> tasks = new List<Task<ResponseModel>>();

            //loads up tasks for async execution
            for(int i = 0; i < files.formFiles.Count; i ++)
            {
                tasks.Add(_service.ProcessFile(files.formFiles[i]));
            }

            var result = await Task.WhenAll(tasks);

            return Ok(result);
        }
    }
}
