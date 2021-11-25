using Microsoft.AspNetCore.Http;
using Movies.Utilities.Validations;

namespace Movies.Infraestructure.Dtos
{
    public class ActorUpsertModelDto : ActorPatchDto
    {
        //Campo IFormFile para recibir el archivo en formato de foto al crear el actor
        [FileSizeValidate(maxSizeMBytes: 4)]//Tamaño máximo 4Megas
        [FileTypeValidate(fileTypeGroup:FileTypeGroup.Image)] //El campo por parametro: ClaseEnum.Imagen (Esto contiene los jpeg, png, etc)
        public IFormFile Photo { get; set; }
    }
}
