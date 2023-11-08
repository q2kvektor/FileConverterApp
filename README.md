# FileConverterApp

## **Instructions:**
1. Simply launch the web app project via the solution.
2. Write down a local drive path you would like in the appsettings.json of the API.
3. Use the file upload field to add as many files as you like and submit.
4. If the files are legitemate xmls with proper contents they will be converted to JSoN and stored on your selected local drive.

## **API Usage:**
The project contains currently only one API call which is of the type HttpPost that always returns status 200.
The return object will have an error message complemented with an ID as well as the file name correlating to the message.

The Post method accepts an object as a paramenter which consists of two lists - IFormFile and string, for the file and the name of the file.
Post model properties
```csharp
    public List<IFormFile> formFiles { get; set; }
    public List<string> fileNames { get; set; }
```
Response model properties:
```csharp
    public int statusCode { get; set; }
    public int messageId { get; set; }
    public string message { get; set; }
    public string fileName { get; set; } 
```
## **Requirements:**
1. .Net 7.0
2. Newtonsoft.Json 13.0.3

## **Known Issues:**
1. Needs a better robust way to read the XML stream. Current one may cause issues if the load is too great.
