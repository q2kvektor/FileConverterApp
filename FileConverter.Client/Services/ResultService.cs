using FileConverterApp.Models;

namespace FileConverter.Client.Services
{
    public interface IResultService
    {
        public bool isProcessDone { get; set; }
        public List<ResponseModel> ResultsSuccess { get; set; }
        public List<ResponseModel> ResultsFailed { get; set; }
    }

    public class ResultService : IResultService
    {
        public bool isProcessDone { get; set; } = false;
        public List<ResponseModel> ResultsSuccess { get; set; } = new List<ResponseModel>();

        public List<ResponseModel> ResultsFailed { get; set; } = new List<ResponseModel>();
    }
}
