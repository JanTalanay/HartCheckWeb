namespace HartCheck_Doctor_test.FileUploadService;

public interface IFileUploadService
{
    Task <string> UploadFileAsync(IFormFile file);
}