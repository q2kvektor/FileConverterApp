using FileConverterApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FileConverterApp.Controllers;
using FileConverterApp.Services;
using FileConverter.Client.Services;

namespace FileConverter.Client.Pages
{
    public class IndexModel : PageModel
    {        

        private readonly IResultService _resulstService;
        public List<ResponseModel> responseModel { get; set; }
        public string Baseurl = "https://localhost:7026/";

        public IndexModel(IResultService service)
        {
            responseModel = new List<ResponseModel>();
            _resulstService = service;
        }

        public void OnGet()
        {

        }

        public void OnPost()
        {   
            var model = new FileUploadModel();
            model.formFiles = new List<IFormFile>();
            model.fileNames = new List<string>();

            foreach (var file in Request.Form.Files)
            {
                model.formFiles.Add(file);
                model.fileNames.Add(file.FileName);
            }
            
            if (Request.Form.Files.Count() == 0) return;

            var controller = new FilesController(new FileService(new VerificationService()));

            var actionResult = controller.Post(model).Result as OkObjectResult;
            this.responseModel = (actionResult.Value as IEnumerable<ResponseModel>).ToList();

            this._resulstService.ResultsFailed = responseModel.Where(x => x.messageId != 0).ToList();
            this._resulstService.ResultsSuccess = responseModel.Where(x => x.messageId == 0).ToList();
            this._resulstService.isProcessDone = true;

            return;
        }
    }
}