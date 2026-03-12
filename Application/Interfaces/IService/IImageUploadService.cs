namespace Application.Interfaces.IService
{
    public interface IImageUploadService
    {
        Task<string> UploadImageAsync(Stream imageStream, string fileName);
    }
}
