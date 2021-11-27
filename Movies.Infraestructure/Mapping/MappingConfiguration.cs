using AutoMapper;
using Movies.Domain.Models;
using Movies.Infraestructure.Dtos;
using System.Collections.Generic;

namespace Movies.Infraestructure.Mapping
{
    public class MappingConfiguration
    {

        public static MapperConfiguration RegisterMaps()
        {

            var mapping = new MapperConfiguration(config =>
            {
                config.CreateMap<GenreModels, GenreModelDto>().ReverseMap();

                config.CreateMap<ActorModels, ActorModelDto>().ReverseMap();
                config.CreateMap<ActorModels, ActorPatchDto>().ReverseMap();
                config.CreateMap<ActorModels, ActorUpsertModelDto>().ReverseMap()
                .ForMember(x => x.Photo, options => options.Ignore()); //Ignorar el mapeo de foto IformFile a String
                
                config.CreateMap<MovieModels, MovieModelDto>().ReverseMap();
                config.CreateMap<MovieModels, MoviePatchDto>().ReverseMap();
                config.CreateMap<MovieModels, MovieUpsertModelDto>().ReverseMap()
                .ForMember(x => x.Poster, options => options.Ignore())
                .ForMember(x => x.MoviesAndGenreList, options => options.MapFrom(MapMoviesAndGenres))
                .ForMember(x => x.MoviesAndActorsList, options => options.MapFrom(MapMoviesAndActors));

                config.CreateMap<MovieDetailsDto, MovieModels>().ReverseMap()
                .ForMember(x => x.ActorDetails, options => options.MapFrom(MapActorDetails))
                .ForMember(x => x.GenresDetails, options => options.MapFrom(MapGenreDetails));
            });
            
            //Mapeo personalizado para Moviedetails que contendrá la información de Actores-Personaje-Pelicula
            List<ActorDetailsDto> MapActorDetails(MovieModels movieModels, MovieDetailsDto movieDetailsDto)
            {
                //Result sera la variable que ccontenga la lista de ActorDetailsDto
                var Result = new List<ActorDetailsDto>();
                if (movieModels.MoviesAndActorsList == null) { return Result; }
                foreach (var actor in movieModels.MoviesAndActorsList)
                {
                    Result.Add(new ActorDetailsDto{ ActorId = actor.ActorModelsId,                         
                        CharacterDetails = actor.MovieCharacter,
                        ActorNameDetails = actor.ActorModels.Name
                    });
                }
                return Result;
                
            }

            //Mapeo personalizado para GenresDetailsDto
            List<GenreDetailsDto> MapGenreDetails(MovieModels movieModels, MovieDetailsDto movieDetailsDto)
            {
                var Result = new List<GenreDetailsDto>();
                if (movieModels.MoviesAndGenreList == null) { return Result; }
                foreach (var genre in movieModels.MoviesAndGenreList)
                {
                    Result.Add(new GenreDetailsDto { GenreDetailsId = genre.GenreModelsId, Title = genre.GenreModels.Title});
                }
                return Result;
            }


            //Mapeo personalizado para movies and genres
            List<MoviesAndGenresModels> MapMoviesAndGenres(MovieUpsertModelDto movieUpsertModelDto, MovieModels movieModels)
            {
                var Result = new List<MoviesAndGenresModels>();
                if (movieUpsertModelDto.GenresIDs == null)
                {
                    return Result;
                }
                foreach (var id in movieUpsertModelDto.GenresIDs)
                {
                    Result.Add(new MoviesAndGenresModels() { GenreModelsId = id });
                }
                return Result;
            }
            //Mapeo personalizado para movies and actors
            List<MoviesAndActorsModels> MapMoviesAndActors(MovieUpsertModelDto movieUpsertModelDto, MovieModels movieModels)
            {
                var Result = new List<MoviesAndActorsModels>();
                if (movieUpsertModelDto.Actors == null)
                {
                    return Result;
                }
                foreach (var actor in movieUpsertModelDto.Actors)
                {
                    Result.Add(new MoviesAndActorsModels() { ActorModelsId = actor.ActorModelsId, MovieCharacter = actor.MovieCharacter});
                }
                return Result;
            }

            return mapping;
        }        
    }
}
