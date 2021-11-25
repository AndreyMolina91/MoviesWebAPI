using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Movies.Utilities.Validations
{
    public class FileSizeValidate : ValidationAttribute
    {
        //inicializamos esta propiedad para almacenar el tamaño en int
        private readonly int _maxSizeMBytes;
        //Metodo constructor para inicializar la instancia creada de la propiedad int _maxS...
        public FileSizeValidate(int maxSizeMBytes)
        {
            _maxSizeMBytes = maxSizeMBytes;
        }

        //Validacion
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            //Si es nulo no se valida el peso
            if (value == null)
            {
                return ValidationResult.Success;
            }
            //Transformacion a IFormfile
            IFormFile formFile = value as IFormFile;
            if (formFile == null) //Si es nulo no hay nada que validar
            {
                return ValidationResult.Success;
            }

            //Si el tamaño del archivo es mayo al, tamaño en bytes * 1024 (Kylobytes) * 1024 (MegaBytes)
            if (formFile.Length>_maxSizeMBytes * 1024 * 1024)
            {
                //Si supera ese tamaño máximo permitido arroja el mensaje de validación
                return new ValidationResult($"El tamaño del archivo supera el máximo permitido {_maxSizeMBytes}mb");
            }
            //Si llegamos hasta aqui es que todo se cumplió satisfactoriamente
            
            return ValidationResult.Success;
        }
    }
}
