namespace Movies.Infraestructure.Dtos
{
    public class ActorDetailsDto
    {
        //contendrá la información respectiva al Actor de tablas ActorModels y personaje de tabla MoviesAndActorsModels
        public int ActorId { get; set; }
        public string CharacterDetails { get; set; }
        public string ActorNameDetails { get; set; }
    }
}
