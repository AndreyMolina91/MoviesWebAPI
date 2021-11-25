using AutoMapper;
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
    public class ActorController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfwork;
        private readonly IMapper _mapper;
        private readonly IFilesStorage _filesStorage;
        private readonly string containerFolder = "actorsphotos"; //Nombre de la carpeta contenedora en Azure
        public ActorController(IUnitOfWork unitOfwork, IMapper mapper,
                                IFilesStorage filesStorage)
        {
            _unitOfwork = unitOfwork;
            _mapper = mapper;
            _filesStorage = filesStorage;
        }

        // GET: api/Actors
        [HttpGet]
        public async Task<IEnumerable<ActorModelDto>> GetActorModels()
        {
            var listActors = await _unitOfwork.Actors.GetAllModel();
            var listActorsDto = _mapper.Map<IEnumerable<ActorModelDto>>(listActors);
            return listActorsDto;
        }

        // GET: api/Actor/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ActorModelDto>> GetActorModels(int id)
        {
            var actorModels = await _unitOfwork.Actors.GetModelById(id);
            if (actorModels==null)
            {
                return NotFound();
            }
            var actorDto = _mapper.Map<ActorModelDto>(actorModels);
            return actorDto;
        }


        [HttpPost]
        public async Task PostActorModels([FromForm] ActorUpsertModelDto actorModelsDto)
        {
            var actorModel = _mapper.Map<ActorModels>(actorModelsDto);
            if (actorModelsDto.Photo != null)
            {
                //Instancia de memoryStream para extraer el arreglo de bytes del IFormFile 
                using (var memoryStream = new MemoryStream())
                {
                    await actorModelsDto.Photo.CopyToAsync(memoryStream); //Copiamos el arreglo de bytes en nuestra variable
                    var content = memoryStream.ToArray(); //Creamos una variable que contenga el array de la copia del arreglo de bytes de photo
                    var extension = Path.GetExtension(actorModelsDto.Photo.FileName);
                    //Guardamos la url que se retorna del metodo guardar archivo en Filestorage esta vez en la entidad mapeada del dto
                    //ContainerFolder es nuestro objeto private readonly que contiene el nombre de la carpeta arriba del constructor
                    //Y el contenttype que recibe el metodo SaveFileAsync lo recibimos del contentype del dto en su propiedad photo
                    actorModel.Photo = await _filesStorage.SaveFileAsync(content, extension, containerFolder,
                                                                         actorModelsDto.Photo.ContentType);
                }
            }
            await _unitOfwork.Actors.AddModel(actorModel);
            _unitOfwork.SaveData();
        }

        [HttpPut("{id}")]//Pendiente a utilizar un dto<
        public async Task<ActionResult> UpdateActor(int id, [FromForm] ActorUpsertModelDto actorModelDto)
        {
            ActorModels editModel = await _unitOfwork.Actors.GetModelById(id);
            var editModelDB = _mapper.Map(actorModelDto, editModel);
            using (var memoryStream = new MemoryStream())
            {
                await actorModelDto.Photo.CopyToAsync(memoryStream); //Copiamos el arreglo de bytes en nuestra variable
                var content = memoryStream.ToArray();
                var extension = Path.GetExtension(actorModelDto.Photo.FileName);

                editModelDB.Photo = await _filesStorage.EditFileAsync(content, extension, containerFolder,
                                                                     actorModelDto.Photo.ContentType, editModelDB.Photo);
            }
            _unitOfwork.SaveData();
            return NoContent();   
        }

        [HttpPatch("{id}")] //Editar por campos name, birthday
        public async Task<ActionResult> PatchActor(int id,[FromBody] JsonPatchDocument<ActorPatchDto> actorPatchDocument)
        {
            if (actorPatchDocument==null)
            {
                return BadRequest();
            }
            ActorModels editModel = await _unitOfwork.Actors.GetModelById(id);
            if (editModel==null)
            {
                return NotFound();
            }
            var actorModelDto = _mapper.Map<ActorPatchDto>(editModel);
            actorPatchDocument.ApplyTo(actorModelDto, ModelState);
            var isValid = TryValidateModel(actorModelDto);
            if (!isValid)
            {
                return BadRequest(ModelState);
            }
            _mapper.Map(actorModelDto, editModel);
            _unitOfwork.SaveData();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteActor(ActorModels actorModels)
        {
            await _filesStorage.RemoveFileAsync(actorModels.Photo, containerFolder);
            await _unitOfwork.Actors.RemoveModelById(actorModels.Id);
            _unitOfwork.SaveData();
            return NoContent();
        }
    }
}
