namespace be.Services.Interfaces
{
    public interface IImageService
    {
        Task<string?> Upload(IFormFile file);
    }
}