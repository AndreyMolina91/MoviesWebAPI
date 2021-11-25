using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Movies.Services.AzureServices
{
    public class FilesStorage : IFilesStorage
    {
        private readonly string connectionString;
        //Constructor
        public FilesStorage(IConfiguration configuration)
        {
            //Conexion a azure storage configurada en el JSON appsettings
            connectionString = configuration.GetConnectionString("AzureStorage");
        }
        public async Task<string> EditFileAsync(byte[] content, string extension, string container, string contentType, string route)
        {
            await RemoveFileAsync(route, container); //Borramos el anterior
            return await SaveFileAsync(content, extension, container, contentType); //Creamos el  nuevo con lo recibido por parametros
        }

        public async Task RemoveFileAsync(string route, string container)
        {
            if (string.IsNullOrEmpty(route)) //Si la ruta está vacia es porque no existe la imagen a borrar
            {
                return;
            }
            //Variable que conecta a Azure y contiene en content de la imagen
            var client = new BlobContainerClient(connectionString, container);
            await client.CreateIfNotExistsAsync();
            var filePath = Path.GetFileName(route); //Obtenemos la ruta
            var blob = client.GetBlobClient(filePath); //blob mediante la ruta
            await blob.DeleteAsync(); //borramos el blob con esa ruta

        }

        public async Task<string> SaveFileAsync(byte[] content, string extension, string container, string contentType)
        {
            //Tomar arreglo de bytes y guardarlo en el azurestorage
            var client = new BlobContainerClient(connectionString, container);
            await client.CreateIfNotExistsAsync();
            client.SetAccessPolicy(PublicAccessType.Blob);

            var fileNameRandom = $"{Guid.NewGuid()}{extension}"; //Generamos el nombre del archivo de manera aleatoria con newguid + la extension recibida por parametro
            var blob = client.GetBlobClient(fileNameRandom);

            var blobUploadOptions = new BlobUploadOptions(); //Blobopciones
            var blobHttpHeader = new BlobHttpHeaders(); //BlobHeader
            blobHttpHeader.ContentType = contentType; //tipo de contenido del blobheader será igual al contenttype recibido del archivo por parametro
            blobUploadOptions.HttpHeaders = blobHttpHeader; //La cabecera del bloboptions sera igual al contentype por parametro

            //Carga hacia azure con el upload recibiendo el contenido + el header del options
            await blob.UploadAsync(new BinaryData(content), blobUploadOptions);
            return blob.Uri.ToString(); //Convertido todo el contenido a string para formar la url
        }
    }
}
