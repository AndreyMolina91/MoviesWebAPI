using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movies.Utilities.CustomModelBinder;
using Movies.Utilities.Validations;
using System.Collections.Generic;

namespace Movies.Infraestructure.Dtos
{
    public class MovieUpsertModelDto : MoviePatchDto
    {
        //Validaciones para la imagen
        [FileSizeValidate(maxSizeMBytes: 4)]//Tamaño máximo 4Megas
        [FileTypeValidate(fileTypeGroup: FileTypeGroup.Image)]
        public IFormFile Poster { get; set; }

        /*
         * 
         * Propiedad para que recibamos un listado de enteros 
         * Con esta propiedad podemos recibir de parte del cliente para recibir el listado de ids de los generos de la pelicula
         * Para poder recibir el listado de enteros por parte del cliente es necesario configurar
         * un custom ModelBinder, de lo contrario nos dará como resultado un badrequest por el mapeo en el fromform de parte del modelbinder
         * 
        */
        [ModelBinder(BinderType =typeof(TypeBinder<List<int>>))] //Nuestro custom model binder para recibir el array de ids
        public List<int> GenresIDs { get; set; }

        //ActorID y el personaje de la pelicula
        [ModelBinder(BinderType = typeof(TypeBinder<List<MoviesAndActorsUpsertDTO>>))]
        public List<MoviesAndActorsUpsertDTO> Actors { get; set; } 
        //Una vez confirmado que esta recibiendo los datos el metodo
        //Vamos a mapear el dto personalizado con formember para que se creen las relaciones
        //entre movies-actores-generos
    }
}
