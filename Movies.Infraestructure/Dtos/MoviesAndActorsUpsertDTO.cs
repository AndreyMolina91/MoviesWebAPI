namespace Movies.Infraestructure.Dtos
{
    public class MoviesAndActorsUpsertDto
    {
        //Deserealizado con typeBinder en el MovieUpsertModelDto
        public int ActorModelsId { get; set; }
        public string MovieCharacter { get; set; }
    }
}
