namespace FileConverterApp.Entities
{
    public class ResponseType
    {
        public enum ResponseTypes
        {
            File_Successfully_Converted = 0,
            Wrong_File_Extension = 1,
            File_Conversion_Failed = 2,
            File_Could_Not_Be_Saved_Or_Already_Exists = 3,
            Invalid_XML_Contents = 4,
            No_Files_Were_Uploaded = 100
        }
    }
}
