using static FileConverterApp.Entities.ResponseType;

namespace FileConverterApp.Models
{
    public class ResponseModel
    {
        public int statusCode { get; set; }
        public int messageId { get; set; }
        public string message { get; set; } = "";

        public string fileName { get; set; } = "";        

        public void SetRspsValues(int statusCode, int messageId, ResponseTypes message)
        {
            this.statusCode = statusCode;
            this.messageId = messageId;
            this.message = message.ToString().Replace("_", " ");
        }
    }    
}
