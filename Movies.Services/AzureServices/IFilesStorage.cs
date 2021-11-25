using System.Threading.Tasks;

namespace Movies.Services.AzureServices
{
    //Almacenador de archivos
    public interface IFilesStorage
    {
        Task<string> SaveFileAsync(byte[] content, string extension, string container, string contentType);
        Task<string> EditFileAsync(byte[] content, string extension, string container, string contentType, string route);
        Task RemoveFileAsync(string route, string container);
    }
}
