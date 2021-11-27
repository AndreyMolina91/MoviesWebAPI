using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movies.DataAccess.Context;
using Movies.Domain.IRepos;
using Movies.Domain.Models;
using Movies.Infraestructure.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieTheatresController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public MovieTheatresController(ApplicationDbContext context,
                                       IUnitOfWork unitOfWork, IMapper mapper )
        {
            _context = context;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<MovieTheatreUpsertDto> PostMovietheater(MovieTheatreModels movieTheatreModels)
        {            
            await _unitOfWork.MovieTheaters.AddModel(movieTheatreModels);
            _unitOfWork.SaveData();
            var movieTheaterDto = _mapper.Map<MovieTheatreUpsertDto>(movieTheatreModels);
            return movieTheaterDto;
        }

        [HttpGet]
        public async Task<IEnumerable<MovieTheatreUpsertDto>> GetMovieTheatres()
        {
            var listTheaters = await _unitOfWork.MovieTheaters.GetAllModel();
            return _mapper.Map<IEnumerable<MovieTheatreUpsertDto>>(listTheaters);
        }
    }
}
