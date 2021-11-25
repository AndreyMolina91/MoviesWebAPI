﻿using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Movies.Domain.IRepos;
using Movies.Domain.Models;
using Movies.Infraestructure.Dtos;
using Movies.Services.AzureServices;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Movies.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly string containerFolder = "moviesposters";
        private readonly IFilesStorage _fileStorage;

        public MoviesController(IUnitOfWork unitOfWork, IMapper mapper, IFilesStorage fileStorage)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _fileStorage = fileStorage;
        }

        // GET: api/Movies
        [HttpGet]
        public async Task<IEnumerable<MovieModelDto>> GetMovieModels()
        {
            var listMovies = await _unitOfWork.Movies.GetAllModel(includeproperties: "MoviesAndActorsModels,MoviesAndGenresModels");
            var listDto = _mapper.Map<IEnumerable<MovieModelDto>>(listMovies);
            return listDto;
        }

        [HttpPost]
        public async Task<ActionResult<MovieUpsertModelDto>> PostMovieModels([FromForm] MovieUpsertModelDto movieUpsertDto)
        {
            var movieBD = _mapper.Map<MovieModels>(movieUpsertDto);

            if (movieUpsertDto.Poster != null)
            {
                //Instancia de memoryStream para extraer el arreglo de bytes del IFormFile 
                using (var memoryStream = new MemoryStream())
                {
                    await movieUpsertDto.Poster.CopyToAsync(memoryStream); //Copiamos el arreglo de bytes en nuestra variable
                    var content = memoryStream.ToArray(); //Creamos una variable que contenga el array de la copia del arreglo de bytes de photo
                    var extension = Path.GetExtension(movieUpsertDto.Poster.FileName);
                    //Guardamos la url que se retorna del metodo guardar archivo en Filestorage esta vez en la entidad mapeada del dto
                    //ContainerFolder es nuestro objeto private readonly que contiene el nombre de la carpeta arriba del constructor
                    //Y el contenttype que recibe el metodo SaveFileAsync lo recibimos del contentype del dto en su propiedad photo
                    movieBD.Poster = await _fileStorage.SaveFileAsync(content, extension, containerFolder,
                                                                         movieUpsertDto.Poster.ContentType);
                }
            }
            _unitOfWork.Movies.OrderActors(movieBD);
            await _unitOfWork.Movies.AddModel(movieBD);
            _unitOfWork.SaveData();

            return movieUpsertDto;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateMovie(int id, [FromForm] MovieUpsertModelDto movieModelsDto)
        {
            MovieModels editModel = await _unitOfWork.Movies.GetModelById(id, includeproperties: "MoviesAndActorsModels,MoviesAndGenresModels");
            var editModelDB = _mapper.Map(movieModelsDto, editModel);
            using (var memoryStream = new MemoryStream())
            {
                await movieModelsDto.Poster.CopyToAsync(memoryStream); //Copiamos el arreglo de bytes en nuestra variable
                var content = memoryStream.ToArray();
                var extension = Path.GetExtension(movieModelsDto.Poster.FileName);

                editModelDB.Poster = await _fileStorage.EditFileAsync(content, extension, containerFolder,
                                                                     movieModelsDto.Poster.ContentType, editModelDB.Poster);
            }
            _unitOfWork.Movies.OrderActors(editModelDB);
            _unitOfWork.SaveData(); 
            return NoContent();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<MovieModelDto>> GetMovieById([FromRoute]int id)
        {
            var movieModel = await _unitOfWork.Movies.GetModelById(id, includeproperties: "MoviesAndActorsModels,MoviesAndGenresModels");
            var movieDto = _mapper.Map<MovieModelDto>(movieModel);
            return movieDto;
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> UpdatePartialMovie(int id, [FromBody] JsonPatchDocument<MoviePatchDto> moviePatchDocument)
        {
            if (moviePatchDocument == null)
            {
                return BadRequest();
            }
            MovieModels editModel = await _unitOfWork.Movies.GetModelById(id);
            if (editModel == null)
            {
                return NotFound();
            }
            var movieModelDto = _mapper.Map<MoviePatchDto>(editModel);
            moviePatchDocument.ApplyTo(movieModelDto, ModelState);
            var isValid = TryValidateModel(movieModelDto);
            if (!isValid)
            {
                return BadRequest(ModelState);
            }
            _mapper.Map(movieModelDto, editModel);
            _unitOfWork.SaveData();
            return NoContent();
        }
    }
}