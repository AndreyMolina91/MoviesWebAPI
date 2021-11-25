using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Movies.Domain.IRepos;
using Movies.Domain.Models;
using Movies.Infraestructure.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GenreController(IUnitOfWork unitOfWork,
                               IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // POST: api/GenreModels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task PostGenreModels(GenreModelDto genreModelsDto)
        {
            var genreModel = _mapper.Map<GenreModels>(genreModelsDto);
            await _unitOfWork.Genres.AddModel(genreModel);
            _unitOfWork.SaveData();

        }

        [HttpGet]
        public async Task<IEnumerable<GenreModelDto>> GetGenres()
        {
            var genresList = await _unitOfWork.Genres.GetAllModel();
            var genresListDto = _mapper.Map<IEnumerable<GenreModelDto>>(genresList);
            return genresListDto;
        }

        [HttpGet("{id}")]
        public async Task<GenreModelDto> GetByid(int id)
        {
            var genreModel = await _unitOfWork.Genres.GetModelById(id);
            var genreDto = _mapper.Map<GenreModelDto>(genreModel);
            return genreDto;
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _unitOfWork.Genres.RemoveModelById(id);
        }

        [HttpPut]
        public async Task UpdateGenre(GenreModels genreModels)
        {
            await _unitOfWork.Genres.Update(genreModels);
        }
    }
}
