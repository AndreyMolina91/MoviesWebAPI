namespace Movies.Domain.IRepos
{
    public interface Iid
    {
        //Interfaz para pedir a la clase que implemente un campo id y asi
        //Usar el campo id en el repositorioGeneral
        public int Id { get; set; }
    }
}
